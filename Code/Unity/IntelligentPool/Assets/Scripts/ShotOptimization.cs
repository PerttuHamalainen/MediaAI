using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICM;
using AaltoGames;

public class ShotOptimization : MonoBehaviour {
    public enum UIState { idle, optimizing };
    UIState uiState = UIState.idle;
    //initial shot parameters
    public float[] x = new float[2] { 0, 0 };
    GameSystem gameSystem;
    public int populationSize=16;
    public float forceMultiplier = 100;
    public int maxIter = 50;
    int iter = 0;
    OptimizationSample [] samples;
    MAES opt = new MAES();
    // Use this for initialization
	void Start () {
        gameSystem = FindObjectOfType<GameSystem>();
	}
    //Convert optimized variables to the force vector passed to the game system
    Vector3 paramsToForceVector(double[] x)
    {
        Vector3 force = forceMultiplier * (new Vector3((float)x[0], 0, (float)x[1]));
        const float maxSpeed = 10;
        if (force.magnitude > maxSpeed)
            force = maxSpeed * force.normalized;
        return force;
    }
	// Update is called once per frame
	void Update () {
        if (uiState == UIState.optimizing)
        {
            opt.generateSamples(samples);
            //TODO: generate multiple copies of the scene such that the evaluations can be parallelized by Unity
            foreach (OptimizationSample s in samples)
            {
                float shotScore = gameSystem.evaluateShot(paramsToForceVector(s.x),Color.grey);
                s.objectiveFuncVal = shotScore;
            }
            opt.update(samples);
            Debug.Log("Best shot score " + opt.getBestObjectiveFuncValue());
            gameSystem.evaluateShot(paramsToForceVector(opt.getBest()), Color.green);
            iter++;
            if (iter >= maxIter)
            {
                endOptimization();
            }
        }
	}
    void endOptimization()
    {
        uiState = UIState.idle;
        gameSystem.shoot(paramsToForceVector(opt.getBest()));
        Physics.autoSimulation = true;
    }
    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (uiState == UIState.idle)
        {
            if (GUILayout.Button("Optimize",GUILayout.Width (100)))
            {
                //init optimization
                samples = new OptimizationSample[populationSize];
                for (int i = 0; i < populationSize; i++)
                    samples[i] = new OptimizationSample(2);
                opt.init(2, populationSize, new double[2] { 0, 0 }, 1, OptimizationModes.maximize);
                iter = 0;
                //update state
                uiState = UIState.optimizing;
                Physics.autoSimulation = false;
            }
        }
        else
        {
            if (GUILayout.Button("Shoot", GUILayout.Width(100)))
            {
                endOptimization();
            }
        }
        gameSystem.rewardShaping = GUILayout.Toggle(gameSystem.rewardShaping, "Reward Shaping", GUILayout.Width(200));
        GUILayout.Label("Predicted score: " + gameSystem.predictedShotScore);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Max Iter:",GUILayout.Width(100));
        maxIter=(int)GUILayout.HorizontalSlider(maxIter,1,50,GUILayout.Width(50));
        GUILayout.Label(maxIter.ToString(),GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Population size:", GUILayout.Width(100));
        populationSize = (int)GUILayout.HorizontalSlider(populationSize, 1, 100, GUILayout.Width(50));
        GUILayout.Label(populationSize.ToString());

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

    }

}
