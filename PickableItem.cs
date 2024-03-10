using UnityEngine;

public class PickableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableItemSO itemSO;

    public void Interact(Soldier _soldier){
        Destroy(this.gameObject);
    }

    public Transform GetTransform() {
        return transform;
    }

    public InteractableItemSO GetInteractableItemSO() {
        return itemSO;
    }
}
