using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCommand : ICommand
{
    private IInteractable interactable;
    private Soldier soldier;

    public event Action OnComplete;

    public InteractCommand(IInteractable _interactable, Soldier _soldier){
        interactable = _interactable;
        soldier=_soldier;
    }

    public void Execute()
    {
        soldier.Interact(interactable);
    }

    public bool IsComplete()
    {
        return soldier.GetComponent<SoldierInteractionLogic>().HasInteracted();
    }

    public void Update()
    {
        if(IsComplete()){
            //Debug.Log("Interact Command Completed....");
            OnComplete?.Invoke();
        }
    }
}
