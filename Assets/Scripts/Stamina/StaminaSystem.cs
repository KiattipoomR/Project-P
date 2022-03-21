using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider staminaBar;

    public TextMeshProUGUI UI_MAX_STAMINA_TEXT;
    public TextMeshProUGUI UI_CURRENT_STAMINA_TEXT;

    private int maxStamina = 100;
    private int currentStamina;

    public static StaminaSystem instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        // UI_MAX_STAMINA_TEXT.text = currentStamina.ToString();
        UI_CURRENT_STAMINA_TEXT.text = currentStamina.ToString();
    }

    public void UseStamina(int amount)
    {
        int tmpStamina = currentStamina - amount;
        if (tmpStamina >= 0)
        {
            currentStamina = tmpStamina;
            staminaBar.value = currentStamina;
            UI_CURRENT_STAMINA_TEXT.text = currentStamina.ToString();
        }
        else
        {
            print("Not enough stamina");
            currentStamina = 0;
            staminaBar.value = currentStamina;
            UI_CURRENT_STAMINA_TEXT.text = currentStamina.ToString();
        }
    }

    public void RecoverStamina(int amount)
    {
        int tmpStamina = currentStamina + amount;
        if (tmpStamina <= 100)
        {
            currentStamina = tmpStamina;
            staminaBar.value = currentStamina;
            UI_CURRENT_STAMINA_TEXT.text = currentStamina.ToString();
        }
        else
        {
            print("Full recover stamina");
            currentStamina = 100;
            staminaBar.value = currentStamina;
            UI_CURRENT_STAMINA_TEXT.text = currentStamina.ToString();
        }
    }

}
