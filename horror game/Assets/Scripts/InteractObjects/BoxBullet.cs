using UnityEngine;

public class BoxBullet : MonoBehaviour, IInteract
{
    [SerializeField] private int _countBullet;
    private Weapon _weapon;
    public void Interact()
    {
        _weapon = FindFirstObjectByType<Weapon>();
        _weapon.ChangeAllBullet(_countBullet);
        Destroy(gameObject);
    }
}
