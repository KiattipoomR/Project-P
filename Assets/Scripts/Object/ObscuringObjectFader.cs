using System.Collections;
using UnityEngine;

namespace Object
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObscuringObjectFader : MonoBehaviour
    {
        private SpriteRenderer _sprite;

        private const float FadeInSeconds = 0.25f;
        private const float FadeOutSeconds = 0.35f;
        private const float TargetAlpha = 0.45f;

        private void Awake()
        {
            _sprite = gameObject.GetComponent<SpriteRenderer>();
        }

        public void FadeIn()
        {
            StartCoroutine(FadeInRoutine());
        }

        public void FadeOut()
        {
            StartCoroutine(FadeOutRoutine());
        }

        private IEnumerator FadeInRoutine()
        {
            float currentAlpha = _sprite.color.a;
            float distance = 1f - currentAlpha;

            while (1f - currentAlpha > 0.01f)
            {
                currentAlpha += distance / FadeOutSeconds * Time.deltaTime;
                _sprite.color = new Color(1f, 1f, 1f, currentAlpha);
                yield return null;
            }

            _sprite.color = new Color(1f, 1f, 1f, 1f);
        }

        private IEnumerator FadeOutRoutine()
        {
            float currentAlpha = _sprite.color.a;
            float distance = currentAlpha - TargetAlpha;

            while (currentAlpha - TargetAlpha > 0.01f)
            {
                currentAlpha -= distance / FadeOutSeconds * Time.deltaTime;
                _sprite.color = new Color(1f, 1f, 1f, currentAlpha);
                yield return null;
            }

            _sprite.color = new Color(1f, 1f, 1f, TargetAlpha);
        }
    }
}
