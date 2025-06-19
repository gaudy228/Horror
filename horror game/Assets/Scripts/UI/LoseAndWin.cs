using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseAndWin : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private PlayerHP _playerHP;
    [SerializeField] private Weapon _weapon;
    private void OnEnable()
    {
        _playerHP.OnDied += Lose;
    }
    private void OnDisable()
    {
        _playerHP.OnDied -= Lose;
    }
    private void Lose()
    {
        _losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _weapon.CanShoot = false;
    }
    private void Win()
    {
        _winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _weapon.CanShoot = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Win();
        }
    }
    public void GameAgain()
    {
        SceneManager.LoadScene(1);
    }
}
