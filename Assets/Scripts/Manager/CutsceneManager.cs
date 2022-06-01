using UnityEngine;
using UnityEngine.SceneManagement;
using Dialogue;
using UnityEngine.Events;
using Entity;

namespace Manager
{
  public class CutsceneManager : MonoBehaviour
  {
    CutsceneEntry _currentCutsceneEntry;
    int _nextPlayingOrder = 0;

    public static UnityAction OnCutsceneStart;
    public static UnityAction OnCutsceneEnd;

    private void OnEnable()
    {
      SceneControllerManager.OnSceneLoaded += CheckOnLoadNewScene;
      SceneControllerManager.OnSceneFinishedFadeIn += CheckEventDialogue;
      DialogueManager.OnDialogueEnded += CheckEventDialogue;
    }

    private void OnDisable()
    {
      SceneControllerManager.OnSceneLoaded -= CheckOnLoadNewScene;
      SceneControllerManager.OnSceneFinishedFadeIn -= CheckEventDialogue;
      DialogueManager.OnDialogueEnded -= CheckEventDialogue;
    }

    private void CheckOnLoadNewScene()
    {
      _currentCutsceneEntry = DataManager.GetCutsceneEntryByName(SceneManager.GetActiveScene().name);
      _nextPlayingOrder = 0;

      if (_currentCutsceneEntry == null) return;
      switch (SceneManager.GetActiveScene().name)
      {
        case "01 - Beach":
          GameObject.Find("Player").GetComponent<Player.Player>().ChangeAnimationState("BodyIdleUp");
          break;

        case "04 - Tura's House":
          GameObject.Find("Nun").GetComponent<NpcEntity>().ChangeAnimationState("NunIdleUp");
          break;

        default:
          return;
      }
    }

    private void CheckEventDialogue()
    {
      if (_currentCutsceneEntry == null)
      {
        GameManager.Instance.soundManager.PlayBackgroundMusic(null, true);
        return;
      }
      GameManager.Instance.soundManager.PlayBackgroundMusic(_currentCutsceneEntry.backgroundMusic, false);
      if (_nextPlayingOrder < _currentCutsceneEntry.dialogueStartEnd.Length)
      {
        GameManager.Instance.dialogueManager.StartDialogue(_currentCutsceneEntry.dialogueStartEnd[_nextPlayingOrder], _currentCutsceneEntry.dialogueFile);
        _nextPlayingOrder++;
      }
      else
      {
        CheckAndLoadNextScene();
      }
    }

    private void CheckAndLoadNextScene()
    {
      if (_currentCutsceneEntry.nextScene != "")
      {
        GameManager.Instance.sceneControllerManager.ChangeScene(_currentCutsceneEntry.nextScene, _currentCutsceneEntry.spawnPoint);
      }
      _currentCutsceneEntry = null;
    }
  }
}