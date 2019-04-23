using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 90.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 v = transform.right;
        v.y = 0;
        v = v.normalized;
        transform.position+=moveSpeed*Input.GetAxisRaw("Horizontal")*v;
        v = transform.forward;
        v.y = 0;
        v = v.normalized;
        transform.position+=moveSpeed * Input.GetAxisRaw("Vertical")*v;
        if (Input.GetKey(KeyCode.E))
            transform.position += moveSpeed*Vector3.up;
        if (Input.GetKey(KeyCode.Q))
            transform.position -= moveSpeed * Vector3.up;

        if (Input.GetMouseButton(1))
        {
            Vector3 currentTargetPoint = transform.position + transform.forward;
            currentTargetPoint += rotateSpeed * Input.GetAxis("Mouse X") * transform.right;
            currentTargetPoint += rotateSpeed * Input.GetAxis("Mouse Y") * Vector3.up;
            transform.LookAt(currentTargetPoint);
        }
	}
}
