
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.SceneManagement;




public class GameManager
{

    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameManager();
            }
            return _Instance;
        }
    }

    public bool isGameOver = false;
    public int curKillCount = 0;
    public int totalEnemy = 0;

    public void OnKillEnemy()
    {
        if (isGameOver)
        {
            return;
        }
        curKillCount++;
        if (curKillCount == totalEnemy && totalEnemy != 0)
        {
            isGameOver = true;
            Messenger.Broadcast<bool>(GameEvent.GameOver, true);
        }
        Messenger.Broadcast<int>(GameEvent.KillEnemy, curKillCount);
    }

    public void InitBattleInfo(int _totalEnemy)
    {
        totalEnemy = _totalEnemy;
        isGameOver = false;
        curKillCount = 0;
    }

    public CharacterType CharacterType
    {
        get
        {
            return (CharacterType)PlayerPrefs.GetInt(GameDefines.playerRole);
        }
    }

    public string PlayerName
    {
        get
        {
            return PlayerPrefs.GetString(GameDefines.playerName);
        }
    }



    //切换场景
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public T LoadResources<T>(string path) where T : Object
    {
        Object obj = Resources.Load(path);
        if (obj == null)
        {
            return null;
        }
        return (T)obj;
    }
}
