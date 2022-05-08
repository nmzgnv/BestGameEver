using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _attackRadiusCenter;

    [SerializeField]
    private Transform _bulletTarget;

    private PlayerHealth _playerHealth;
    private PlayerAttack _playerAttack;
    private ManaController _manaController;
    private AudioSource _audioSource;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAttack = GetComponent<PlayerAttack>();
        _manaController = GetComponent<ManaController>();
        _audioSource = GetComponent<AudioSource>();
    }

    public AudioSource AudioSource => _audioSource;
    public PlayerHealth Health => _playerHealth;
    public PlayerAttack Attack => _playerAttack;
    public ManaController Mana => _manaController;

    public Transform BulletTarget => _bulletTarget;
    public Transform AttackRadiusCenter => _attackRadiusCenter;
}