using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Parametes")]
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _shootsInSecond;
    [SerializeField] private float _distance;
    private float _nextTimeToShoot;

    [Header("Bullet Parametes")]
    //[SerializeField] private BulletPool _bulletPool;
    [SerializeField] private GameObject _bullet;
    private int _curBulletInWeapon;
    [SerializeField] private int _countBulletSpendWhenShoot;
    [SerializeField] private int _maxCountBulletInWeapon;
    [SerializeField] private int _allBullet;
    public bool CanShoot { set; private get; } = true;
    private WeaponUI _weaponMagazineUI;

    [Header("Other")]
    [SerializeField] private Animator _amimShootRecoil;
    public Action<float, float, float> OnCameraShaked;
    [SerializeField] private AudioClip _shootSound;
    private void Awake()
    {
        _weaponMagazineUI = GetComponent<WeaponUI>();
    }
    private void Start()
    {
        _curBulletInWeapon = _maxCountBulletInWeapon;
        _weaponMagazineUI.ChangeUI(_curBulletInWeapon, _maxCountBulletInWeapon, _allBullet);
    }
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
        ReloadBullet();
    }
    private void Shoot()
    {
        if (CanShoot && _curBulletInWeapon > 0)
        {
            Instantiate(_bullet, _shootPosition.position, _shootPosition.rotation);
            ShootBullet(-_countBulletSpendWhenShoot);
            _amimShootRecoil.SetTrigger("Shoot");
            Sounds.OnPlaySound?.Invoke(_shootSound, 0.85f, 1.2f);
        }
    }
    public void ChangeAllBullet(int countBullet)
    {
        _allBullet += countBullet;
        _weaponMagazineUI.ChangeUI(_curBulletInWeapon, _maxCountBulletInWeapon, _allBullet);
    }
    private void ShootBullet(int countBullet)
    {
        if (_curBulletInWeapon + countBullet < 0)
        {
            Debug.Log("Bullet < 0");
        }
        else
        {
            _curBulletInWeapon += countBullet;
        }
        _weaponMagazineUI.ChangeUI(_curBulletInWeapon, _maxCountBulletInWeapon, _allBullet);
    }
    private void ReloadBullet()
    {
        if (Input.GetKeyDown(KeyCode.R) && _curBulletInWeapon < _maxCountBulletInWeapon)
        {
            if (_allBullet - (_maxCountBulletInWeapon - _curBulletInWeapon) > 0)
            {
                _allBullet -= _maxCountBulletInWeapon - _curBulletInWeapon;
                _curBulletInWeapon = _maxCountBulletInWeapon;
            }
            else
            {
                _curBulletInWeapon += _allBullet;
                _allBullet = 0;
            }
            _weaponMagazineUI.ChangeUI(_curBulletInWeapon, _maxCountBulletInWeapon, _allBullet);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Ray ray = new Ray(_shootPosition.position, _shootPosition.forward);

        if (Physics.Raycast(ray, out var hitInfo, _distance, _layerMask))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPosition = ray.origin + ray.direction * _distance;
            DrawRay(ray, hitPosition, _distance, Color.green);
        }
    }
    private static void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
    {
        const float hitPointRadius = 0.15f;
        Debug.DrawRay(ray.origin, ray.direction * distance, color);
        Gizmos.color = color;
        Gizmos.DrawSphere(hitPosition, hitPointRadius);
    }
}
