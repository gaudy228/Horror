using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bulletsCount;
    public void ChangeUI(int curBullet, int maxBullet, int allBullet)
    {
        _bulletsCount.text = $"{curBullet}/{maxBullet}\n{allBullet}";
    }
}
