using UnityEngine;
public class FlashlightSwitch : MonoBehaviour
{
    private Light _light;
    public bool FlashlightOn { get; private set; } = true;
    public float BaseLightIntensity;
    private BatteryPower _batteryPower;
    private LightAttak _lightAttak;
    private void Awake()
    {
        _light = GetComponent<Light>();
        _batteryPower = GetComponent<BatteryPower>();
        _lightAttak = GetComponent<LightAttak>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlashlightOn = !FlashlightOn;
            if (FlashlightOn && !_batteryPower.NeedBattery && !_lightAttak.Attaking)
            {
                _light.intensity = BaseLightIntensity;
            }
        }
        if (_batteryPower.NeedBattery)
        {
            FlashlightOn = false;
        }
        if (!FlashlightOn)
        {
            _light.intensity = 0;
        }
    }
}
