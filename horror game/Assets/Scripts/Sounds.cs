using System;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    private AudioSource _audioSource => GetComponent<AudioSource>();
    public static Action<AudioClip, float, float> OnPlaySound;
    private void OnEnable()
    {
        OnPlaySound += PlaySound;
    }
    private void OnDisable()
    {
        OnPlaySound -= PlaySound;
    }
    private void PlaySound(AudioClip clip, float p1 = 0.85f, float p2 = 1.2f)
    {
        _audioSource.pitch = UnityEngine.Random.Range(p1, p2);
        _audioSource.PlayOneShot(clip);
    }
}
