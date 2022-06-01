using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace NPC
{
  [System.Serializable]
  public struct DialogueStartEnd
  {
    public string startLine;
    public string endLine;
  }

  [System.Serializable]
  public struct DialogueFlowLinkCondition
  {
    public string choiceId;
    public int value;
  }

  [System.Serializable]
  public struct DialogueFlowLink
  {
    public DialogueFlowLinkCondition[] conditions;
    public int nextDialogueIndex;
  }

  [System.Serializable]
  public struct DialogueFlowNode
  {
    public DialogueStartEnd startEndLine;
    public DialogueFlowLink[] links;
  }

  [System.Serializable]
  [CreateAssetMenu(menuName = "Scriptable Object/NPC", fileName = "NPC_NpcData")]
  public class NpcData : ScriptableObject
  {
    [SerializeField] protected string npcName;
    [SerializeField] protected Sprite npcSprite;
    [SerializeField] TextAsset dialogueTextAsset;
    [SerializeField] DialogueFlowNode[] dialogueFlowGraph;
    [SerializeField] int startDialogueState = 0;

    public string NpcName => npcName;
    public Sprite NpcSprite => npcSprite;
    public TextAsset DialogueTextAsset => dialogueTextAsset;
    public DialogueFlowNode[] DialogueFlowGraph => dialogueFlowGraph;
    public int StartDialogueState => startDialogueState;
  }
}