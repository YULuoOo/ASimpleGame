using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    private Button startButon;

	// Use this for initialization
	void Start () {
        startButon = GetComponent<Button>();
        startButon.onClick.AddListener(StartClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void StartClick()
    {
        //if(GameManager.Instance.PlayerName != string.Empty && GameManager.Instance.PlayerName != null)
       // {
       //     GameManager.Instance.LoadScene(GameDefines.mainScene);
        //}
       // else
            GameManager.Instance.LoadScene(GameDefines.Create);
    }
}
