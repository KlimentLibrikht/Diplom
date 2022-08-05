using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class Main_Menu_Buttons : MonoBehaviour
{
    public Button loadButton;
    public GameObject globalObject;
    public Global global;
    public void Start()
    {
        globalObject = GameObject.FindGameObjectWithTag("Global");
        global = globalObject.GetComponent<Global>();
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            loadButton.interactable = true;
        }
        else
        {
            loadButton.interactable = false;
        }
    }
    public void PlayGame()
    {
        DontDestroyOnLoad(global);
        global.NewGame();
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        DontDestroyOnLoad(global);
        global.LoadGame();
    }
}
