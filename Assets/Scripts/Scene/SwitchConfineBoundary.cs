using UnityEngine;
using Cinemachine;

public class SwitchConfineBoundary : MonoBehaviour
{
    void Start()
    {
        SwitchBoundary();
    }

    private void SwitchBoundary()
    {
        PolygonCollider2D bounds = GameObject.FindGameObjectWithTag("BoundaryConfiner").GetComponent<PolygonCollider2D>();

        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = bounds;

        confiner.InvalidatePathCache();
    }
}
