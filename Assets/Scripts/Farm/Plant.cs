using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Plant : MonoBehaviour
{
    private SpriteRenderer spriteRenderer => gameObject.GetComponent<SpriteRenderer>();
    private Collider2D plantCollider => gameObject.GetComponent<Collider2D>();

    [SerializeField] private int currentStage;
    [SerializeField] private int timeToNextStage;
    [SerializeField] private PlantStage[] plantStages;

    private void Start()
    {
        SetupNewStage();
    }

    private void UpdateTimeToNextStage() {
        if (timeToNextStage > 0) timeToNextStage--;
        if (timeToNextStage == 0 && currentStage < plantStages.Length - 1) {
            currentStage++;
            SetupNewStage();
        } 
    }

    private void SetupNewStage() {
        spriteRenderer.sprite = plantStages[currentStage].sprite;
        timeToNextStage = plantStages[currentStage].timeToNextStage;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        UpdateTimeToNextStage();
    }
}