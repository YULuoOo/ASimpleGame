using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Entity {

    public float attackCoolTime = 1;
    public float skillCoolTime = 3;
    private MainPanel skillUI;

    public override void InitEntity(Vector3 pos, Vector3 dir)
    {
        base.InitEntity(pos, dir);
        Camera.main.GetComponent<CameraFollow>().InitCamera(transform);
        InitSkillUI();
        totalHP = 100;
        curHP = totalHP;
        damageHP = 10;
    }

    private void InitSkillUI()
    {
        skillUI = FindObjectOfType<MainPanel>();
       
        skillUI.InitSkillTime(attackCoolTime, skillCoolTime);
    }

    protected override void OnEnterAttack1()
    {
        base.OnEnterAttack1();
        skillUI.OnPlayerUseSkill(mState);
    }

    protected override void OnEnterAttack2()
    {
        base.OnEnterAttack2();
        skillUI.OnPlayerUseSkill(mState);
    }

    void Update () {
        if(GameManager.Instance.isGameOver)
        {
            if(mState == EntityState.Run)
            {
                mNav.isStopped = true;
                OnEnterIdle();
            }
            return;
        }

        if(mState == EntityState.Death)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (CheckPress(Input.mousePosition))
            {

            }
        }

        else if (Input.GetMouseButtonDown(1) && skillUI.IsAttackOK())
        {
          
            if (TurnTo(Input.mousePosition))
            {
                OnEnterAttack1();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && skillUI.IsSkillOK())
        {

            if (TurnTo(Input.mousePosition))
            {
                OnEnterAttack2();
            }
            
        }

        if (mState ==EntityState.Run && mNav.remainingDistance == 0)
        {
            mNav.isStopped = true;
            OnEnterIdle();
        }
	}

    RaycastHit hit;
    private bool TurnTo(Vector3 inputPos)
    {
       
        Ray ray = Camera.main.ScreenPointToRay(inputPos);
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Terrain") || hit.collider.CompareTag("Enermy"))
            {

                mNav.isStopped = true;
                mRoot.LookAt(hit.point);
                
                return true;
            }
        }
        return false;

    }

    private bool CheckPress(Vector3 inputPos)
    {

        Ray ray = Camera.main.ScreenPointToRay(inputPos);
        //Debug.Log("aa");
        Debug.DrawLine(ray.origin, hit.point, Color.red, 2);
        if(Physics.Raycast(ray,out hit))
        {
            if(hit.collider.CompareTag("Terrain") )
            {
            
                Move(hit.point);
               // Debug.Log("bb");
                return true;
            }
        }
        return false;
    }

    private void Move(Vector3 targetPos)
    {
        mNav.isStopped = false;
        mRoot.LookAt(targetPos);
        OnEnterRun();
        mNav.SetDestination(targetPos);
   
    }

    protected override void OnEnterDeath()
    {
        base.OnEnterDeath();
        GameManager.Instance.isGameOver = true;
        Messenger.Broadcast<bool>(GameEvent.GameOver, false);
    }
}
