using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage; // Arrastra aquí la barra roja (Fill)

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}