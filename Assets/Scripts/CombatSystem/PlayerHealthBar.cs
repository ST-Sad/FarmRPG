using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Slider healthBar;
    private CharacterStats characterStats;
    private float lastDamageTime;
    private float hideDelay = 5f;

    void Start()
    {
        healthBar = GetComponent<Slider>();
        characterStats = GetComponentInParent<CharacterStats>();

        if (characterStats == null)
        {
            Debug.LogError("CharacterStats not found in parent of " + gameObject.name);
            return;
        }

        gameObject.SetActive(false);
        characterStats.OnTakeDamage += OnPlayerTakeDamage;
    }

    void Update()
    {
        if (characterStats == null) return;

        healthBar.maxValue = characterStats.maxHealth;
        healthBar.value = characterStats.currentHealth;

        if (Time.time - lastDamageTime >= hideDelay && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        if (characterStats.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnPlayerTakeDamage()
    {
        lastDamageTime = Time.time;
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    void OnDestroy()
    {
        if (characterStats != null)
        {
            characterStats.OnTakeDamage -= OnPlayerTakeDamage;
        }
    }
}