using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    void Start()
    {
        currentHealth = maxHealth;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        UpdateUI();
    }

    void Update()
    {
        // Hasar sonrasý geçici baðýþýklýk
        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;

            // Yanýp sönme efekti
            spriteRenderer.enabled = (Time.time * 10) % 2 < 1;

            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        UpdateUI();

        /*DÜZELECEK
        // Ses çal
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("hit");
        }
        */

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Geçici baðýþýklýk baþlat
            isInvincible = true;
            invincibilityTimer = 0f;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateUI();
    }

    void Die()
    {
        /* DÜZELECEK
        // Oyunu bitir
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        */

        // Oyuncuyu yok et
        Destroy(gameObject);
    }

    void UpdateUI()
    {
        /* DÜZELECEK
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHealth(currentHealth, maxHealth);
        }
        */
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
