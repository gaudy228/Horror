using System.Collections;
using UnityEngine;
using DG.Tweening;
public class LightAttak : MonoBehaviour
{
    private Light _light;
    [SerializeField] private float _attakLightIntensity;
    [SerializeField] private float _timeToAttak;
    [SerializeField] private float _timeToBase;
    [SerializeField] private float _minusPercentAttak;
    public bool Attaking { get; private set; } = false;
    private BatteryPower _batteryPower;
    private FlashlightSwitch _flashlightSwitch;
    private void Awake()
    {
        _light = GetComponent<Light>();
        _batteryPower = GetComponent<BatteryPower>();
        _flashlightSwitch = GetComponent<FlashlightSwitch>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !Attaking
            && _batteryPower.BatteryPercent - _minusPercentAttak >= 0 && _flashlightSwitch.FlashlightOn)
        {
            _batteryPower.ChangeBattery(-_minusPercentAttak);
            StartCoroutine(Attak());
        }
    }
    private IEnumerator Attak()
    {
        Attaking = true;
        DOTween.To(OnUpdate, _flashlightSwitch.BaseLightIntensity, _attakLightIntensity, _timeToAttak).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_timeToAttak);
        DOTween.To(OnUpdate, _attakLightIntensity, _flashlightSwitch.BaseLightIntensity, _timeToBase).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_timeToBase);
        Attaking = false;
    }
    private void OnUpdate(float value)
    {
        _light.intensity = value;
    }
}
