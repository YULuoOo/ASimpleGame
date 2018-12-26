using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player {
    private float attackRadius = 10;
    private Quaternion curRotation;

    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        curRotation = mRoot.rotation;
        Invoke("PlayAttack1Effect", 0.4f);
    }

    private void PlayAttack1Effect()
    {
        mRoot.rotation = curRotation;
        Vector3 targetPos = mRoot.position + attackRadius * mRoot.forward.normalized;
        EffectManager.Instance.CreateEffect(GameDefines.archerSkill1,Vector3.zero,middlePoint,targetPos,damageHP);
    }

    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();

        curRotation = mRoot.rotation;
        Invoke("PlayAttack2Effect", 0.6f);
    }
    private void PlayAttack2Effect()
    {
        mRoot.rotation = curRotation;
        Vector3 targetPos1 = mRoot.position + attackRadius * mRoot.forward.normalized;
        float middleAngle = Vector3.Angle(mRoot.forward, Vector3.right);
        middleAngle = mRoot.forward.z > 0 ? middleAngle : -middleAngle;

        float x2 = mRoot.position.x + attackRadius * Mathf.Cos((middleAngle + 30) * Mathf.Deg2Rad);
        float z2 = mRoot.position.z + attackRadius * Mathf.Sin((middleAngle + 30) * Mathf.Deg2Rad);
        float x3 = mRoot.position.x + attackRadius * Mathf.Cos((middleAngle - 30) * Mathf.Deg2Rad);
        float z3 = mRoot.position.z + attackRadius * Mathf.Cos((middleAngle - 30) * Mathf.Deg2Rad);
        Vector3 targetPos2 = new Vector3(x2, targetPos1.y, z2);
        Vector3 targetPos3 = new Vector3(x3, targetPos1.y, z3);

        EffectManager.Instance.CreateEffect(GameDefines.archerSkill2, Vector3.zero, middlePoint, targetPos1, damageHP);
        EffectManager.Instance.CreateEffect(GameDefines.archerSkill2, Vector3.zero, middlePoint, targetPos2, damageHP);
        EffectManager.Instance.CreateEffect(GameDefines.archerSkill2, Vector3.zero, middlePoint, targetPos3, damageHP);
    }
}
