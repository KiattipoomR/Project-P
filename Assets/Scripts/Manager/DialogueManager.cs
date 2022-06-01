using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Dialogue;

namespace Manager
{
  public class DialogueManager : MonoBehaviour
  {
    [Header("Components")]
    [SerializeField] private RPGTalk rpgTalk;

    public static UnityAction OnDialogueStarted;
    public static UnityAction OnDialogueEnded;
    public static UnityAction<string, int> OnSaveChoice;

    bool _dialogueInProcess;

    private void Start()
    {
      _dialogueInProcess = false;
    }

    private void OnEnable()
    {
      rpgTalk.OnMadeChoice += SaveChoice;
    }

    private void OnDisable()
    {
      rpgTalk.OnMadeChoice -= SaveChoice;
    }

    private void SaveChoice(string questionId, int choiceId)
    {
      Debug.Log("Save choice " + questionId + " " + choiceId);
      OnSaveChoice?.Invoke(questionId, choiceId);
    }

    public void StartDialogue(DialogueStartEnd dialogueStartEnd, TextAsset dialogueTextAsset)
    {
      if (_dialogueInProcess) return;
      _dialogueInProcess = true;
      OnDialogueStarted?.Invoke();
      rpgTalk.NewTalk(dialogueStartEnd.startLine, dialogueStartEnd.endLine, dialogueTextAsset);
    }

    public void EndDialogue()
    {
      _dialogueInProcess = false;
      OnDialogueEnded?.Invoke();
    }
  }
}