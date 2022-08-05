using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroWait : MonoBehaviour
{
    public int _waitTime;
    public int _sceneIndex;
    public bool _isWaiting = true;
    public GameObject _text;

    private void Start()
    {
        if (_text != null)
        {
            Invoke("TextStart", 5.0f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync(_sceneIndex);
        }
        else if (_isWaiting == true)
        {
            Invoke("WaitForLevel", _waitTime);
            _isWaiting = false;
        }
    }
    void WaitForLevel()
    {
        SceneManager.LoadSceneAsync(_sceneIndex);
    }

    void TextStart()
    {
        _text.gameObject.SetActive(true);
    }
}
