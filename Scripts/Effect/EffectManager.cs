using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{

    private static EffectManager _Instance;
    public static EffectManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType(typeof(EffectManager)) as EffectManager;
                if (_Instance == null)
                {
                    GameObject obj = new GameObject();
                    _Instance = (EffectManager)obj.AddComponent<EffectManager>();
                }
            }
            return _Instance;
        }
    }

    public List<Effect> effectList = new List<Effect>();
    void Awake()
    {

    }



    public Effect CreateEffect(string resName, Vector3 pos)
    {
        Effect effect = new Effect();
        effect.InitEffect(resName, pos);
        effectList.Add(effect);
        return effect;
    }

    public Effect CreateEffect(string resName, Vector3 pos, Transform parent)
    {
        Effect effect = new Effect();
        effect.InitEffect(resName, pos, parent);
        effectList.Add(effect);
        return effect;
    }

    public Effect CreateEffect(string resName, Vector3 pos, Transform parent,Vector3 targetPos, int damage)
    {
        FlyEffect effect = new FlyEffect();
        effect.InitEffect(resName, pos, parent,targetPos);
        effect.SetDamage(damage);
        effectList.Add(effect);
        return effect;
    }


    void Update()
    {
        if (effectList.Count > 0)
        {
            for (int i = 0; i < effectList.Count; i++)
            {
                if (effectList[i] == null || effectList[i].lifeTime == GameDefines.skillFXForeverTime)
                {
                    continue;
                }
                effectList[i].lifeTime -= Time.deltaTime;               
                if (effectList[i].lifeTime <= 0)
                {
                    GameObject.Destroy(effectList[i].mRoot.gameObject);
                    effectList.Remove(effectList[i]);
                }
            }
        }
    }
}
