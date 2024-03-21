using UnityEngine;

public class SoldierAnimationLogic : MonoBehaviour
{
    private Animator animator;
    
    private const string PICKUP = "PickUp";
    private const string HEAL = "Heal";

    private bool hasPlayedAnimation;

    private void Start() {
        animator = GetComponent<Animator>();
        
        GetComponent<SoldierInteractionLogic>().OnInteractPerformed += SoldierInteractionLogic_OnInteractPerformed;
        GetComponent<SoldierHealingLogic>().OnHealPerformed += SoldierAnimationLogic_OnHealPerformed;
    }

    private void SoldierAnimationLogic_OnHealPerformed() {
        hasPlayedAnimation = false;
        PlayHealAnimation();
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
    public void PlayHealAnimation() {
        animator.SetTrigger(HEAL);
    }

    //Called by event on animation
    public void ToggleAnimationStatus() {
        hasPlayedAnimation = true;
    }
    //Called by SoldierInteractionLogic, SoldierHealLogic
    public bool HasPlayedAnimation() {
        return hasPlayedAnimation;
    }
}
