using Manager;
using UnityEngine;

namespace Scene
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SceneTeleport : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private string destinationSceneName;
        [SerializeField] private Vector3 destinationSpawnPoint = new Vector3();

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player.Player player = other.GetComponent<Player.Player>();
            if (player == null) return;

            float xPos = Mathf.Approximately(destinationSpawnPoint.x, 0f)
                ? player.transform.position.x
                : destinationSpawnPoint.x;
            float yPos = Mathf.Approximately(destinationSpawnPoint.y, 0f)
                ? player.transform.position.y
                : destinationSpawnPoint.y;

            GameManager.Instance.sceneControllerManager.ChangeScene(destinationSceneName, new Vector3(xPos, yPos, 0f));
        }
    }
}
