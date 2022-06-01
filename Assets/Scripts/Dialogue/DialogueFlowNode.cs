namespace Dialogue
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
}