using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public bool newGame = false;
    public bool loadGame = false;
    public GameObject player;
    public PlayerController playerController;

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && newGame == true)
        {
            NewGameData();
            newGame = false;
        }
        if (SceneManager.GetActiveScene().buildIndex != 1 && loadGame == true)
        {
            LoadGameData();
            loadGame = false;
        }
    }

    public void NewGame()
    {
        SceneTransition.SwitchToSceneByIndex(2);
        newGame = true;
    }

    public void NewGameData()
    {
        player = GameObject.FindGameObjectWithTag("Actor");
        playerController = player.GetComponent<PlayerController>();
        SaveSystem.NewGame(playerController);
    }

    public void LoadGame()
    {
        SaveSystem.LoadPlayer();
        loadGame = true;
    }

    public void LoadGameData()
    {
        player = GameObject.FindGameObjectWithTag("Actor");
        playerController = player.GetComponent<PlayerController>();
        PlayerData data = SaveSystem.LoadDataPlayer();
        playerController._health = data.health;
        Vector3 positionLoad;
        positionLoad.x = data.postition[0];
        positionLoad.y = data.postition[1];
        positionLoad.z = data.postition[2];
        player.transform.position = positionLoad;
        playerController.countTriggers = data.countTriggers;
        playerController.countMessages = data.countMessages;
        playerController._countKillEnemy = data.countkills;
        playerController.LoadSaveObjects();
        Debug.Log("Õ¿ ŒÕ≈÷-“Œ «¿√–”«»ÀŒ—‹!");
    }


}
