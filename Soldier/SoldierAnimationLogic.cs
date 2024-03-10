using UnityEngine;

public class SoldierAnimationLogic : MonoBehaviour
{
    private Animator animator;
    private SoldierInteractionLogic soldierInteractionLogic;

    private const string PICKUP = "PickUp";

    private bool hasPlayedAnimation;

    private void Start() {
        animator = GetComponent<Animator>();
        soldierInteractionLogic=GetComponent<SoldierInteractionLogic>();

        soldierInteractionLogic.OnInteractPerformed += SoldierInteractionLogic_OnInteractPerformed;
    }

    private void SoldierInteractionLogic_OnInteractPerformed(IInteractable _interactable) {

        hasPlayedAnimation = false;

        if (_interactable.GetTransform().GetComponent<PickableItem>()) {
            //Item is pickable
            PlayPickUpAnimation();
        }
    }

    public void PlayPickUpAnimation() {
        animator.SetTrigger(PICKUP);
    }

    //Called by event on animation
    public void ToggleAnimationStatus() {
        hasPlayedAnimation = true;
    }
    //Called by SoldierInteractionLogic
    public bool HasPlayedAnimation() {
        return hasPlayedAnimation;
    }
}
