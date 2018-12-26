using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class DamageNum : MonoBehaviour {

    private Text damageText;
	// Use this for initialization
	void Awake () {
        damageText = transform.GetComponent<Text>();
		
	}

    public void InitDamageNum(int damage)
    {
        damageText.text = damage.ToString();
        HOTween.Init();
        TweenParms parm = new TweenParms();
        float y = transform.position.y;
        parm.Prop("position", new PlugVector3Y(y + 1));
        parm.Ease(EaseType.Linear);
        parm.OnComplete(DestroySelf);
        HOTween.To(transform, 1, parm);
    }

    private void DestroySelf()
    {
        GameObject.Destroy(gameObject);
    }

 
	
}
