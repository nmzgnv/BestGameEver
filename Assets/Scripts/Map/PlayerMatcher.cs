using UnityEngine;

public class PlayerMatcher : MonoBehaviour
{
    [SerializeField]
    private CameraMovementController cameraMovementController;

    private Player _player;

    private void Update()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
            if (_player != null)
            {
                Debug.Log("Player Was Found");
                cameraMovementController.Target = _player.BulletTarget;
            }
        }
    }
}