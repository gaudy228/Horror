using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private Key _key;
    private bool _isOpen = false;
    private void OnEnable()
    {
        _key.OnTakeKey += DoorOpen;
    }
    private void OnDisable()
    {
        _key.OnTakeKey -= DoorOpen;
    }
    private void DoorOpen()
    {
        _isOpen = true;
    }
    public void Interact()
    {
        if(_isOpen)
        {
            Destroy(gameObject);
        }
    }
}
