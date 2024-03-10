using System;
using UnityEngine;
using UnityEngine.UI;

public class SoldierAvatarTemplateUI : MonoBehaviour
{
    [SerializeField] private Image avatarIcon;
    [SerializeField] private Transform selectedVisual;
    [SerializeField] private HealthUI healthUI;

    private Button button;
    private bool isSelected;
    private Soldier soldier;
    public static event Action<Soldier> OnAvatarButtonClicked;

    private void Awake() {
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            OnAvatarButtonClicked?.Invoke(soldier);
        });
    }

    public void SetSoldier(Soldier _soldier) {
        soldier = _soldier;
        avatarIcon.sprite = soldier.GetSoldierSO().icon;

        soldier.GetComponent<SoldierSelectionLogic>().OnSelectionStatusChanged += SoldierAvatarTemplateUI_OnSelectionStatusChanged;
        soldier.GetComponent<Health>().OnHealthChange += SoldierAvatarTemplateUI_OnHealthChange;
    }

    private void SoldierAvatarTemplateUI_OnHealthChange(float obj) {
        healthUI.ChangeHealth(obj);
    }

    private void SoldierAvatarTemplateUI_OnSelectionStatusChanged(bool obj) {
        ToggleSelection();
    }
    public void ToggleSelection() {
        isSelected = !isSelected;
        selectedVisual.gameObject.SetActive(isSelected);
    }
}
