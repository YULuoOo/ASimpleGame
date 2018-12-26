using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private Player player;
    public Vector3[] spawnPoints;

	// Use this for initialization
	void Start () {
        CharacterType type = GameManager.Instance.CharacterType;
        string playerName = string.Empty;
        if(type == CharacterType.warrior)
        {
            playerName = GameDefines.warriorPath;
        }
        else
        {
            playerName = GameDefines.archerPath;
        }

        player = EntityManager.Instance.CreateEntity<Player>(playerName, new Vector3(-18, 0.5f, -3), Vector3.forward);

        SpawnEnermy();
	}

    private void SpawnEnermy()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        int enermyCount = spawnPoints.Length;
        for(int i =0;i<spawnPoints.Length;i++)
        {
            Vector3 dir = new Vector3(Random.Range(-360, 360), 0, Random.Range(-360, 360));
            Enermy enermy = EntityManager.Instance.CreateEnermy(GameDefines.enermyPath, spawnPoints[i], dir);
            enermy.InitEnermy(spawnPoints, player);

        }
        GameManager.Instance.InitBattleInfo(enermyCount);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
