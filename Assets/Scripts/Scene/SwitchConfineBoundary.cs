using Cinemachine;
using Manager;
using UnityEngine;

namespace Scene
{
    public class SwitchConfineBoundary : MonoBehaviour
    {
        private const string BoundaryConfinerTag = "BoundaryConfiner";

        private void OnEnable()
        {
            SceneControllerManager.OnSceneLoaded += SwitchBoundary;
        }

        private void OnDisable()
        {
            SceneControllerManager.OnSceneLoaded -= SwitchBoundary;
        }

        private void SwitchBoundary()
        {
            PolygonCollider2D bounds = GameObject.FindGameObjectWithTag(BoundaryConfinerTag).GetComponent<PolygonCollider2D>();

            CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
            confiner.m_BoundingShape2D = bounds;

            confiner.InvalidatePathCache();
        }
    }
}