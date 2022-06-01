using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dialogue;

namespace Manager
{
  public class CutsceneManager : MonoBehaviour
  {
    CutsceneEntry _currentCutsceneEntry;
    int _nextPlayingOrder = 0;

    private void OnEnable()
    {
      SceneControllerManager.OnSceneLoaded += CheckOnLoadNewScene;
      SceneControllerManager.OnScreenFinishedFadeIn += CheckEventDialogue;
      DialogueManager.OnDialogueEnded += CheckEventDialogue;
    }

    private void OnDisable()
    {
      SceneControllerManager.OnSceneLoaded -= CheckOnLoadNewScene;
      SceneControllerManager.OnScreenFinishedFadeIn -= CheckEventDialogue;
      DialogueManager.OnDialogueEnded -= CheckEventDialogue;
    }

    private void CheckOnLoadNewScene()
    {
      _currentCutsceneEntry = DataManager.GetCutsceneEntryByName(SceneManager.GetActiveScene().name);
      _nextPlayingOrder = 0;
    }

    private void CheckEventDialogue()
    {
      if (_currentCutsceneEntry == null) return;
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
        GameManager.Instance.sceneControllerManager.ChangeScene(_currentCutsceneEntry.nextScene, new Vector3());
      }
    }
  }
}