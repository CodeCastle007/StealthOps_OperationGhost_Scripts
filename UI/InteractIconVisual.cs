using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractIconVisual : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Image icon;
    [SerializeField] private Button interactButton;

    public static event Action<Transform> OnAnyInteractIconClicked;

    private void Awake() {
        interactButton.onClick.AddListener(() =>
        {
            OnAnyInteractIconClicked?.Invoke(GetComponentInParent<IInteractable>().GetTransform());
        });
    }

    

    private void Start() {
        //Get icon from parent object which is an interactable
        icon.sprite = GetComponentInParent<IInteractable>().GetInteractableItemSO().icon;

        SoldierCommandHandler.Instance.OnCurrentCommandChange += SoldierCommandHandler_OnCurrentCommandChange;
        SoldierSelectionHandler.Instance.OnSelectedSoldierChanged += SoldierSelectionHandler_OnSelectedSoldierChanged;

        Hide();
    }

    private void SoldierSelectionHandler_OnSelectedSoldierChanged() {
        Hide();
    }

    private void SoldierCommandHandler_OnCurrentCommandChange() {

        CommandsSO currentCommandSO = SoldierCommandHandler.Instance.GetCurrentCommandSO();

        if(currentCommandSO != null) {
            if (currentCommandSO.commandType == CommandsSO.Command.Interact) {
                Show();
                return;
            }
        }

        Hide();
    }

    private void OnDestroy() {
        SoldierCommandHandler.Instance.OnCurrentCommandChange -= SoldierCommandHandler_OnCurrentCommandChange;
        SoldierSelectionHandler.Instance.OnSelectedSoldierChanged -= SoldierSelectionHandler_OnSelectedSoldierChanged;
    }

    private void Show() {
        container.gameObject.SetActive(true);
    }
    private void Hide() {
        container.gameObject.SetActive(false);
    }
}
