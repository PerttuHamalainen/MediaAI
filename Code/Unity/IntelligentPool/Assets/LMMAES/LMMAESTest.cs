using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICM;

public class LMMAESTest : MonoBehaviour {
    LMMAES opt = new LMMAES();
    public int nVariables = 2;
    int iter=0;
    OptimizationSample[] samples;
	void Start () {
        //Init optimization
        opt.init(nVariables, opt.recommendedPopulationSize(nVariables), new double[nVariables], 1, OptimizationModes.minimize);
        //allocate container for sample vectors
        samples = new OptimizationSample[opt.recommendedPopulationSize(nVariables)];
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = new OptimizationSample(nVariables);
        }
	}
    double squared(double x)
    {
        return x*x;
    }
    //Objective function to minimize
    double rosenbrock(double[] x)
    {
        double result = 0;
        for (int i = 0; i < nVariables - 1; i++)
        {
            result += 100.0 * squared((x[i + 1] - x[i] * x[i])) + squared(1.0 - x[i]);
        }
        return result;
    }

    //Run one optimization iteration per update
    void Update()
    {
        //sample
        opt.generateSamples(samples);
        //compute objective function value for each sample
        foreach (OptimizationSample s in samples)
        {
            s.objectiveFuncVal = rosenbrock(s.x);
        }
        //update the sampling distribution based on the objective function values and generated samples
        opt.update(samples);
        //report results
        Debug.Log("Iteration " + iter + " f(x)=" + opt.getBestObjectiveFuncValue());
        iter++;
	}
}
