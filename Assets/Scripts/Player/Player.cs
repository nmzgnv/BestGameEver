using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _attackRadiusCenter;

    [SerializeField]
    private Transform _bulletTarget;

    [SerializeField]
    private AudioClip _takeDamageSound;

    private PlayerHealth _playerHealth;
    private PlayerAttack _playerAttack;
    private ManaController _manaController;
    private AudioSource _audioSource;
    private PhysicsMovement _physicsMovement;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAttack = GetComponent<PlayerAttack>();
        _manaController = GetComponent<ManaController>();
        _audioSource = GetComponent<AudioSource>();
        _physicsMovement = GetComponent<PhysicsMovement>();
    }

    private void Start()
    {
        if (_playerHealth != null)
            _playerHealth.OnPlayerTakesDamage += () =>
            {
                _audioSource.pitch = Random.Range(0.8f, 1.2f);
                _audioSource.PlayOneShot(_takeDamageSound, 0.5f);
            };
    }

    public AudioSource AudioSource => _audioSource;
    public PlayerHealth Health => _playerHealth;
    public PlayerAttack Attack => _playerAttack;
    public ManaController Mana => _manaController;
    public PhysicsMovement PhysicsMovement => _physicsMovement;

    public Transform BulletTarget => _bulletTarget;
    public Transform AttackRadiusCenter => _attackRadiusCenter;
}