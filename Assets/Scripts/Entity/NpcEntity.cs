using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using NPC;
using Manager;

namespace Entity
{
  public class NpcEntity : MonoBehaviour
  {
    //[SerializeField] UnityEvent OnInteract;

    [SerializeField] NpcData npc;

    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;

    private bool interactable = false;

    private void Awake()
    {
      _renderer = GetComponentInChildren<SpriteRenderer>();
      _collider = GetComponent<BoxCollider2D>();

      if (!npc) Destroy(gameObject);
      _renderer.sprite = npc.NpcSprite;
      _collider.isTrigger = false;
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
      GameManager.Instance.dialogueManager.StartDialogue(npc.DialogueTextAsset);
    }
  }
}
