using UnityEngine;
using System;
using UnityEngine.Playables;

public class SoldierInteractionLogic : MonoBehaviour
{
    private Soldier soldier;
    private SoldierAnimationLogic soldierAnimationLogic;

    private IInteractable interactable;
   

    public event Action<IInteractable> OnInteractPerformed;

    private void Start(){
        soldier=GetComponent<Soldier>();
        soldierAnimationLogic = GetComponent<SoldierAnimationLogic>();
    }

    public void InteractWithInteractable(IInteractable _interactable){
        if (_interactable == null) return;

        interactable = _interactable;

        OnInteractPerformed?.Invoke(interactable);
    }

    //Called by interactCommand to check if we have interacted or not to issue the next command
    public bool HasInteracted(){
        if (soldierAnimationLogic.HasPlayedAnimation()) {

            interactable.Interact(soldier);
            interactable = null;
            return true;
        }
        
        return false;
    }
}
