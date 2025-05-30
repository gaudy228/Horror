using UnityEngine;

public class Gan : MonoBehaviour
{
    [Header("Weapon Parametes")]
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _shootsInSecond;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;

    private float _nextTimeToShoot;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(Time.time > _nextTimeToShoot)
            {
                _nextTimeToShoot = Time.time + 1 / _shootsInSecond;
                Shoot();
            } 
        }
    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(_bullet, _shootPosition.position, _shootPosition.rotation).GetComponent<Bullet>();
        bullet.StartCoroutine(bullet.Shoot(_shootPosition.position, _shootPosition.forward * 500f, _bulletSpeed));
    }
}
