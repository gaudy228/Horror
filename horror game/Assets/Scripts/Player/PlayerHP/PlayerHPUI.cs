using TMPro;
using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpPlayer;
    private PlayerHP _playerHP;
    private void Awake()
    {
        _playerHP = GetComponent<PlayerHP>();
    }
    private void OnEnable()
    {
        _playerHP.OnChangeHPUI += ChangeUI;
    }
    private void OnDisable()
    {
        _playerHP.OnChangeHPUI -= ChangeUI;
    }
    private void ChangeUI(float hp)
    {
        _hpPlayer.text = hp.ToString();
    }
}
