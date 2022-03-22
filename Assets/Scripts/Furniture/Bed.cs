using UnityEngine;

public class Bed : MonoBehaviour, IInteractable 
{
    private void OnTriggerEnter2D(Collider2D other) {
        interact();
    }

    public void interact() {
        Date.instance.AddDay();
    }
}