using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private Transform inventorySlotTemplate;
    [SerializeField] private Button closeButton;

    private Dictionary<InteractableItemSO, int> inventory;
    private Soldier soldier;

    private void Awake() {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start() 
    {
        Hide();
        inventorySlotTemplate.gameObject.SetActive(false);

        UnitSelectionHandlerUI.Instance.OnInventoryButtonPressed += UnitSelectionHandlerUI_OnInventoryButtonPressed; //Listens to event to open inventory
        InventorySlotTemplateUI.OnAnySlotDropButtonPressed += InventorySlotTemplateUI_OnAnySlotDropButtonPressed;
    }

    private void InventorySlotTemplateUI_OnAnySlotDropButtonPressed(InteractableItemSO obj) {

        soldier.GetComponent<SoldierInventoryLogic>().RemoveItem(obj);

        //Updating inventory UI after droping an object
        ClearSlots();
        GenerateSlots(); 
    }

    private void UnitSelectionHandlerUI_OnInventoryButtonPressed() {
        if(SoldierSelectionHandler.Instance.GetSelectedUnitsList().Count == 1) 
        {
            //We will get the soldier dictionary including items 
            soldier = SoldierSelectionHandler.Instance.GetSelectedUnitsList()[0];
            inventory = soldier.GetComponent<SoldierInventoryLogic>().GetInventoryDictionary();

            GenerateSlots();
            Show();
        }
        else {
            inventory = null;
            soldier = null;
        }
        
    }

    private void GenerateSlots() {
        ClearSlots();

        foreach (var pair in inventory) {
            Transform slotTransform = Instantiate(inventorySlotTemplate, slotContainer);
            slotTransform.gameObject.SetActive(true);

            slotTransform.GetComponent<InventorySlotTemplateUI>().SetInteractableSO(pair.Key, pair.Value);
        }
    }

    private void ClearSlots() {
        foreach (Transform child in slotContainer) {
            if (child == inventorySlotTemplate) continue;
            Destroy(child.gameObject);
        }
    }

    private void Show() {
        container.gameObject.SetActive(true);
    }
    private void Hide() {
        container.gameObject.SetActive(false);
    }
}
