using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image bar;

    public void ChangeHealth(float value) {
        if (value < 0) return;

        bar.fillAmount = value;
    }
}
