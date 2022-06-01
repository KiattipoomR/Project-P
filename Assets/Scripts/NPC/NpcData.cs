using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
  [System.Serializable]
  [CreateAssetMenu(menuName = "Scriptable Object/NPC", fileName = "NPC_NpcData")]
  public class NpcData : ScriptableObject
  {
    [SerializeField] protected string npcName;
    [SerializeField] protected Sprite npcSprite;
    [SerializeField] TextAsset dialogueTextAsset;

    public string NpcName => npcName;
    public Sprite NpcSprite => npcSprite;
    public TextAsset DialogueTextAsset => dialogueTextAsset;
  }
}