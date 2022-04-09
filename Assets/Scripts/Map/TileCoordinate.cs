using UnityEngine;

namespace Map
{
    [System.Serializable]
    public struct TileCoordinate
    {
        private int _x;
        private int _y;

        public int X => _x;
        public int Y => _y;

        public TileCoordinate(int x, int y)
        {
            _x = x;
            _y = y;
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