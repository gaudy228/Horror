using System;
using UnityEngine;

public class Key : MonoBehaviour, IInteract
{
    public Action OnTakeKey;
    public void Interact()
    {
        OnTakeKey?.Invoke();
        Destroy(gameObject);
    }
}
