using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [CreateAssetMenu(menuName = "Scriptable Object/Tile Property", fileName = "")]
    public class TilePropertyLayer : ScriptableObject
    {
        public string sceneName;
        public int mapWidth;
        public int mapHeight;
        public int originX;
        public int originY;

        [SerializeField] public List<TileProperty> tileProperty;
    }
}