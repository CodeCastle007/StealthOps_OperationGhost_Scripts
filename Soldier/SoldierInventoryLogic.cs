using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SoldierInventoryLogic : MonoBehaviour
{
    private SoldierInteractionLogic soldierInteractionLogic;

    [SerializeField] private int maxSlots = 5;
    private Dictionary<InteractableItemSO,int> inventory;

    private void Start() {
        soldierInteractionLogic = GetComponent<SoldierInteractionLogic>();
        inventory = new Dictionary<InteractableItemSO, int>();

        soldierInteractionLogic.OnInteractPerformed += SoldierInteractionLogic_OnInteractPerformed;
    }

    private void SoldierInteractionLogic_OnInteractPerformed(IInteractable _interactable) {

        InteractableItemSO _interactableItemSO = _interactable.GetTransform().GetComponent<PickableItem>().GetInteractableItemSO();

        if (inventory.ContainsKey(_interactableItemSO)) {
            //Check if we already contain that item or not
            StackItem(_interactableItemSO);
        }
        else {
            //We donot have this item
            AddNewItem(_interactableItemSO);
        }
    }

    private void StackItem(InteractableItemSO _interactableSO) {
        //We already have the item just increase the count
        inventory[_interactableSO]++;

        Debug.Log("Stacking " + _interactableSO.name + " in Inventory....Count: " + inventory[_interactableSO]);
    }
    private void AddNewItem(InteractableItemSO _interactablSO) {
        if (inventory.Count < maxSlots) {
            //We have sufficient slots available to store
            inventory.Add(_interactablSO,1);

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
        }
        //Debug.Log("Removing " + _interactableSO.name + " from Inventory");
    }

    public Dictionary<InteractableItemSO,int> GetInventoryDictionary() {
        return inventory;
    }
}
