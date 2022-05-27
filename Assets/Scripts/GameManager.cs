using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CameraMovementController _cameraMovementController;
    private Player _player;
    private EnemiesController _enemiesController;
    public bool IsPlayerControlled = true; 
    
    private void SetUpCamera()
    {
        var camera = Camera.main.gameObject;
        _cameraMovementController = camera.GetComponent<CameraMovementController>();
        if (_cameraMovementController == null)
            _cameraMovementController = camera.gameObject.AddComponent<CameraMovementController>();
    }

    private void Start()
    {
        SetUpCamera();

        _enemiesController = FindObjectOfType<EnemiesController>();
        _player = FindObjectOfType<Player>();
        if (_player == null)
            Debug.LogWarning("Player not found!");
        
        _enemiesController.SetTarget(_player.BulletTarget);
        _cameraMovementController.Target = _player.BulletTarget;
    }
}