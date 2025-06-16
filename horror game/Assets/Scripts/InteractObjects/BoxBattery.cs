using UnityEngine;

public class BoxBattery : MonoBehaviour, IInteract
{
    [SerializeField] private float _plusBatteryPercent;
    public void Interact()
    {
        BatteryPower.OnChangeBattery?.Invoke(_plusBatteryPercent);
        Destroy(gameObject);
    }
}
