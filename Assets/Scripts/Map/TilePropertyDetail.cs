using System.Collections.Generic;

namespace Map
{
    [System.Serializable]
    public class TilePropertyDetail
    {
        private TileCoordinate _coordinate;
        private Dictionary<TilePropertyType, bool> _properties;

        public TileCoordinate Coordinate => _coordinate;
        public Dictionary<TilePropertyType, bool> Properties => _properties;
        
        public TilePropertyDetail(TileCoordinate coordinate)
        {
            _coordinate = coordinate;
            _properties = new Dictionary<TilePropertyType, bool>();
        }
    }
    
}