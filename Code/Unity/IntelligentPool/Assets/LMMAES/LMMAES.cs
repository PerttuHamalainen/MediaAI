/**
 * This file is part of the materials for the course "Intelligent Computational Media" (formerly: "Computational Intelligence in Games")
 * of Aalto University, Finland.
 *
 * You are free to use this code for any purpose, but at your own risk.
 * */


//TODO: initial guess handling: instead of sampling z from std. normal, use d=initial guess, z=inv(M)d
// => requires matrix inversion => need to integrate Math.NET. 
// In online use, might be better to just reinit the optimization if initial guess is best and much further from xmean than current step size (sigma)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ICM
{
    //this class would be in IGM namespace, except for the fact that it needs to inherit MonoBehaviour
    public class LMMAES : IMAES
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
        int nvectors;
        double bestFitness;

        List<double[]> arx = new List<double[]>();
        List<double[]> arz = new List<double[]>();
        List<double[]> ard = new List<double[]>();
        double[] ps;
        double[,] ps_arr;
        double[] xmean;
        double[] zmean;
        double[] xold;
        double[] z;
        double[] weights;
        double[] arfitness;
        double[] dz;
        double[] Az;
        double[] xBest;
        double mueff;

        int[] arindex;
        int iterator_sz;
        List<SortedVal> arr_tmp;
        double cs;
        public void init(int dim, int populationSize, double[] initialMean, float initialStepSize, OptimizationModes mode)
        {
            this.dim = dim;
            this.mode = mode;
            int N = dim;
            // set boring parameters
            lambda = populationSize;
            mu = lambda / 2;					// number of parents, e.g., floor(lambda/2);
            sigma = initialStepSize;		// initial step-size
            chiN = Math.Sqrt((double)N) * (1.0 - 1.0 / (4.0 * N) + 1 / (21.0 * N * N));

            // set interesting (to be tuned) parameters
            nvectors = 4 + 3 * (int)Math.Log((double)N);	// number of stored direction vectors, e.g., nvectors = 4+floor(3*log(N))
            cs = 2.0 * ((double)lambda) / N;		// nvectors;			

            // memory allocation
            //ibit = 31;								// for the fast Rademacher sampling

            arx = new List<double[]>();
            arz = new List<double[]>();
            for (int i = 0; i < lambda; i++)
            {
                arx.Add(new double[N]);
                arz.Add(new double[N]);
            }
            ps_arr = new double[nvectors, N];
            ps = new double[N];
            xmean = new double[N];
            xBest = new double[N];
            zmean = new double[N];
            xold = new double[N];
            z = new double[N];
            dz = new double[N];
            Az = new double[N];
            weights = new double[mu];
            arfitness = new double[lambda];
            arindex = new int[lambda];
            arr_tmp = new List<SortedVal>(lambda);
            for (int i = 0; i < lambda; i++)
            {
                arr_tmp.Add(new SortedVal());
            }
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
            if (N < 32)
                Debug.LogWarning("Warning: LM-MA-ES is not optimal for low-dimensional problems. You should use MA-ES instead");
                //With small N, LMMAES way of computing cs can result in negative or zero values.
                //In this case, we revert to how it's computed in CMA-ES and MA-ES.
                //However, this is a hack to prevent crashes, and one should rather use MA-ES than LM-MA-ES with small N.
                cs = (mueff + 2.0) / (N + mueff + 5.0);  


            for (int i = 0; i < N; i++)
            {
                ps[i] = 0;
            }
            if (initialMean.Length != dim)
                Debug.LogException(new Exception("Incorrect array length!"));
            Array.Copy(initialMean, xmean, xmean.Length);

            for (int i = 0; i < nvectors; i++)
                for (int j = 0; j < N; j++)
                    ps_arr[i, j] = 0;
            bestFitness = mode == OptimizationModes.minimize ? double.PositiveInfinity : double.NegativeInfinity;
            iterator_sz = 0;
        }
        public int recommendedPopulationSize(int dim)
        {
            return 4 + 3 * (int)Math.Log((double)dim);
        }
        public void generateSamples(OptimizationSample[] samples, int nInitialGuesses = 0)
        {
            if (nInitialGuesses > 0)
                Debug.LogError("Initial guesses not yet implemented in LM-MA-ES!");
            int N = dim;
            for (int i = 0; i < lambda; i++) // O(lambda*m*n)
            {
                for (int k = 0; k < N; k++)	// O(n)
                {
                    z[k] = randNormal();
                    Az[k] = z[k];
                }

                //double mcur = 4.0*fabs(random_Gauss(&gt.ttime));
                //if (mcur > iterator_sz)	mcur = iterator_sz;
                double mcur = iterator_sz;

                for (int k = 0; k < mcur; k++)
                {
                    double c1 = 1.0 / (N * (Math.Pow(1.5, (double)k)));
                    double ps_j_mult_z = 0;
                    for (int p = 0; p < N; p++)				// product vector times Az
                        ps_j_mult_z += ps_arr[k, p] * Az[p];
                    ps_j_mult_z = ps_j_mult_z * c1;
                    for (int p = 0; p < N; p++)
                        Az[p] = (1.0 - c1) * Az[p] + ps_arr[k, p] * ps_j_mult_z;
                }

                for (int k = 0; k < N; k++)	// O(n)
                {
                    samples[i].x[k] = xmean[k] + sigma * Az[k];
                    arz[i][k] = z[k];
                }
            }
        }
        public double update(OptimizationSample[] samples)
        {

            //sort samples based on fitness
            int N = dim;
            double objMultiplier = 1;
            if (mode == OptimizationModes.minimize)
                objMultiplier = -1;  //because the sorting order needs to be changed
            for (int i = 0; i < lambda; i++)
            {
                arr_tmp[i].id = i;
                arr_tmp[i].value = objMultiplier * samples[i].objectiveFuncVal;
            }
            arr_tmp.Sort();
            for (int i = 0; i < lambda; i++)
            {
                arindex[i] = arr_tmp[i].id;
                arfitness[i] = objMultiplier * arr_tmp[i].value;
            }
            if ((mode == OptimizationModes.minimize && arfitness[0] < bestFitness)
                || (mode == OptimizationModes.maximize && arfitness[0] > bestFitness))
            {
                System.Array.Copy(samples[arindex[0]].x, xBest, N);
                bestFitness = arfitness[0];
            }

            //Update LM-MA-ES state: sigma and sampling directions
            for (int i = 0; i < N; i++)
            {
                xold[i] = xmean[i];
                xmean[i] = 0;
                zmean[i] = 0;
            }

            for (int i = 0; i < mu; i++)
            {
                double[] cur_x = samples[arindex[i]].x;
                double[] cur_z = arz[arindex[i]];
                for (int j = 0; j < N; j++)
                {
                    xmean[j] += weights[i] * cur_x[j];
                    zmean[j] += weights[i] * cur_z[j];
                }
            }

            for (int i = 0; i < N; i++)
                ps[i] = (1.0 - cs) * ps[i] + Math.Sqrt(cs * (2 - cs) * mueff) * zmean[i];

            if (iterator_sz < nvectors)
                iterator_sz++;
            for (int i = 0; i < nvectors; i++)
            {
                double lr = (((double)lambda) / (double)N) / Math.Pow(4.0, (double)i);
                double alpha = (1.0 - lr);
                double beta = Math.Sqrt(lr * (2.0 - lr) * mueff);
                for (int j = 0; j < N; j++)
                    ps_arr[i, j] = alpha * ps_arr[i, j] + beta * zmean[j];
            }

            double norm_ps = 0;
            for (int i = 0; i < N; i++)
                norm_ps += ps[i] * ps[i];

            sigma = sigma * Math.Exp((cs / 1.0) * (norm_ps / N - 1.0));
            return arfitness[0];
        }

        public double[] getBest()
        {
            return xBest;
        }
        public double[] getMean()
        {
            return xmean;
        }
        public double getBestObjectiveFuncValue()
        {
            return bestFitness;
        }
        public void optimize(Func<double[], int, double> objectiveFunc, int maxIter)
        {
            OptimizationSample[] samples = new OptimizationSample[lambda];
            for (int i = 0; i < maxIter; i++)
            {
                generateSamples(samples);
                for (int j = 0; j < lambda; j++)
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

        //Original Loschilov code, everything in the same function, just converted from C++ to C#.
        //Other code in this class has been refactored from this to support separate init(), generateSamples() and update() calls
        void testLMMAES()
        {
            int N = 100;
            // set boring parameters
            double xmin = -5.0;						// x parameters lower bound
            double xmax = 5.0;						// x parameters upper bound
            int lambda = 4 + 3 * (int)Math.Log((double)N);	// population size, e.g., 4+floor(3*log(N));
            int mu = lambda / 2;					// number of parents, e.g., floor(lambda/2);
            double sigma = 0.3 * (xmax - xmin);		// initial step-size
            double target_f = 1e-10;				// target fitness function value, e.g., 1e-10
            double maxevals = 10000 * N;			// maximum number of function evaluations allowed, e.g., 1e+6
            bool sample_symmetry = true;				// 1 or 0, to sample symmetrical solutions to save 50% time and sometimes evaluations
            double minsigma = 1e-20;				// stop if sigma is smaller than minsigma
            double chiN = Math.Sqrt((double)N) * (1.0 - 1.0 / (4.0 * N) + 1 / (21.0 * N * N));

            // set interesting (to be tuned) parameters
            int nvectors = 4 + 3 * (int)Math.Log((double)N);	// number of stored direction vectors, e.g., nvectors = 4+floor(3*log(N))
            double cs = 2.0 * ((double)lambda) / N;		// nvectors;			

            // memory allocation
            //ibit = 31;								// for the fast Rademacher sampling

            List<double[]> arx = new List<double[]>();
            List<double[]> arz = new List<double[]>();
            for (int i = 0; i < lambda; i++)
            {
                arx.Add(new double[N]);
                arz.Add(new double[N]);
            }
            double[,] ps_arr = new double[nvectors, N];
            double[] ps = new double[N];
            double[] xmean = new double[N];
            double[] zmean = new double[N];
            double[] xold = new double[N];
            double[] z = new double[N];
            double[] dz = new double[N];
            double[] Az = new double[N];
            double[] weights = new double[mu];
            double[] arfitness = new double[lambda];
            int[] arindex = new int[lambda];
            List<SortedVal> arr_tmp = new List<SortedVal>(lambda);
            for (int i = 0; i < lambda; i++)
            {
                arr_tmp.Add(new SortedVal());
            }
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
            if (N<32)
                cs = (mueff + 2.0) / (N + mueff + 5.0);  //the paper says this for CMA-ES and MA-ES

            for (int i = 0; i < N; i++)
            {
                ps[i] = 0;
                xmean[i] = xmin + (xmax - xmin) * rng.NextDouble();
            }

            for (int i = 0; i < nvectors; i++)
                for (int j = 0; j < N; j++)
                    ps_arr[i, j] = 0;

            double counteval = 0;
            int iterator_sz = 0;
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
                int sign = 1;
                for (int i = 0; i < lambda; i++) // O(lambda*m*n)
                {
                    if (sign == 1)	// if sign==1, then sample new solution, otherwise use its mirror version with sign=-1
                    {
                        for (int k = 0; k < N; k++)	// O(n)
                        {
                            z[k] = randNormal();
                            Az[k] = z[k];
                        }

                        //double mcur = 4.0*fabs(random_Gauss(&gt.ttime));
                        //if (mcur > iterator_sz)	mcur = iterator_sz;
                        double mcur = iterator_sz;

                        for (int k = 0; k < mcur; k++)
                        {
                            double c1 = 1.0 / (N * (Math.Pow(1.5, (double)k)));
                            double ps_j_mult_z = 0;
                            for (int p = 0; p < N; p++)				// product vector times Az
                                ps_j_mult_z += ps_arr[k, p] * Az[p];
                            ps_j_mult_z = ps_j_mult_z * c1;
                            for (int p = 0; p < N; p++)
                                Az[p] = (1.0 - c1) * Az[p] + ps_arr[k, p] * ps_j_mult_z;
                        }
                    }

                    for (int k = 0; k < N; k++)	// O(n)
                    {
                        arx[i][k] = xmean[k] + sign * sigma * Az[k];
                        arz[i][k] = sign * z[k];
                    }
                    if (sample_symmetry) // sample in the opposite direction, seems to work better in most cases AND decreases the CPU cost of the sampling by 2.0
                        sign = -sign;

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
                    arindex[i]=arr_tmp[i].id;
                    arfitness[i]=arr_tmp[i].value;
                }


                for (int i = 0; i < N; i++)
                {
                    xold[i] = xmean[i];
                    xmean[i] = 0;
                    zmean[i] = 0;
                }

                for (int i = 0; i < mu; i++)
                {
                    double[] cur_x = arx[arindex[i]];
                    double[] cur_z = arz[arindex[i]];
                    for (int j = 0; j < N; j++)
                    {
                        xmean[j] += weights[i] * cur_x[j];
                        zmean[j] += weights[i] * cur_z[j];
                    }
                }

                for (int i = 0; i < N; i++)
                    ps[i] = (1.0 - cs) * ps[i] + Math.Sqrt(cs * (2 - cs) * mueff) * zmean[i];

                if (iterator_sz < nvectors)
                    iterator_sz = itr + 1;
                for (int i = 0; i < nvectors; i++)
                {
                    double lr = (((double)lambda) / (double)N) / Math.Pow(4.0, (double)i);
                    double alpha = (1.0 - lr);
                    double beta = Math.Sqrt(lr * (2.0 - lr) * mueff);
                    for (int j = 0; j < N; j++)
                        ps_arr[i, j] = alpha * ps_arr[i, j] + beta * zmean[j];
                }

                double norm_ps = 0;
                for (int i = 0; i < N; i++)
                    norm_ps += ps[i] * ps[i];

                sigma = sigma * Math.Exp((cs / 1.0) * (norm_ps / N - 1.0));

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
        }  //test LM-MA-ES
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
    }  //class LMMAES
} //namespace IGM