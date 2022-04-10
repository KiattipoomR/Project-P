namespace Map
{
    [System.Serializable]
    public class TileProperty
    {
        public TileCoordinate coordinate;
        public TilePropertyType propertyType;
        public bool propertyValue;

        public TileProperty(TileCoordinate tileCoordinate, TilePropertyType gridPropertyType, bool gridPropertyValue)
        {
            coordinate = tileCoordinate;
            propertyType = gridPropertyType;
            propertyValue = gridPropertyValue;
        }
    }

    [System.Serializable]
    public enum TilePropertyType
    {
        IsDiggable,
    }
}