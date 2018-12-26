using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum attackType
{
    attack,
    skill,
}

public enum EntityState
{
    Idle,
    Run,
    Hit,
    Attack1,
    Attack2,
    Death,
}

public class Entity : MonoBehaviour
{

    protected Animator animator;
    public Transform mRoot;
    public EntityState mState;
    public NavMeshAgent mNav;

    protected Transform topPoint;
    protected Transform middlePoint;
    protected Transform bottomPoint;

    public int totalHP;
    public int curHP;
    protected int damageHP;
    protected UISlider slider;

    public virtual void InitEntity(Vector3 pos, Vector3 dir)
    {
        mRoot.position = pos;
        mRoot.rotation = Quaternion.LookRotation(dir);
        OnEnterIdle();

        topPoint = mRoot.Find("Point/Top");
        middlePoint = mRoot.Find("Point/Middle");
        bottomPoint = mRoot.Find("Point/Bottom");
        CreateHPSlider();


    }

    protected void CreateHPSlider()
    {
        GameObject objLoaded = GameManager.Instance.LoadResources<GameObject>(GameDefines.UISlider);
        GameObject obj = GameObject.Instantiate(objLoaded) as GameObject;
        obj.transform.SetParent(topPoint);
        obj.transform.localPosition = Vector3.zero;
        slider = obj.GetComponent<UISlider>();
        if(slider == null)
        {
            slider = obj.AddComponent<UISlider>();
        }
    }

    protected void RefreshHPSlider()
    {
        slider.RefreshSlider(curHP, totalHP);
    }

    // Use this for initialization
    void Awake()
    {
        mRoot = transform;
        animator = mRoot.GetComponent<Animator>() as Animator;
        mNav = mRoot.GetComponent<NavMeshAgent>() as NavMeshAgent;
    }


    public void PlayAnimation(string animName)
    {
        if (animName == string.Empty)
        {
            Debug.Log("传入动作空" + animName);
            return;
        }
        animator.CrossFade(animName,0.10f);
    }

    protected void OnEnterIdle()
    {
        mState = EntityState.Idle;
        PlayAnimation(GameDefines.animIdle);
    }
    protected void OnEnterRun()
    {
        mState = EntityState.Run;
        PlayAnimation(GameDefines.animRun);
    }
    protected void OnEnterHit()
    {
        mNav.isStopped = true;
        mState = EntityState.Hit;
        PlayAnimation(GameDefines.animHit);
        Invoke("OnEnterIdle", 1);
    }
    protected virtual void OnEnterAttack1()
    {
        mNav.isStopped = true;
        mState = EntityState.Attack1;
        PlayAnimation(GameDefines.animAttack1);
    }
    protected virtual void OnEnterAttack2()
    {
        mNav.isStopped = true;
        mState = EntityState.Attack2;
        PlayAnimation(GameDefines.animAttack2);
    }
    protected virtual void OnEnterDeath()
    {
        mNav.isStopped = true;
        CancelInvoke();
        mState = EntityState.Death;
        PlayAnimation(GameDefines.animDeath);
        Invoke("DestroySelf", 2.0f);
    }

    public virtual void DestroySelf()
    {
        GameObject.Destroy(mRoot.gameObject);
    }

    public void GetHurt(int damage)
    {
        //Debug.Log(1);
        if (GameManager.Instance.isGameOver || mState == EntityState.Death)
        {
            return;
        }
        // Debug.Log(curHP);
        curHP -= damage;
        CreateDamageNum(damage);
        RefreshHPSlider();
        if(curHP <= 0)
        {
            OnEnterDeath();
        }
        else
        {
            OnEnterHit();
        }
    }

    private void CreateDamageNum( int damage)
    {
        GameObject objLoaded = GameManager.Instance.LoadResources<GameObject>(GameDefines.UIDamageNum);
        GameObject obj = GameObject.Instantiate(objLoaded) as GameObject;
        obj.transform.SetParent(topPoint);
        obj.transform.localPosition = Vector3.zero;
        DamageNum damageText = obj.GetComponent<DamageNum>();
        if (damageText == null)
        {
            damageText = obj.AddComponent<DamageNum>();
        }
        damageText.InitDamageNum(damage * -1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
