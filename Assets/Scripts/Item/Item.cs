using System;
using UnityEngine;

namespace Item
{
    public class Item : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}