using UnityEngine;

namespace Map
{
    [System.Serializable]
    public struct TileCoordinate
    {
        [SerializeField] private int x;
        [SerializeField] private int y;

        public int X => x;
        public int Y => y;

        public TileCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static explicit operator Vector2(TileCoordinate tile)
        {
            return new Vector2(tile.X, tile.Y);
        }

        public static explicit operator Vector3(TileCoordinate tile)
        {
            return new Vector3(tile.X, tile.Y, 0f);
        }

        public static explicit operator Vector3Int(TileCoordinate tile)
        {
            return new Vector3Int(tile.X, tile.Y, 0);
        }
    }
}