using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    private CinemachineVirtualCamera _camera;
    private CinemachineBasicMultiChannelPerlin _channelPerlin;
    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _channelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void OnEnable()
    {
        _weapon.OnCameraShaked += Shake;
    }
    private void OnDisable()
    {
        _weapon.OnCameraShaked -= Shake;
    }
    private void Shake(float strengtMax, float gainTime, float fadeTime)
    {
        StopCoroutine(ShakeCor(strengtMax, gainTime, fadeTime));
        StartCoroutine(ShakeCor(strengtMax, gainTime, fadeTime));
    }
    void OnUpdate(float value)
    {
        _channelPerlin.m_AmplitudeGain = value;
    }
    private IEnumerator ShakeCor(float strengtMax, float gainTime, float fadeTime)
    {
        float strengtMin = 0;
        DOTween.To(OnUpdate, strengtMin, strengtMax, gainTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(gainTime);
        DOTween.To(OnUpdate, strengtMax, strengtMin, fadeTime).SetEase(Ease.Linear);
    }
}
