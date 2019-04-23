/*
 * This is the human interface to the game
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    GameObject whiteBall;
    GameObject stick;
    public float aimForceMultiplier = 100;
    public bool previewTrajectories = true;
    [HideInInspector]
    public bool gameOver = false;
    [HideInInspector]
    public float score = 0;
    GameSystem gameSystem;
    public enum UIState
    {
        idle,aiming,simulating
    }
    UIState uiState = UIState.idle;

	// Use this for initialization
	void Start () {
		//cache game objects
        whiteBall = GameObject.Find("WhiteBall");
        stick = GameObject.Find("Stick");
        stick.SetActive(false);
        gameSystem = GameObject.FindObjectOfType<GameSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        //We do a different update based on current state
        if (uiState == UIState.idle)
        {
            //check whether player is clicking on the white ball to start aiming
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.name == "WhiteBall")
                    {
                        uiState = UIState.aiming;
                    }
                }
            }
        }
        else if (uiState == UIState.aiming)
        {

            //Compute the aim vector
            Plane plane=new Plane(Vector3.up, whiteBall.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enterDistance=0;
            plane.Raycast(ray, out enterDistance);
            Vector3 mouseOnPlane = ray.origin + enterDistance * ray.direction;
            Vector3 aimVector = whiteBall.transform.position - mouseOnPlane;

            //Visualize with stick
            stick.SetActive(true);
            stick.transform.position = mouseOnPlane;
            stick.transform.LookAt(mouseOnPlane+aimVector);

            //Preview trajectories
            if (previewTrajectories)
            {
                //must also disable autosimulation, as otherwise we get nasty jitter due to the imperfect physics state saving and loading
                Physics.autoSimulation = false;
                float score=gameSystem.evaluateShot(aimForceMultiplier * aimVector, Color.green);
                Debug.Log("Predicted shot score " + score);
            }
            //When mouse released, apply force to ball and go into simulation mode
            if (Input.GetMouseButtonUp(0))
            {
                gameSystem.shoot(aimForceMultiplier*aimVector);
                uiState = UIState.simulating;
                stick.SetActive(false);
            }
        }
        else if (uiState == UIState.simulating)
        {
            Physics.autoSimulation = true;
            //if all physics objects have stopped moving (i.e., their speeds are below the 0.001f threshold), go back to idle mode
            if (gameSystem.shotComplete())
            {
                uiState = UIState.idle;
                //just to be sure, fully stop all bodies
                gameSystem.stopAll();
            }
        }
	}

}
