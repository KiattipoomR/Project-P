using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
  [System.Serializable]
  [CreateAssetMenu(menuName = "Scriptable Object/Cutscene", fileName = "Dialogue_CutsceneEntry")]
  public class CutsceneEntry : ScriptableObject
  {
    public string sceneName;
    public TextAsset dialogueFile;
    public AudioClip backgroundMusic;
    public DialogueStartEnd[] dialogueStartEnd;
    public string nextScene;
  }
}
