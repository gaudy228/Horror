using System;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamageble
{
    [SerializeField] private float _hp;
    public Action<float> OnChangeHPUI;
    public Action OnDied;
    [SerializeField] private AudioClip _takeDamageSound;
    public void TakeDamage(float damage)
    {
        _hp -= damage;
        Sounds.OnPlaySound?.Invoke(_takeDamageSound, 0.85f, 1.2f);
        if (_hp <= 0)
        {
            _hp = 0;
            OnDied?.Invoke();
        }
        OnChangeHPUI?.Invoke(_hp);
    }
}
