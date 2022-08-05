using UnityEngine;

public class FirstArena : MonoBehaviour
{
    public GameObject _arena;
    public RandomSpawner _randomSpawner;
    public PlayerController _playerController;
    public AudioSource _monolog;
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
        _cameraController.minPosition.x = 3140;
        _cameraController.maxPosition.x = 3630;
        if (_monolog != null)
        {
            _monolog.Play();
            _monolog = null;
        }
    }

    void Update()
    {
        if (_playerController._countKillEnemy == 4)
        {
            _arrowNext.gameObject.SetActive(true);
            _arena.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            _cameraController.maxPosition.x = _backupMaxPositionCameraX;
            _cameraController.minPosition.x = _backupMinPositionCameraX;
        }
    }
}
