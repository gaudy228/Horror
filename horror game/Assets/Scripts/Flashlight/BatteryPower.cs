using System;
using UnityEngine;
public class BatteryPower : MonoBehaviour
{
    [SerializeField] private float _maxBatteryPercent;
    [SerializeField] private float _tic;
    private float _time;
    [SerializeField] private float _minusBatteryTic;
    private LightBatteryUI _batteryUI;
    public float BatteryPercent { get; private set; }
    public bool NeedBattery { get; private set; } = false;
    private FlashlightSwitch _flashlightSwitch;
    public static Action<float> OnChangeBattery;
    private void Awake()
    {
        _flashlightSwitch = GetComponent<FlashlightSwitch>();
        _batteryUI = GetComponent<LightBatteryUI>();
    }
    private void Start()
    {
        BatteryPercent = _maxBatteryPercent;
    }
    private void OnEnable()
    {
        OnChangeBattery += ChangeBattery;
    }
    private void OnDisable()
    {
        OnChangeBattery -= ChangeBattery;
    }
    private void Update()
    {
        MinusPercent();
    }
    public void ChangeBattery(float power)
    {
        BatteryPercent += power;
        if (BatteryPercent > _maxBatteryPercent)
        {
            BatteryPercent = _maxBatteryPercent;
        }
        if (BatteryPercent < 0)
        {
            BatteryPercent = 0;
        }
        NeedRecharge();
        _batteryUI.UpdateUIPercent(BatteryPercent);
    }
    private void NeedRecharge()
    {
        if (BatteryPercent == 0)
        {
            NeedBattery = true;
        }
        else
        {
            NeedBattery = false;
        }
    }
    private void MinusPercent()
    {
        if (BatteryPercent > 0 && _flashlightSwitch.FlashlightOn && !NeedBattery)
        {
            if (_time >= _tic)
            {
                ChangeBattery(-_minusBatteryTic);
                _time = 0;
            }
            else
            {
                _time += Time.deltaTime;
            }
        }
    }
}
