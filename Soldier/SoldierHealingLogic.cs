using System;
using UnityEngine;

public class SoldierHealingLogic : MonoBehaviour
{
    private SoldierAnimationLogic soldierAnimationLogic;
    private Health health;

    public event Action OnHealPerformed;

    private void Start() {
        soldierAnimationLogic = GetComponent<SoldierAnimationLogic>();
        health = GetComponent<Health>();
    }

    public void Heal() {
        OnHealPerformed?.Invoke();
    }

    public bool HasHealed() {
        if (soldierAnimationLogic.HasPlayedAnimation()) {
            float increaseAmount = 30f;
            health.IncreaseHealth(increaseAmount);
            return true;
        }
        else {
            return false;
        };
    }
}
