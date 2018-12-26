using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class FlyEffect : Effect {
    private int damage;

    public override void InitEffect(string resName, Vector3 pos, Transform parent, Vector3 targetPos)
    {
        base.InitEffect(resName, pos, parent, targetPos);
        mScript.SetColliderCallback(OnColliderHandler);
        Move(targetPos);
    }

    private void Move(Vector3 targetPos)
    {
        HOTween.Init();
        TweenParms parm = new TweenParms();
        parm.Prop("position", targetPos);
        parm.Ease(EaseType.Linear);
        parm.OnComplete(OnArrive);
        HOTween.To(mRoot, 2, parm);

    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    private void OnArrive()
    {
        lifeTime = 0;
    }

    private void OnColliderHandler(Transform other)
    {
        EffectManager.Instance.CreateEffect(GameDefines.archerSkillHit, mRoot.position);
        Enermy enermy = other.GetComponent<Enermy>();
        enermy.GetHurt(damage);
        lifeTime = 0;
    }
}
