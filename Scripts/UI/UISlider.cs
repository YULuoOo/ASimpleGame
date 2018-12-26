using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour {

    private Slider mSlider;
	// Use this for initialization
	void Start () {
        mSlider = transform.GetComponent<Slider>();
        mSlider.value = 1;
		
	}

    public void RefreshSlider(int curHP,int totalHP)
    {
        //Debug.Log(curHP);
        float value = (float)curHP / totalHP;
        mSlider.value = value;
    }
	
}
