using UnityEngine;

namespace Entity
{
    public class ItemEntity : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}