using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;     // Any Health (player, enemy, boss)
    [SerializeField] private Image fillImage;   // The Fill image

    private void Awake()
    {
        if (health == null)
            Debug.LogWarning("HealthBarUI: Health reference is not assigned.", this);

        if (fillImage == null)
            Debug.LogError("HealthBarUI: Fill Image is not assigned.", this);
    }

    private void Update()
    {
        if (health == null || fillImage == null) return;

        // Health already gives us a normalized value (0–1)
        fillImage.fillAmount = health.GetHP01();
    }

    // Optional: allows assigning health by code later
    public void SetHealth(Health newHealth)
    {
        health = newHealth;
    }
}
