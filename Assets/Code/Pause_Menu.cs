using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public PlayerController Player;
    public Button _saveButton, _menuButton, _exitButton;
    Scene scene;
    public GameObject _firstArena, _secondArena, _thirdArena, _bossCamera;
    private Vector3 _exitPosition, _menuPosition;

    private void Start()
    {
        Cursor.visible = false;
        _exitPosition = _exitButton.transform.position;
        _menuPosition = _menuButton.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == false)
            {
                Pause();
            }
        }
    }

    public void SaveGame()
    {
        Player.SaveGame();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Player.isPause = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Player.isPause = true;
        Cursor.visible = true;
        if (_firstArena.activeInHierarchy == true || _secondArena.activeInHierarchy == true || _thirdArena.activeInHierarchy == true || _bossCamera.activeInHierarchy == true)
        {
            _exitButton.transform.position = _menuButton.transform.position;
            _menuButton.transform.position = _saveButton.transform.position;
            _saveButton.enabled = false;
            _saveButton.gameObject.SetActive(false);
        }
        else
        {
            _saveButton.gameObject.SetActive(true);
            _saveButton.enabled = true;
            _menuButton.transform.position = _menuPosition;
            _exitButton.transform.position = _exitPosition;
        }
    }

    public void LoadMenu()
    {
        SceneTransition.SwitchToSceneByIndex(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
