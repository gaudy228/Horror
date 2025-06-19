using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private CameraRotate _camR;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Scrollbar _sens;
    [SerializeField] private Scrollbar _volumeAudio;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private AudioSource _audioSource;
    private void Start()
    {
        Time.timeScale = 1;
        _volumeAudio.value = _audioSource.volume;
        _sens.value = _camR.MouseSens / _camR.MaxMouseSens;
        _sens.onValueChanged.AddListener(value =>
        {
            _camR.MouseSens = Mathf.Max(_camR.MinMouseSens, value * _camR.MaxMouseSens);
        });
        _volumeAudio.onValueChanged.AddListener(value =>
        {
            _audioSource.volume = Mathf.Max(0, value * 1);
        });
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        _pausePanel.SetActive(false);
        _weapon.CanShoot = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pausePanel.SetActive(true);
            _weapon.CanShoot = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
