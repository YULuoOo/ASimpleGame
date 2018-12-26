using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

    private Image attackImage;
    private Image skillImage;
    private Transform mRoot;
    private float attackCoolTime;
    private float skillCoolTime;
    private float curAttackTime;
    private float curSkillTime;
    private float originLastTime = 60;
    private float lastTime = 60;
    private int HP;
    private Text timeText;
    private Text KillCountText;
    private Text HPText;
    private Player player;

    // Use this for initialization
    void Awake () {
        mRoot = transform;
        attackImage = mRoot.Find("Attack").GetComponent<Image>();
        skillImage = mRoot.Find("Skill").GetComponent<Image>();
        timeText = mRoot.Find("LastTime/Value").GetComponent<Text>();
        KillCountText = mRoot.Find("KillCount/Value").GetComponent<Text>();
        HPText = mRoot.Find("HP/Value").GetComponent<Text>();
        //player = GameObject.FindObjectOfType<Player>();
 
        timeText.text = "00:00";
        KillCountText.text = "0";
        HPText.text = "0";
       // HP = player.curHP;
    }
    void OnEnable()
    {
        Messenger.AddListener<bool>(GameEvent.GameOver, OnGameOver);
        Messenger.AddListener<int>(GameEvent.KillEnemy, OnEnemyKilled);
    }
    void OnDisable()
    {
        Messenger.RemoveListener<bool>(GameEvent.GameOver, OnGameOver);
        Messenger.RemoveListener<int>(GameEvent.KillEnemy, OnEnemyKilled);
    }

    private void OnEnemyKilled(int curKillNumber)
    {
        KillCountText.text = curKillNumber.ToString();
    }

    private void OnGameOver(bool isWin)
    {
        GameObject loadedObj = GameManager.Instance.LoadResources<GameObject>(GameDefines.UIGameOver);
        GameObject gameoverPanel = GameObject.Instantiate(loadedObj);
        RectTransform gameoverRectTransform = gameoverPanel.GetComponent<RectTransform>();
        gameoverRectTransform.SetParent(mRoot.GetComponent<RectTransform>());
        gameoverRectTransform.transform.localPosition = Vector3.zero;
        gameoverRectTransform.transform.localScale = Vector3.one;
        GameOverPanel panel = gameoverRectTransform.GetComponent<GameOverPanel>();
        if (panel == null)
        {
            panel = gameoverRectTransform.gameObject.AddComponent<GameOverPanel>();
        }
        panel.InitPanel(isWin, GameManager.Instance.curKillCount, (int)(originLastTime - lastTime));
    }


    public void InitSkillTime(float _normalTime, float _skillTime)
    {
        attackCoolTime = _normalTime;
        skillCoolTime = _skillTime;
        curAttackTime = attackCoolTime;
        curSkillTime = skillCoolTime;
        attackImage.fillAmount = curAttackTime / attackCoolTime;
        skillImage.fillAmount = curSkillTime / skillCoolTime;
    }

    public void OnPlayerUseSkill(EntityState state)
    {
        if(state == EntityState.Attack1)
        {
            curAttackTime = 0;
        }
        if(state == EntityState.Attack2)
        {
            curSkillTime = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.isGameOver)
        {
            return;
        }
        if(player == null)
            player = GameObject.FindObjectOfType<Player>();
        HP = player.curHP;
        HPText.text= HP.ToString();
        if (curAttackTime <= attackCoolTime)
        {
            curAttackTime += Time.deltaTime;
            attackImage.fillAmount = curAttackTime / attackCoolTime;
        }

        if(curSkillTime <= skillCoolTime)
        {
            curSkillTime += Time.deltaTime;
            skillImage.fillAmount = curSkillTime / skillCoolTime;
        }
        if (lastTime > 0 )
        {
            lastTime -= Time.deltaTime;
            lastTime = lastTime < 0 ? 0 : lastTime;
            float minute = Mathf.Floor(lastTime / 60);
            float secondes = lastTime % 60;
            timeText.text = minute.ToString("00") + ":" + secondes.ToString("00");
            if (lastTime <= 0)
            {
                GameManager.Instance.isGameOver = true;
                OnGameOver(false);
            }
        }
    }


    public bool IsAttackOK()
    {
        if(curAttackTime >= attackCoolTime)
        {
            return true;
        }
        return false;
    }

    public bool IsSkillOK()
    {
        if(curSkillTime >= skillCoolTime)
        {
            return true;
        }
        return false;
    }

}
