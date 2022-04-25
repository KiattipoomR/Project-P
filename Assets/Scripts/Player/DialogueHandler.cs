using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class DialogueHandler : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RPGTalk rpgTalk;
        
        [Header("Attributes")]
        [SerializeField] private TextAsset textAsset;
        
        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                rpgTalk.NewTalk("1", "-1", textAsset);
            }
        }
    }
}