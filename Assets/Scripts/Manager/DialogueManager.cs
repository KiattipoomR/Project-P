using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Manager
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RPGTalk rpgTalk;
        
        [Header("Attributes")]
        [SerializeField] private TextAsset textAsset;

        public static UnityAction OnDialogueStarted;
        public static UnityAction OnDialogueEnded;
        
        private void Update()
        {
            if (!Keyboard.current.tKey.wasPressedThisFrame) return;
            
            OnDialogueStarted?.Invoke();
            rpgTalk.NewTalk("1", "-1", textAsset);
            
        }

        public void EndDialogue()
        {
            OnDialogueEnded?.Invoke();
        }
    }
}