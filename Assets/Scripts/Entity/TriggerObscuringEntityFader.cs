using UnityEngine;

namespace Entity
{
    public class TriggerObscuringEntityFader : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            ObscuringEntityFader[] obscuringEntities = other.gameObject.GetComponentsInChildren<ObscuringEntityFader>();
            if (obscuringEntities.Length < 1) return;

            foreach (ObscuringEntityFader obscuringEntity in obscuringEntities)
            {
                obscuringEntity.FadeOut();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ObscuringEntityFader[] obscuringEntities = other.gameObject.GetComponentsInChildren<ObscuringEntityFader>();
            if (obscuringEntities.Length < 1) return;

            foreach (ObscuringEntityFader obscuringEntity in obscuringEntities)
            {
                obscuringEntity.FadeIn();
            }
        }
    }
}