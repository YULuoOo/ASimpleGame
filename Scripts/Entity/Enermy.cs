using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : Entity {

    private Vector3[] movePoints;
    private float standTime = 0;
    private Transform target;
    private float chaseRadius = 6;
    private float attackRadius = 1.5f;
    private Player targetEntity;
    private float attackCoolTime = 1;
    private float curCoolTime = 0;

    public override void InitEntity(Vector3 pos, Vector3 dir)
    {
        base.InitEntity(pos, dir);
        totalHP = 40;
        curHP = totalHP;
        damageHP = 10;
        curCoolTime = attackCoolTime;
    }

    public void InitEnermy(Vector3[] points, Player player)
    {
        movePoints = points;
        target = player.mRoot;
        targetEntity = player;
        standTime = Random.Range(0, 10);
    }

    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        curCoolTime = 0;
        Invoke("attackDamage", 0.7f);
    }

    private void attackDamage()
    {
        if (curHP <= 0)
        {
            OnEnterDeath();
            return;
        }
        if (target == null)
        {
            return;
        }

        float dis = Vector3.Distance(mRoot.position, target.position);
        float targetAngle = Vector3.Angle(mRoot.forward.normalized,(target.position - mRoot.position).normalized); 
        if (dis < attackRadius && targetAngle < 30)
        {
            targetEntity.GetHurt(damageHP);
        }
        OnEnterIdle();
   }
    


    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            if (mState == EntityState.Run)
            {
                mNav.isStopped = true;
                OnEnterIdle();
            }
            return;
        }
        if (curCoolTime < attackCoolTime)
        {
            curCoolTime += Time.deltaTime;
        }

        if(    movePoints == null 
            || movePoints.Length == 0
            || mState == EntityState.Death 
           )
        {
            return;
        }
        if(target != null && mState != EntityState.Death && mState != EntityState.Hit)
        {
            float targetDis = Vector3.Distance(mRoot.position, target.position);
            if (targetDis <= attackRadius && curCoolTime >= attackCoolTime)
            {
                StartAttack();
                return;
            }
            if (targetDis <= chaseRadius && targetDis > attackRadius)
            {
                StartChase();
                return;
            }
        }
        if(standTime > 0)
        {
            standTime -= Time.deltaTime;
            return;
        }

        if (mState == EntityState.Idle && standTime <= 0)
        {
            StartMove();
            return;
        }
        if (mState == EntityState.Run && mNav.remainingDistance <= mNav.stoppingDistance)
        {
            StartIdle();
        }



    }

    private void Move(Vector3 targetPos)
    {
        CancelInvoke();
        mNav.isStopped = false;
        mRoot.LookAt(targetPos);
        mNav.SetDestination(targetPos);
        if(mState != EntityState.Run)
            OnEnterRun();
    }

    private void StartMove()
    {
        OnEnterRun();
        Vector3 targetPos = movePoints[Random.Range(0, movePoints.Length - 1)];
        float dis = Vector3.Distance(targetPos,mRoot.position);
        if(dis > 2)
        {
            Move(targetPos);
        }
    }
    private void StartIdle()
    {
        standTime = Random.Range(2, 10);
        mNav.isStopped = true;
        OnEnterIdle();
    }
    private void StartChase()
    {
       // OnEnterRun();
        Vector3 tarPos = target.position;
        Move(tarPos);
    }
    private void StartAttack()
    {
        OnEnterIdle();
        mRoot.LookAt(target);
        mNav.isStopped = true;
        OnEnterAttack1();
    }

    protected override void OnEnterDeath()
    {
        base.OnEnterDeath();
        EntityManager.Instance.removeEnermy(this);
        GameManager.Instance.OnKillEnemy();
    }

}
