using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    // Components
    [SerializeField] Slider slider;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        HealthManager.onHealthChange += SetHealth;
        HealthManager.onMaxHealthChange += SetMaxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        SetHealth(health);
    }
}
