using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _hp;
    public void ApplyDamage(float damage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
