using TMPro;
using UnityEngine;
public class LightBatteryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _batteryPercent;
    public void UpdateUIPercent(float percent)
    {
        _batteryPercent.text = $"{percent}%";
    }
}
