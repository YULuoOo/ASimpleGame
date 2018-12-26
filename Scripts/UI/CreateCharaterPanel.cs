using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharaterPanel : MonoBehaviour
{

    public Button warriorButton;
    public Button archerButton;
    public Button createButton;
    public InputField nameInput;
    public Transform mRoot;
    private GameObject warriorObj;
    private GameObject archerObj;

    // Use this for initialization
    void Start()
    {
        mRoot = transform;
        warriorButton = mRoot.Find("WarriorButton").GetComponent<Button>() as Button;
        archerButton = mRoot.Find("ArcherButton").GetComponent<Button>() as Button;
        createButton = mRoot.Find("CreateButton").GetComponent<Button>() as Button;
        nameInput = mRoot.Find("Name/InputField").GetComponent<InputField>() as InputField;

        warriorButton.onClick.AddListener(WarriorClick);
        archerButton.onClick.AddListener(ArcherClick);
        createButton.onClick.AddListener(CreateClick);
    }
    private void WarriorClick()
    {
        if(warriorObj == null)
        {
            GameObject obj = GameManager.Instance.LoadResources<GameObject>(GameDefines.warriorPath);
            warriorObj = GameObject.Instantiate(obj);
            warriorObj.transform.position = Vector3.zero;
            warriorObj.transform.rotation = new Quaternion(0, 180, 0, 0);
            warriorObj.SetActive(true);
        }
        else
        {
            warriorObj.SetActive(true);
        }
        if(archerObj != null)
        {
            archerObj.SetActive(false);
        }
        PlayerPrefs.SetInt(GameDefines.playerRole, (int)CharacterType.warrior);

    }
    private void ArcherClick()
    {
        if (archerObj == null)
        {
            GameObject obj = GameManager.Instance.LoadResources<GameObject>(GameDefines.archerPath);
            archerObj = GameObject.Instantiate(obj);
            archerObj.transform.position = Vector3.zero;
            archerObj.transform.rotation = new Quaternion(0, 180, 0, 0);
            archerObj.SetActive(true);
        }
        else
        {
            archerObj.SetActive(true);
        }
        if (warriorObj != null)
        {
            warriorObj.SetActive(false);
        }
        PlayerPrefs.SetInt(GameDefines.playerRole, (int)CharacterType.archer);
    }
    private void CreateClick()
    {
        string name = nameInput.text;
        if (name == string.Empty)
        {
            return;
        }
        PlayerPrefs.SetString(GameDefines.playerName, name);
        GameManager.Instance.LoadScene(GameDefines.mainScene);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
