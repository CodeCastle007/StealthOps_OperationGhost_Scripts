using UnityEngine;

public interface IInteractable
{
    public void Interact(Soldier _soldier);

    public Transform GetTransform();
    public InteractableItemSO GetInteractableItemSO();
}
