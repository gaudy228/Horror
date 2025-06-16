using System;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageble
{
    [SerializeField] private float _hp;
    public Action<float> OnChangeHPUI;
    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Debug.Log("You dead");
        }
        OnChangeHPUI?.Invoke(_hp);
    }
}
