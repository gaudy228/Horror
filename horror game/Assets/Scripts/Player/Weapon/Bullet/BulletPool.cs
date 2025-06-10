using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _poolCapacity;
    private Queue<Bullet> _bullet = new Queue<Bullet>();
    private void Awake()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            _bullet.Enqueue(Instantiate(_bulletPrefab, transform));
        }
        foreach (Bullet bullet in _bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.Initialize(this);
        }
    }
    public Bullet Get()
    {
        if(_bullet.Count == 0)
        {
            ExpandPool();
        }
        Bullet newBullet = _bullet.Dequeue();
        newBullet.gameObject.SetActive(true);
        return newBullet;
    }
    public void Return(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        _bullet.Enqueue(bullet);   
    }
    private void ExpandPool()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform);
        bullet.Initialize(this);
        _bullet.Enqueue(bullet);
    }
}
