using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AaltoGames;

public class GameSystem : MonoBehaviour {
    GameObject whiteBall;
    public float score = 0;
    List<Rigidbody> ballsToPocket;
    Vector3[] pocketPositions;
    public bool rewardShaping = true;
    public float physicsDrag = 0.5f;
    [HideInInspector]
    public float predictedShotScore = 0;
    // Use this for initialization
    void Start()
    {
        //cache game objects
        whiteBall = GameObject.Find("WhiteBall");
        //initialize reward shaping, need quick access to all balls that need to be pocketed, as well as all pocket positions
        ballsToPocket = new List<Rigidbody>();
        Rigidbody[] bodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody b in bodies)
        {
            if (b.name != "WhiteBall")
                ballsToPocket.Add(b);
            b.angularDrag = physicsDrag;
            b.drag = physicsDrag;
        }
        GameObject[] pockets = GameObject.FindGameObjectsWithTag("Pocket");
        pocketPositions = new Vector3[pockets.Length];
        for (int i = 0; i < pockets.Length; i++)
            pocketPositions[i] = pockets[i].transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void shoot(Vector3 force)
    {
        Rigidbody r = whiteBall.GetComponent<Rigidbody>();
        r.velocity = force;
        //Uncomment the following to also set the angular velocity of the ball such that it agrees with velocity.
        //This should prevent the ball from first sliding and then decelerating rapidly. However, it doesn't seem to work in practice,
        //and better results are obtained simply by having everything with zero friction and basically no rotation (which however looks ok only without textures...)
        //Vector3 axis = Vector3.Cross(Vector3.up, force.normalized).normalized; //this is the axis of rotation
        //float ballRadius = whiteBall.transform.localScale.x * 0.5f;
        //r.angularVelocity = 10.0f*axis * force.magnitude / (ballRadius * 2.0f * Mathf.PI);
    }
    public void onPocket(GameObject ball)
    {
        if (ball == whiteBall)
        {
            score -= 10;
        }
        else
        {
            score += 1;
        }
        ball.SetActive(false);
    }
    public bool shotComplete()
    {
        //check whether any physics object is moving
        Rigidbody[] bodies = GameObject.FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody b in bodies)
        {
            if (b.velocity.magnitude > 0.001f)
                return false;
        }
        return true;
    }
    public void stopAll()
    {
        Rigidbody[] bodies = GameObject.FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody b in bodies)
        {
            b.velocity = Vector3.zero;
        }
    }
    PhysicsStorage storage = new PhysicsStorage();
    float savedScore;
    public void saveState()
    {
        storage.saveState();
        savedScore = score;
    }
    public void restoreState()
    {
        storage.restoreState();
        score = savedScore;
    }
    public float evaluateShot(Vector3 force, Color drawColor, int maxSteps=1000)
    {
        //Disable autosimulation, the manual simulation will otherwise have no effect
        bool oldAutoSimulation = Physics.autoSimulation;
        Physics.autoSimulation = false;

        //save state, we don't want this preview simulation to have any effect after it's done
        saveState();

        //initialize score to 0, want to count only this shot
        score = 0;

        //initialize shot
        shoot(force);

        //some helpers
        Rigidbody[] bodies = FindObjectsOfType<Rigidbody>();
        Vector3[] pos = new Vector3[bodies.Length];

        //forward simulation loop
        bool complete;
        for (int step=0; step<maxSteps; step++)
        {
            //simulate and visualize all moving bodies
            for (int i = 0; i < pos.Length; i++)
                pos[i] = bodies[i].position;
            Physics.Simulate(Time.fixedDeltaTime);
            for (int i = 0; i < pos.Length; i++)
                if (bodies[i].gameObject.activeSelf)
                    //Debug.DrawLine(pos[i], bodies[i].position,Color.green);
                    GraphUtils.AddLine(pos[i], bodies[i].position, drawColor);

            //check whether movement stopped, exit early if yes
            complete = true;
            foreach (Rigidbody b in bodies)
            {
                if (b.velocity.magnitude > 0.01f)
                {
                    complete = false;
                    break;
                }
            }
            if (complete)
                break;
        }
        if (rewardShaping)
        {
            //Since the score as such provides very little gradient, we add a small score if the balls get close to the pockets
            foreach (Rigidbody b in ballsToPocket)
            {
                Vector3 ballPos = b.position;
                float minSqDist = float.MaxValue;
                for (int i = 0; i < pocketPositions.Length; i++)
                {
                    minSqDist = Mathf.Min(minSqDist, (pocketPositions[i] - ballPos).sqrMagnitude);
                }
                //each ball that is close to a pocket adds 0.1 to the score
                float distanceSd = 0.5f;
                score += 0.1f * Mathf.Exp(-0.5f * minSqDist / (distanceSd * distanceSd));
            }
        }

        //restore state and return the score for this shot
        float resultScore = score;
        restoreState();
        Physics.autoSimulation = oldAutoSimulation;
        predictedShotScore = resultScore;
        return resultScore;  
    }
}
