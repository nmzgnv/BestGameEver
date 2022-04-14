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

    private void Start()
    {
        _rateOfFire = 1 / shotsPerSecond;
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;


        if (_elapsed >= _rateOfFire)
        {
            _elapsed %= _rateOfFire;
            Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
        }
    }
}