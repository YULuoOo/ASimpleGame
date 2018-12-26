using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{

    private Text resultText;
    private Text killCountText;
    private Text timeText;
    private Text hpText;
    private int killcount;
    private int useTime;
    private int HPNumber;
    private Transform mRoot;
    private Player player;

    void Awake()
    {
        mRoot = transform;
        resultText = mRoot.Find("ResultText").GetComponent<Text>();
        killCountText = mRoot.Find("KillCount/Value").GetComponent<Text>();
        timeText = mRoot.Find("LastTime/Value").GetComponent<Text>();
        hpText = mRoot.Find("HP/Value").GetComponent<Text>();
        player = GameObject.FindObjectOfType<Player>();

    }

    public void InitPanel(bool isWin, int _killcount, int _useTime)
    {
        if (isWin)
        {
            resultText.text = "胜利";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = "失败";
            resultText.color = Color.blue;
        }

        killcount = _killcount;
        useTime = _useTime;
        HPNumber = player.curHP;
        killCountText.text = "0";
        timeText.text = "00:00";
        hpText.text = "0";
        StartCoroutine("KillNumberCount");
    }

    IEnumerator KillNumberCount()
    {
        int count = 0;
        while (killcount > count)
        {
            count++;
            killCountText.text = count.ToString();
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine("UseTimeCount");
    }

    IEnumerator UseTimeCount()
    {
        int count = 0;
        while (useTime > count)
        {
            count++;
            float minute = Mathf.Floor(count / 60);
            float seconds = count % 60;
            timeText.text = minute.ToString("00") + ":" + seconds.ToString("00");
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("GoldCount");
    }

    IEnumerator GoldCount()
    {
        int count = 0;
        while (HPNumber > count)
        {
            count++;
            hpText.text = count.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
