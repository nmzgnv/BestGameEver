using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float shotsPerSecond;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform bulletSpawnPoint;

    private float _elapsed;
    private float _rateOfFire;

    public bool CanShoot { get; set; }

    public Transform BulletSpawnPoint
    {
        get => bulletSpawnPoint;
    }

    private void Start()
    {
        _rateOfFire = 1 / shotsPerSecond;
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;


        if (_elapsed >= _rateOfFire && CanShoot)
        {
            _elapsed %= _rateOfFire;
            Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
        }
    }
}