using UnityEngine;

public class SecondArena : MonoBehaviour
{
    public GameObject _arena;
    public RandomSpawner _randomSpawner;
    public PlayerController _playerController;
    public GameObject _nextTrigger;
    public GameObject _thirdAudioTrigger;
    public CameraController _cameraController;
    public GameObject _arrowNext;
    float _backupMinPositionCameraX;
    float _backupMaxPositionCameraX;

    private void Start()
    {
        _backupMaxPositionCameraX = _cameraController.maxPosition.x;
        _backupMinPositionCameraX = _cameraController.minPosition.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _arena.gameObject.SetActive(true);
        _randomSpawner.StartSpawn();
        _cameraController.minPosition.x = 6373;
        _cameraController.maxPosition.x = 7153;
    }

    void Update()
    {
        if (_playerController._countKillEnemy == 10)
        {
            _arrowNext.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            _arena.gameObject.SetActive(false);
            _nextTrigger.gameObject.SetActive(true);
            _thirdAudioTrigger.gameObject.SetActive(true);
            _cameraController.maxPosition.x = _backupMaxPositionCameraX;
            _cameraController.minPosition.x = _backupMinPositionCameraX;
        }
    }
}
