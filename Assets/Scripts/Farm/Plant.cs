using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{
    private SpriteRenderer spriteRenderer => gameObject.GetComponent<SpriteRenderer>();

    [SerializeField] private int currentStage;
    [SerializeField] private int timeToNextStage;
    [SerializeField] private PlantStage[] plantStages;
    [SerializeField] private List<PlantFlag> currentStatus;

    private void Awake() {
        TempTime.instance.OnDayChanged.AddListener(UpdateTimeToNextStage);
    }

    private void Start()
    {
        SetupNewStage();
    }

    private void UpdateTimeToNextStage() {
        currentStatus.Clear();
        if (timeToNextStage > 0 && currentStatus.SequenceEqual(plantStages[currentStage].statusNeed)) timeToNextStage--;
        if (timeToNextStage == 0 && currentStage < plantStages.Length - 1) {
            currentStage++;
            SetupNewStage();
        } 
    }

    private void SetupNewStage() {
        spriteRenderer.sprite = plantStages[currentStage].sprite;
        timeToNextStage = plantStages[currentStage].timeToNextStage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var inventoryHolder = other.transform.GetComponent<InventoryHolder>();
        if (!inventoryHolder) return;

        ItemData water = Resources.Load<ItemData>("Water");
        if (!currentStatus.Contains(PlantFlag.WATER) && inventoryHolder.InventoryManager.ContainsItem(water, out var invSlots))
        {
            currentStatus.Add(PlantFlag.WATER);
        }
    }
}