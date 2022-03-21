using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct PlantStage {
    public ItemData crop;
    public Sprite sprite;
    public int timeToNextStage;
    public List<PlantFlag> statusNeed;
} 