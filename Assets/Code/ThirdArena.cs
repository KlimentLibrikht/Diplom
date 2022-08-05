using UnityEngine;

public class ThirdArena : MonoBehaviour
{
    public GameObject _arena;
    public RandomSpawner _randomSpawner;
    public PlayerController _playerController;
    public GameObject _fourAudioTrigger;
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
        _cameraController.minPosition.x = 9612;
        _cameraController.maxPosition.x = 10365;
    }

    void Update()
    {
        if (_playerController._countKillEnemy == 18)
        {
            this.gameObject.SetActive(false);
            _arrowNext.gameObject.SetActive(true);
            _arena.gameObject.SetActive(false);
            _fourAudioTrigger.SetActive(true);
            _cameraController.maxPosition.x = _backupMaxPositionCameraX;
            _cameraController.minPosition.x = _backupMinPositionCameraX;
        }
    }
}
