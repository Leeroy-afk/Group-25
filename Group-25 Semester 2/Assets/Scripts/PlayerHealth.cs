using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;
    
    [Header("UI")]
    [SerializeField] private Slider healthBar;
    private void Awake()
    {
        currentHealth = maxHealth; 

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0f);

        Debug.Log("Player Took Damage: " + currentHealth);

        healthBar.value = currentHealth;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }
}


