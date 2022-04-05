using UnityEngine;

public class TriggerObscuringObjectFader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ObscuringObjectFader[] obscuringObjects = other.gameObject.GetComponentsInChildren<ObscuringObjectFader>();
        if (obscuringObjects.Length < 1) return;

        foreach (ObscuringObjectFader obscuringObject in obscuringObjects)
        {
            obscuringObject.FadeOut();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ObscuringObjectFader[] obscuringObjects = other.gameObject.GetComponentsInChildren<ObscuringObjectFader>();
        if (obscuringObjects.Length < 1) return;

        foreach (ObscuringObjectFader obscuringObject in obscuringObjects)
        {
            obscuringObject.FadeIn();
        }
    }
}