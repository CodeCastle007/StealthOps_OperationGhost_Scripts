using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotTemplateUI : MonoBehaviour
{
    [Header("Item Details")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI nametext;
    [SerializeField] private TextMeshProUGUI countText;

    [Space(2)]
    [Header("Buttons")]
    [SerializeField] private Button useButton;
    [SerializeField] private Button dropButton;
    [SerializeField] private Button tradeButton;

    private InteractableItemSO interactableItemSO; //Just for event calling to tell Inventory UI which item to remove
    public static event Action<InteractableItemSO> OnAnySlotDropButtonPressed;

    private void Awake() {
        useButton.onClick.AddListener(() =>
        {
            //STILL TO IMPLEMENT
        });

        dropButton.onClick.AddListener(() =>
        {
            OnAnySlotDropButtonPressed?.Invoke(interactableItemSO);
        });

        tradeButton.onClick.AddListener(() =>
        {
            //STILL TO IMPLEMENT
        });
    }

    public void SetInteractableSO(InteractableItemSO _interactableSO, int _count) {

        interactableItemSO = _interactableSO;

        itemIcon.sprite = _interactableSO.icon;
        nametext.text = _interactableSO.name;
        countText.text = _count.ToString();

        if (!interactableItemSO.useable) {
            useButton.interactable = false;
        }
    }
}
