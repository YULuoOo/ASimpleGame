using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {
    public float lifeTime;
    protected EffectScript mScript;
    public Transform mRoot;

    void Awake()
    {
        
    }

    public void InitEffect(string resName, Vector3 pos)
    {
        GameObject loadResObj = GameManager.Instance.LoadResources<GameObject>(resName);
        GameObject obj = GameObject.Instantiate(loadResObj);
        mRoot = obj.transform;
        mScript = obj.GetComponent<EffectScript>() as EffectScript;
        if(mScript == null)
        {
            Debug.LogError("特效未挂载");
            return;
        }
        lifeTime = mScript.lifeTime;
        mRoot.position = pos;

    }

    public void InitEffect(string resName, Vector3 pos,Transform parent)
    {
        InitEffect(resName, pos);
        mRoot.SetParent(parent);
        mRoot.transform.localPosition = Vector3.zero;
        mRoot.transform.rotation = Quaternion.identity;
    }

    public virtual void InitEffect(string resName, Vector3 pos, Transform parent,Vector3 targetPos)
    {
        InitEffect(resName, pos, parent);
    }

}
