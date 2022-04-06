using System.Collections;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObscuringEntityFader : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private const float FadeInSeconds = 0.25f;
        private const float FadeOutSeconds = 0.35f;
        private const float TargetAlpha = 0.45f;

        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
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
            float currentAlpha = _renderer.color.a;
            float distance = 1f - currentAlpha;

            while (1f - currentAlpha > 0.01f)
            {
                currentAlpha += distance / FadeInSeconds * Time.deltaTime;
                _renderer.color = new Color(1f, 1f, 1f, currentAlpha);
                yield return null;
            }

            _renderer.color = new Color(1f, 1f, 1f, 1f);
        }

        private IEnumerator FadeOutRoutine()
        {
            float currentAlpha = _renderer.color.a;
            float distance = currentAlpha - TargetAlpha;

            while (currentAlpha - TargetAlpha > 0.01f)
            {
                currentAlpha -= distance / FadeOutSeconds * Time.deltaTime;
                _renderer.color = new Color(1f, 1f, 1f, currentAlpha);
                yield return null;
            }

            _renderer.color = new Color(1f, 1f, 1f, TargetAlpha);
        }
    }
}
