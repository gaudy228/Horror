using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _hp;
    public void TakeDamage(float damage, Vector3 position, Vector3 direction)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
