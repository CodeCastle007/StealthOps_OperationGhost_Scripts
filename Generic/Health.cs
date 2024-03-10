using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float totalHealth;
    private float currentHealth;

    public event Action<float> OnHealthChange;
    public event Action OnZeroHealth;

    private void Start() {
        currentHealth = totalHealth;
    }

    public void DealDamage(float _damage) {

        currentHealth -= _damage;
        OnHealthChange?.Invoke(currentHealth/totalHealth);


        if (currentHealth < 0) {
            //Soldier died
            OnZeroHealth?.Invoke();
        }
    }
}
