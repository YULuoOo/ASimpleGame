using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform target;
    private float speed = 2;
    private Vector3 offsetPos;
    private Transform mRoot;


    public void InitCamera(Transform _target)
    {
        target = _target;
        offsetPos = transform.position - target.position;
    }
    void Awake()
    {
        Camera.main.fieldOfView = 80;
        mRoot = transform;
    }

	// Update is called once per frame
	void Update () {
        if(target == null)
        {
            return;
        }
        mRoot.position = Vector3.Lerp(mRoot.position, target.position + offsetPos,Time.deltaTime*speed);

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(Camera.main.fieldOfView <= 80)
            {
                Camera.main.fieldOfView += 5;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView >= 20)
            {
                Camera.main.fieldOfView -= 5;
            }
        }

    }
}
