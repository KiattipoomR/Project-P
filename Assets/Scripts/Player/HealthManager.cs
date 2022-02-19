using UnityEngine;
using System;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        MaxHealthChange(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddHealth(-20);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            AddHealth(10);
        }
    }

    // Method for changing health. Positive for a heal, negative for a damage.
    void AddHealth(int health)
    {
        currentHealth += health;
        if (currentHealth < 0) currentHealth = 0;
        else if (currentHealth > maxHealth) currentHealth = maxHealth;

        HealthChange(currentHealth);
    }

    public static event Action<int> onHealthChange;
    public void HealthChange(int health)
    {
        onHealthChange?.Invoke(health);
    }

    public static event Action<int> onMaxHealthChange;
    public void MaxHealthChange(int health)
    {
        onMaxHealthChange?.Invoke(health);
    }
}
