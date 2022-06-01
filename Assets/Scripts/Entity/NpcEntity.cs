using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using NPC;
using Manager;
using Dialogue;

namespace Entity
{
  public class NpcEntity : MonoBehaviour
  {
    //[SerializeField] UnityEvent OnInteract;

    [SerializeField] NpcData npc;

    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;

    private bool interactable = false;

    private int _currentDialogueState;

    private void Awake()
    {
      _renderer = GetComponentInChildren<SpriteRenderer>();
      _collider = GetComponent<BoxCollider2D>();

      if (!npc) Destroy(gameObject);
      _renderer.sprite = npc.NpcSprite;
      _collider.isTrigger = false;
      _currentDialogueState = npc.StartDialogueState;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.name == "Player") interactable = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
      if (other.gameObject.name == "Player") interactable = false;
    }

    private void Update()
    {
      if (!Keyboard.current.tKey.wasPressedThisFrame || !interactable) return;
      UpdateState();
      GameManager.Instance.dialogueManager.StartDialogue(GetNextDialogueLine(), npc.DialogueTextAsset);
    }

    private DialogueStartEnd GetNextDialogueLine()
    {
      return npc.DialogueFlowGraph[_currentDialogueState].startEndLine;
    }

    public void UpdateState()
    {
      foreach (DialogueFlowLink link in npc.DialogueFlowGraph[_currentDialogueState].links)
      {
        bool walkable = true;
        foreach (DialogueFlowLinkCondition condition in link.conditions)
        {
          if (!PlayerPrefs.HasKey(condition.choiceId) || PlayerPrefs.GetInt(condition.choiceId) != condition.value)
          {
            walkable = false;
            break;
          }
        }
        if (walkable)
        {
          _currentDialogueState = link.nextDialogueIndex;
          break;
        }
      }
    }
  }
}

