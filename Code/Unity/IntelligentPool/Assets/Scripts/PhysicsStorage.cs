using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsStorage
{
    class RigidbodyState
    {
        public Rigidbody b;
        public Vector3 pos, vel, aVel;
        public Quaternion q;
        public bool active;
    }
    List<RigidbodyState> bodyStates = new List<RigidbodyState>();
    public void saveState()
    {
        Rigidbody[] bodies = GameObject.FindObjectsOfType<Rigidbody>();
        bodyStates.Clear();
        foreach (Rigidbody b in bodies)
        {
            RigidbodyState state=new RigidbodyState();
            state.b = b;
            state.pos=b.position;
            state.vel=b.velocity;
            state.aVel=b.angularVelocity;
            state.q=b.rotation;
            state.active = b.gameObject.activeSelf;
            bodyStates.Add(state);
        }
    }
    public void restoreState()
    {
        foreach (RigidbodyState state in bodyStates)
        {
            state.b.gameObject.SetActive(state.active);
            state.b.position=state.pos;
            state.b.velocity=state.vel;
            state.b.angularVelocity=state.aVel;
            state.b.rotation=state.q;
  
            state.b.transform.position = state.pos;  
            state.b.transform.rotation = state.q;
        }        
    }
    //Use this if you want restoreState() have immediate effect on Unity's transforms. Otherwise, they only get updated after the next physics step.
    public void restoreTransforms()
    {
        foreach (RigidbodyState state in bodyStates)
        {
            state.b.transform.position = state.pos;
            state.b.transform.rotation = state.q;
        }
    }
}

