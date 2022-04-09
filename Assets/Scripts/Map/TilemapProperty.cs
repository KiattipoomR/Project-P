using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    [ExecuteAlways]
    public class TilemapProperty : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private TilePropertyLayer tilePropertyLayer;
        [SerializeField] private TilePropertyType tilePropertyType;

        private Tilemap _tilemap;
        private Grid _grid;

        private void OnEnable()
        {
            if (Application.IsPlaying(gameObject)) return;

            _tilemap = GetComponent<Tilemap>();
            if (tilePropertyLayer != null) tilePropertyLayer.tileProperty.Clear();
        }

        private void OnDisable()
        {
            if (Application.IsPlaying(gameObject)) return;

            UpdateProperties();

            if (tilePropertyLayer != null) EditorUtility.SetDirty(tilePropertyLayer);
        }

        private void UpdateProperties()
        {
            _tilemap.CompressBounds();

            if (Application.IsPlaying(gameObject) || tilePropertyLayer == null) return;

            BoundsInt cellBounds = _tilemap.cellBounds;
            Vector3Int startCell = cellBounds.min;
            Vector3Int endCell = cellBounds.max;

            for (int x = startCell.x; x < endCell.x; x++)
            {
                for (int y = startCell.y; y < endCell.y; y++)
                {
                    TileBase tile = _tilemap.GetTile(new Vector3Int(x, y, 0));
                    if (tile == null) continue;

                    tilePropertyLayer.tileProperty.Add(new TileProperty(new TileCoordinate(x, y), tilePropertyType, true));
                }
            }
        }
    }
}