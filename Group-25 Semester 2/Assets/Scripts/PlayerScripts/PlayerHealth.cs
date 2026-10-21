
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHealth : MonoBehaviour, ISaveable
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;

    [Header("UI")]
    [SerializeField] private Slider healthBar;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private DamageFlash damageFlash;

    private bool isDead = false;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    [Serializable]
    private class HealthData
    {
        public float currentHealth;
    }

    private void Awake()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead || damage <= 0f)
            return;

        currentHealth = Mathf.Max(currentHealth - damage, 0f);

        Debug.Log("Player Took Damage: " + currentHealth);

        UpdateHealthUI();

        if (damageFlash != null)
            damageFlash.Flash();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void TakeHalfHealth()
    {
        float damage = currentHealth * 0.5f;
        TakeDamage(damage);
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("PLAYER DIED!");

        if (gameManager != null)
        {
            gameManager.PlayerDied();
        }
        else
        {
            Debug.LogError("PlayerHealth: GameManager is not assigned!");
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public string CaptureState()
    {
        HealthData data = new HealthData();
        data.currentHealth = currentHealth;

        return JsonUtility.ToJson(data);
    }

    public void RestoreState(string state)
    {
        HealthData data = JsonUtility.FromJson<HealthData>(state);

        if (data == null)
            return;

        currentHealth = Mathf.Clamp(
            data.currentHealth, 0f, maxHealth
        );

        isDead = false;

        UpdateHealthUI();
    }

    public void RestoreHealth(float amount)
    {
        if (amount <= 0f || currentHealth <= 0f)
            return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
    }
}