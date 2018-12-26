using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player {

    private float attackRadius = 2;

    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        Invoke("PlayerAttack1Effect", 0.4f);
        
    }

    private void PlayerAttack1Effect()

    {
        attackDamage(attackType.attack);
        EffectManager.Instance.CreateEffect(GameDefines.warriorSkill1, mRoot.position, middlePoint);
    }

    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();
        Invoke("PlayerAttack2Effect", 0.5f);

    }

    private void PlayerAttack2Effect()
    {
        attackDamage(attackType.skill);
        EffectManager.Instance.CreateEffect(GameDefines.warriorSkill2, mRoot.position, bottomPoint);
    }

    private void attackDamage(attackType type)
    {
        List<Enermy> enermyList = EntityManager.Instance.enermyList;
        for(int i= 0;i<enermyList.Count;i++)
        {
            if (enermyList[i] == null)
            {
                continue;
            }
            float dis = Vector3.Distance(mRoot.position, enermyList[i].mRoot.position);
            if(dis < attackRadius)
            {
                if(type == attackType.attack)
                {
                    Vector3 forward = transform.forward.normalized;
                    Vector3 toOther = enermyList[i].mRoot.position - mRoot.position;
                    if(Vector3.Dot(forward,toOther)>0)
                    {
                        enermyList[i].GetHurt(damageHP);
                       //Debug.Log(1);
                    }
                }
                else if(type == attackType.skill)
                {
                    enermyList[i].GetHurt(damageHP);
                   // Debug.Log(1);
                }
            }
        }
    }
}
