using System.Collections;
using UnityEngine;
using DG.Tweening;

public class LightAttak : MonoBehaviour
{
    private Light _light;
    [Header("Light Parametes")]
    [SerializeField] private float _attakLightIntensity;
    [SerializeField] private float _baseLightRange;
    [SerializeField] private float _maxLightRange;
    [SerializeField] private float _timeToAttak;
    [SerializeField] private float _timeToBase;
    [SerializeField] private float _minusPercentAttak;
    public bool Attaking { get; private set; } = false;
    private BatteryPower _batteryPower;
    private FlashlightSwitch _flashlightSwitch;
    [Header("Attak Parametes")]
    [SerializeField] private Transform _attakPoint;
    [SerializeField] private float _attakDistance;
    [SerializeField] private float _timeStun;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstacleLayer;
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
        Stun();
        Attaking = true;
        DOTween.To(OnUpdateIntensity, _flashlightSwitch.BaseLightIntensity, _attakLightIntensity, _timeToAttak).SetEase(Ease.Linear);
        DOTween.To(OnUpdateRange, _baseLightRange, _maxLightRange, _timeToBase).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_timeToAttak);
        DOTween.To(OnUpdateIntensity, _attakLightIntensity, _flashlightSwitch.BaseLightIntensity, _timeToBase).SetEase(Ease.Linear);
        DOTween.To(OnUpdateRange, _maxLightRange, _baseLightRange, _timeToBase).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_timeToBase);
        Attaking = false;
    }
    private void OnUpdateIntensity(float value)
    {
        _light.intensity = value;
    }
    private void OnUpdateRange(float value)
    {
        _light.range = value;
    }
    private void Stun()
    {
        Collider[] _enemy = Physics.OverlapSphere(_attakPoint.position, _attakDistance, _enemyLayer);
        
        for (int i = 0; i < _enemy.Length; i++)
        {
            if (_enemy[i].TryGetComponent(out Enemy enemy))
            {
                enemy.Stun(_timeStun);
            }
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(_attakPoint.position, _attakDistance);
    //}
}
