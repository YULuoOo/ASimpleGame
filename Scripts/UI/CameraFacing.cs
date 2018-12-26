using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour {

    private Camera refCamera;
    //public int a = 1;
    public bool isFacing = false;
    private Transform mRoot;
	
	void Awake () {
		if(!refCamera)
        {
            refCamera = Camera.main;
        }
        mRoot = transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = mRoot.position + refCamera.transform.rotation * (isFacing ? Vector3.back : Vector3.forward);
        Vector3 targetOrientation = refCamera.transform.rotation * Vector3.up;
        mRoot.LookAt(targetPos,targetOrientation);
	}
}
