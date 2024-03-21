using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoldierInventoryLogic : MonoBehaviour
{
   
    [SerializeField] private int maxSlots = 5;
    [SerializeField] private List<InteractableItemSO> loadoutItems;
    private Dictionary<InteractableItemSO, int> inventory;

    private SoldierInteractionLogic soldierInteractionLogic;
    public static event Action OnInventoryItemsChanged;

    private void Start() {
        soldierInteractionLogic = GetComponent<SoldierInteractionLogic>();
        inventory = new Dictionary<InteractableItemSO, int>();

        soldierInteractionLogic.OnInteractPerformed += SoldierInteractionLogic_OnInteractPerformed;
        SetLoadOut();
    }

    private void OnDestroy() {
        OnInventoryItemsChanged = null;
    }

    private void SetLoadOut() {

        if (loadoutItems.Count <= 0) return; //We donot have any initial loadout

        //Give every soldier its initial weapons and items
        for (int i = 0; i < loadoutItems.Count; i++) {
            AddNewItem(loadoutItems[i]);
        }
    }

    private void SoldierInteractionLogic_OnInteractPerformed(IInteractable _interactable) {

        InteractableItemSO _interactableItemSO = _interactable.GetTransform().GetComponent<PickableItem>().GetInteractableItemSO();

        AddNewItem(_interactableItemSO);
    }

    private void StackItem(InteractableItemSO _interactableSO) {
        //We already have the item just increase the count
        inventory[_interactableSO]++;

        Debug.Log("Stacking " + _interactableSO.name + " in Inventory....Count: " + inventory[_interactableSO]);
    }
    private void AddNewItem(InteractableItemSO _interactablSO) {
        //check if we already have same item so we will stack it
        if (inventory.ContainsKey(_interactablSO)) {
            StackItem(_interactablSO);
            return;
        }

        if (inventory.Count < maxSlots) {
            //We have sufficient slots available to store
            inventory.Add(_interactablSO,1);

            OnInventoryItemsChanged?.Invoke();
            //Debug.Log("Adding " + _interactablSO.name + " in Inventory....Count: " + inventory[_interactablSO]);
        }
    }

    public void RemoveItem(InteractableItemSO _interactableSO) {
        if (inventory[_interactableSO] > 1) {
            //We have more than similar item so remove only one
            inventory[_interactableSO]--;
        }
        else {
            inventory.Remove(_interactableSO);

            OnInventoryItemsChanged?.Invoke();
        }
        //Debug.Log("Removing " + _interactableSO.name + " from Inventory");
    }

    public bool Contains(InteractableItemSO _interactableItemSO) {
        return inventory.ContainsKey(_interactableItemSO);
    }

    public Dictionary<InteractableItemSO,int> GetInventoryDictionary() {
        return inventory;
    }
}
