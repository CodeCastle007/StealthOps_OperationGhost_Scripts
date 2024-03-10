using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionHandlerUI : MonoBehaviour
{
    public static UnitSelectionHandlerUI Instance { get; private set; }


    [SerializeField] private Transform container;
    [SerializeField] private Button inventoryButton;

    public event Action OnInventoryButtonPressed;

    private void Awake() {

        Instance = this;

        inventoryButton.onClick.AddListener(() =>
        {
            OnInventoryButtonPressed?.Invoke();
        });
    }

    private void Start() {
        HideInventoryButton();

        SoldierSelectionHandler.Instance.OnSelectedSoldierChanged += SoldierSelectionHandler_OnSelectedSoldierChanged;
    }

    private void SoldierSelectionHandler_OnSelectedSoldierChanged() {
        if(SoldierSelectionHandler.Instance.HasUnitsSelected()) {
            ShowInventoryButton();
        }
        else {
            HideInventoryButton();
        }
    }

    private void ShowInventoryButton() {
        container.gameObject.SetActive(true);
    }

    private void HideInventoryButton() {
        container.gameObject.SetActive(false);
    }
}
