using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EffectScript : MonoBehaviour {
    public float lifeTime;
    public CallBack<Transform> callback;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetColliderCallback(CallBack<Transform> _callback)
    {
        callback = _callback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enermy"))
        {
            callback(other.transform);
        }
    }
}
