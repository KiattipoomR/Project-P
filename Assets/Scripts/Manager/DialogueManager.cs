using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Manager
{
  public class DialogueManager : MonoBehaviour
  {
    [Header("Components")]
    [SerializeField] private RPGTalk rpgTalk;

    public static UnityAction OnDialogueStarted;
    public static UnityAction OnDialogueEnded;

    bool _dialogueInProcess;

    private void Start()
    {
      _dialogueInProcess = false;
    }

    public void StartDialogue(TextAsset dialogueTextAsset)
    {
      if (_dialogueInProcess) return;
      _dialogueInProcess = true;
      OnDialogueStarted?.Invoke();
      rpgTalk.NewTalk("TestChoice_Start", "TestChoice_End", dialogueTextAsset);
    }

    public void EndDialogue()
    {
      _dialogueInProcess = false;
      OnDialogueEnded?.Invoke();
    }
  }
}