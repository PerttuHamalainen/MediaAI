/**
 * This file is part of the materials for the course "Intelligent Computational Media" (formerly: "Computational Intelligence in Games")
 * of Aalto University, Finland.
 *
 * You are free to use this code for any purpose, but at your own risk.
 * */


//TODO: initial guess handling: instead of sampling z from std. normal, use d=initial guess, z=inv(M)d
// => requires matrix inversion => need to integrate Math.NET or Accord.NET
// In online use, might be better to just reinit the optimization if initial guess is best and much further from xmean than current step size (sigma)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ICM
{
    /* MAES interface, just to make usage easier, as the actual implementation is not the easiest to parse.
     * Usage: First call init, and allocate an OptimizationSample array for the population. 
     * After this, repeat: 1) call generateSamples(), 2) update the objective function values of the samples, and 3) call update()
     * */
    public interface IMAES
    {
        //Returns the recommended minimum population size for a given problem dimensionality (number of optimized variables)
        int recommendedPopulationSize(int dim);
        /* Inititalize optimization, allocate necessary data structures.
         * 
         * dim              Problem dimensionality, i.e., number of optimized variables
         * populationSize   How many samples per iteration (i.e., each generateSamples() and update() call
         * initialMean      Initial guess for the optimum
         * initialStepSize  Standard deviation for the initial sampling distribution
         * mode             Specifies whether this is a maximization or minimization problem
        */
        void init(int dim, int populationSize, double[] initialMean, float initialStepSize, OptimizationModes mode);
        /* Generates a population of samples from the current sampling distribution
         * 
         * Parameters:
         * samples          An array of samples which will be updated
         * nInitialGuesses  The first nInitialGuesses samples are treated as initial guesses, i.e., their x vectors will not be altered.
         *                  Note that this works correctly only for the first iteration (a current implementation limitation)
         **/
        void generateSamples(OptimizationSample[] samples, int nInitialGuesses = 0);
        //Update the sampling distribution, assuming that the objective function values for the samples array have been computed by the caller
        double update(OptimizationSample[] samples);
        //Returns the best parameter vector found so far
        double[] getBest();
        //Returns the best objective function value found so far
        double getBestObjectiveFuncValue();
        //A shorthand for running the generateSamples() and update() iteration to optimize a given objective function.
        //The function should be of type double func(double[] x)
        void optimize(Func<double[], int, double> objectiveFunc, int maxIter);
    }


    /* MAES optimizer. The code is ported from Ilya Loschilov's C++ code 
     * 
     * */
    public class MAES : IMAES
    {
        [HideInInspector]
        public List<OptimizationSample> samples = new List<OptimizationSample>();
        int dim;
        [HideInInspector]
        System.Random rng = new System.Random();
        OptimizationModes mode = OptimizationModes.minimize;
        int lambda;
        double sigma;
        int mu;
        const double minsigma = 1e-20;				// stop if sigma is smaller than minsigma
        double chiN;
        double[,] M;
        double[,] Mmult;

        List<double[]> arx = new List<double[]>();
        List<double[]> arz = new List<double[]>();
        List<double[]> ard = new List<double[]>();
        double[] ps;
        double[] xmean;
        double[] zmean;
        double[] xold;
        double[] z;
        double[] Mz;
        double[] weights;
        double[] xBest;
        double bestFitness;
        public double[] arfitness;
        int[] arindex;
        List<SortedVal> arr_tmp;
        double cs, lcoef, c1, cmu,mueff;
        public void init(int dim, int populationSize, double[] initialMean, float initialStepSize, OptimizationModes mode)
        {
            this.dim = dim;
            this.mode = mode;
            int N = dim;
            lambda = populationSize;	// population size, e.g., 4+floor(3*log(N));
            mu = lambda / 2;					// number of parents, e.g., floor(lambda/2);
            sigma = 0.3 * 2.0f;		// initial step-size
            chiN = Math.Sqrt((double)N) * (1.0 - 1.0 / (4.0 * N) + 1 / (21.0 * N * N));

            M = new double[N, N];
            Mmult = new double[N, N];
            ps = new double[N];
            xmean = new double[N];
            xBest = new double[N];
            zmean = new double[N];
            xold = new double[N];
            z = new double[N];
            Mz = new double[N];
            weights = new double[mu];
            arfitness = new double[lambda];
            arindex = new int[lambda];
            arr_tmp = new List<SortedVal>(lambda);
            bestFitness = mode == OptimizationModes.minimize ? double.PositiveInfinity : double.NegativeInfinity;
            for (int i = 0; i < lambda; i++)
            {
                arx.Add(new double[N]);
                arz.Add(new double[N]);
                ard.Add(new double[N]);
            }
            for (int i = 0; i < lambda; i++)
            {
                arr_tmp.Add(new SortedVal());
            }

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (i == j) M[i, j] = 1.0;
                    else M[i, j] = 0.0;


            double sum_weights = 0;
            for (int i = 0; i < mu; i++)
            {
                weights[i] = Math.Pow(Math.Log(0.5 + (double)mu) - Math.Log((double)(1 + i)), 1.0);
                sum_weights = sum_weights + weights[i];
            }
            mueff = 0;
            for (int i = 0; i < mu; i++)
            {
                weights[i] = weights[i] / sum_weights;
                mueff = mueff + weights[i] * weights[i];
            }
            mueff = 1.0 / mueff;

            for (int i = 0; i < N; i++)
            {
                ps[i] = 0;
            }

            cs = (mueff + 2.0) / (N + mueff + 5.0);
            lcoef = 2.0;
            c1 = lcoef / (Math.Pow(N + 1.3, 2.0) + mueff);
            cmu = minv(1.0 - c1, lcoef * (mueff - 2.0 + 1.0 / mueff) / (Math.Pow(N + 2.0, 2.0) + mueff));

            if (initialMean.Length != dim)
                Debug.LogException(new Exception("Incorrect array length!"));
            Array.Copy(initialMean, xmean, xmean.Length);
            sigma = initialStepSize;
        
        }
        public int recommendedPopulationSize(int dim)
        {
            return 4 + (int)(3.0*Math.Log((double)dim));
        }

        public void generateSamples(OptimizationSample[] samples, int nInitialGuesses = 0)
        {
            int N = dim;
            if (samples.Length != lambda)
                Debug.LogError("Invalid sample array size");
            for (int i = 0; i < lambda; i++) // O(lambda*m*n)
            {
                if (i < nInitialGuesses)
                {
                    //If this sample is an initial guess, represent it as z
                    //Note that we currently assume that M is an identity matrix, i.e., this works only at first iteration
                    for (int k = 0; k < N; k++)	// O(n)
                    {
                        z[k] = (samples[i].x[k] - xmean[k])/sigma;
                    }
                }
                else
                {
                    for (int k = 0; k < N; k++)	// O(n)
                    {
                        z[k] = randNormal();
                    }
                }
                matrix_mult_vector(Mz, M, z, N);
                for (int k = 0; k < N; k++)	// O(n)
                {
                    arz[i][k] = z[k];
                    ard[i][k] = Mz[k];
                    samples[i].x[k] = xmean[k] + sigma * ard[i][k];
                }

            }

        }
        public double update(OptimizationSample[] samples)
        {
            int N = dim;
            double objMultiplier=1;
            if (mode==OptimizationModes.minimize)
                objMultiplier=-1;  //because the sorting order needs to be changed
            for (int i = 0; i < lambda; i++)
            {
                arr_tmp[i].id = i;
                arr_tmp[i].value = objMultiplier*samples[i].objectiveFuncVal;
            }
            arr_tmp.Sort();
            for (int i = 0; i < lambda; i++)
            {
                arindex[i] = arr_tmp[i].id;
                arfitness[i] = objMultiplier*arr_tmp[i].value;  
            }
            if ((mode == OptimizationModes.minimize && arfitness[0] < bestFitness)
                || (mode == OptimizationModes.maximize && arfitness[0] > bestFitness))
            {
                System.Array.Copy(samples[arindex[0]].x, xBest,N);
                bestFitness = arfitness[0];
            }

            for (int i = 0; i < mu; i++)
            {
                double[] cur_z = arz[arindex[i]];
                for (int j = 0; j < N; j++)
                    if (i == 0) zmean[j] = weights[i] * cur_z[j];
                    else zmean[j] += weights[i] * cur_z[j];

                double[] cur_d = ard[arindex[i]];
                for (int j = 0; j < N; j++)
                    xmean[j] += sigma * weights[i] * cur_d[j];
            }


            for (int i = 0; i < N; i++)
                ps[i] = (1.0 - cs) * ps[i] + Math.Sqrt(cs * (2 - cs) * mueff) * zmean[i];

            matrix_mult_vector(Mz, M, ps, N);

            double coef1 = (1.0 - 0.5 * c1 - 0.5 * cmu);
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    double val = 0;
                    for (int k = 0; k < mu; k++)
                        val += weights[k] * ard[arindex[k]][i];
                    M[i, j] *= coef1;
                    M[i, j] += 0.5 * ps[j] * (c1 * Mz[i] + cmu * val);
                }

            double norm_ps = 0;
            for (int i = 0; i < N; i++)
                norm_ps += ps[i] * ps[i];

            sigma = sigma * Math.Exp(cs * (norm_ps / N - 1.0));
            return arfitness[0];
        }
        public double[] getBest()
        {
            return xBest;
        }
        public double getBestObjectiveFuncValue()
        {
            return bestFitness;
        }
        public void optimize(Func<double[],int,double> objectiveFunc, int maxIter)
        {
            OptimizationSample[] samples = new OptimizationSample[lambda];
            for (int i = 0; i < maxIter; i++)
            {
                generateSamples(samples);
                for (int j=0; j<lambda; j++)
                    samples[i].objectiveFuncVal = testFunc(samples[i].x);
            }
        }
        float randNormal(float mean = 0, float sd = 1.0f)
        {
            //Box-Muller transform
            float u1 = (float)rng.NextDouble();
            float u2 = (float)rng.NextDouble();
            return mean + sd * Mathf.Sqrt(-2.0f * Mathf.Log(u1 + 1e-10f)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        }
        public class SortedVal : System.IComparable
        {
            public double value;
            public int id;
            public int CompareTo(object obj)
            {
                SortedVal other = obj as SortedVal;
                if (value > other.value)
                    return -1;
                else if (value == other.value)
                    return 0;
                return 1;
            }
        }
        public double square(double x)
        {
            return x * x;
        }
        public double testFunc(double[] x)
        {
            if (x.Length == 2)
            {
                double a = 1;
                double b = 100;
                double Rosenbrock = (a - x[0]) * (a - x[0]) + b * (x[1] - x[0] * x[0]) * (x[1] - x[0] * x[0]);
                return Rosenbrock; //convert from loss to fitness
            }
            else
            {
                double result = 0;
                for (int i = 0; i < x.Length-1; i++)
                {
                    result+=100.0*square(x[i+1]-square(x[i]))+square(x[i]-1);
                }
                return result;
            }
        }

        double minv(double a, double b)
        {
            if (a < b) return a;
            else return b;
        }
        // vector res = matrix a X vector b
        void matrix_mult_vector(double[] res, double[,] a, double[] b, int m)
        {
            double val = 0.0;
            for (int i = 0; i < m; i++)
            {
                val = 0.0;
                for (int j = 0; j < m; j++)
                    val += a[i, j] * b[j];
                res[i] = val;
            }
        }

        //Original MAES code by Ilya Loschilov, all in one function.
        void testMAES()
        {
            int N = 10;
            // set boring parameters
            double xmin = -5.0;						// x parameters lower bound
            double xmax = 5.0;						// x parameters upper bound
            int lambda = 4 + 3 * (int)Math.Log((double)N);	// population size, e.g., 4+floor(3*log(N));
            int mu = lambda / 2;					// number of parents, e.g., floor(lambda/2);
            double sigma = 0.3 * (xmax - xmin);		// initial step-size
            double target_f = 1e-10;				// target fitness function value, e.g., 1e-10
            double maxevals = 10000 * N;			// maximum number of function evaluations allowed, e.g., 1e+6
            double minsigma = 1e-20;				// stop if sigma is smaller than minsigma
            double chiN = Math.Sqrt((double)N) * (1.0 - 1.0 / (4.0 * N) + 1 / (21.0 * N * N));

            // memory allocation
            //ibit = 31;								// for the fast Rademacher sampling
            double[,] M = new double[N,N];
            double[,] Mmult = new double[N,N];

            List<double[]> arx = new List<double[]>();
            List<double[]> arz = new List<double[]>();
            List<double[]> ard = new List<double[]>();
            for (int i = 0; i < lambda; i++)
            {
                arx.Add(new double[N]);
                arz.Add(new double[N]);
                ard.Add(new double[N]);
            }
            double[] ps = new double[N];
            double[] xmean = new double[N];
            double[] zmean = new double[N];
            double[] xold = new double[N];
            double[] z = new double[N];
            double[] Mz = new double[N];
            double[] weights = new double[mu];
            double[] arfitness = new double[lambda];
            int[] arindex = new int[lambda];
            List<SortedVal> arr_tmp = new List<SortedVal>(lambda);
            for (int i = 0; i < lambda; i++)
            {
                arr_tmp.Add(new SortedVal());
            }

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (i == j) M[i, j] = 1.0;
                    else M[i,j] = 0.0;


            double sum_weights = 0;
            for (int i = 0; i < mu; i++)
            {
                weights[i] = Math.Pow(Math.Log(0.5 + (double)mu) - Math.Log((double)(1 + i)), 1.0);
                sum_weights = sum_weights + weights[i];
            }
            double mueff = 0;
            for (int i = 0; i < mu; i++)
            {
                weights[i] = weights[i] / sum_weights;
                mueff = mueff + weights[i] * weights[i];
            }
            mueff = 1.0 / mueff;

            for (int i = 0; i < N; i++)
            {
                ps[i] = 0;
                xmean[i] = xmin + (xmax - xmin) * rng.NextDouble();
            }

            double cs = (mueff + 2.0) / (N + mueff + 5.0);
            double lcoef = 2.0;
            double c1 = lcoef / (Math.Pow(N + 1.3, 2.0) + mueff);
            double cmu = minv(1.0 - c1, lcoef * (mueff - 2.0 + 1.0 / mueff) / (Math.Pow(N + 2.0, 2.0) + mueff));

            double counteval = 0;
            int stop = 0;
            int itr = 0;
            double BestF = double.PositiveInfinity;
            bool logging = true;
            if (logging)
            {
                Debug.Log("Initial value at " + xmean + ": " + testFunc(xmean));
                counteval += 1;
            }

            while (stop == 0)
            {
                for (int i = 0; i < lambda; i++) // O(lambda*m*n)
                {
                    for (int k = 0; k < N; k++)	// O(n)
                    {
                        z[k] = randNormal();
                    }
                    matrix_mult_vector(Mz, M, z, N);

                    for (int k = 0; k < N; k++)	// O(n)
                    {
                        arz[i][k] = z[k];
                        ard[i][k] = Mz[k];
                        arx[i][k] = xmean[k] + sigma * ard[i][k];
                    }

                    arfitness[i] = testFunc(arx[i]);
                    counteval = counteval + 1;
                    if (counteval == 1) BestF = arfitness[i];
                    if (arfitness[i] < BestF) BestF = arfitness[i];
                }

                for (int i = 0; i < lambda; i++)
                {
                    arr_tmp[i].id = i;
                    arr_tmp[i].value = arfitness[i];
                }
                arr_tmp.Sort();
                for (int i = 0; i < lambda; i++)
                {
                    arindex[i] = arr_tmp[i].id;
                    arfitness[i] = arr_tmp[i].value;
                }

                for (int i = 0; i < mu; i++)
                {
                    double[] cur_z = arz[arindex[i]];
                    for (int j = 0; j < N; j++)
                        if (i == 0) zmean[j] = weights[i] * cur_z[j];
                        else zmean[j] += weights[i] * cur_z[j];

                    double[] cur_d = ard[arindex[i]];
                    for (int j = 0; j < N; j++)
                        xmean[j] += sigma * weights[i] * cur_d[j];
                }


                for (int i = 0; i < N; i++)
                    ps[i] = (1.0 - cs) * ps[i] + Math.Sqrt(cs * (2 - cs) * mueff) * zmean[i];

                matrix_mult_vector(Mz, M, ps, N);

                double coef1 = (1.0 - 0.5 * c1 - 0.5 * cmu);
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                    {
                        double val = 0;
                        for (int k = 0; k < mu; k++)
                            val += weights[k] * ard[arindex[k]][i];
                        M[i, j] *= coef1;
                        M[i, j] += 0.5 * ps[j] * (c1 * Mz[i] + cmu * val);
                    }

                double norm_ps = 0;
                for (int i = 0; i < N; i++)
                    norm_ps += ps[i] * ps[i];

                sigma = sigma * Math.Exp(cs * (norm_ps / N - 1.0));

                if (arfitness[0] < target_f)
                    stop = 1;
                if (counteval >= maxevals)
                    stop = 1;
                itr = itr + 1;

                if (sigma < minsigma)
                    stop = 1;
                if (logging)
                {
                    Debug.Log("Evaluation " + counteval + " best fitness " + BestF);
                }
            }
        }  //test MA-ES

    }  //class LMMAES
} //namespace IGM