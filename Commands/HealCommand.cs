using System;
using UnityEngine;

public class HealCommand : ICommand
{
    private Soldier soldier;
    private InteractableItemSO requiredItemSO;
    public event Action OnComplete;

    public HealCommand(InteractableItemSO _requiredItemSO,Soldier _soldier) {
        soldier = _soldier;
        requiredItemSO = _requiredItemSO;
    }

    public void Execute() {
        soldier.Heal(requiredItemSO);
    }

    public bool IsComplete() {
        return soldier.GetComponent<SoldierHealingLogic>().HasHealed();
    }

    public void Update() {
        if (IsComplete()) {
            //Debug.Log("Interact Command Completed....");
            OnComplete?.Invoke();
        }
    }
}
