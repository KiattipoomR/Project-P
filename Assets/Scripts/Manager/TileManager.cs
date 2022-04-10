using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Manager
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private TilePropertyLayer[] tilePropertyLayers;

        public Grid grid;
        private Dictionary<TileCoordinate, TilePropertyDetail> _tilePropertyDictionary;

        private void Start()
        {
            LoadTilePropertyLayer();
        }

        private void OnEnable()
        {
            SceneControllerManager.OnSceneLoaded += LoadGrid;
        }

        private void OnDisable()
        {
            SceneControllerManager.OnSceneLoaded -= LoadGrid;
        }

        private void LoadGrid()
        {
            grid = FindObjectOfType<Grid>();
        }

        private void LoadTilePropertyLayer()
        {
            foreach (TilePropertyLayer layer in tilePropertyLayers)
            {
                Dictionary<TileCoordinate, TilePropertyDetail> tilePropertyDictionary = new Dictionary<TileCoordinate, TilePropertyDetail>();

                foreach (TileProperty tileProperty in layer.tileProperty)
                {
                    TileCoordinate tileCoordinate =
                        new TileCoordinate(tileProperty.coordinate.X, tileProperty.coordinate.Y);
                    
                    TilePropertyDetail tilePropertyDetail = GetTilePropertyDetail(tilePropertyDictionary, tileCoordinate) ??
                                                            new TilePropertyDetail(tileCoordinate);

                    tilePropertyDetail.Properties.Add(tileProperty.propertyType, tileProperty.propertyValue);

                    SetTilePropertyDetail(tilePropertyDictionary, tileCoordinate, tilePropertyDetail);
                }

                if (layer.sceneName == GameManager.Instance.sceneControllerManager.StartingSceneName)
                {
                    _tilePropertyDictionary = tilePropertyDictionary;
                }
            }
        }

        private TilePropertyDetail GetTilePropertyDetail(Dictionary<TileCoordinate, TilePropertyDetail> tilePropertyDictionary, TileCoordinate tileCoordinate)
        {
            return tilePropertyDictionary.TryGetValue(tileCoordinate, out TilePropertyDetail tilePropertyDetail) ? tilePropertyDetail : null;
        }

        private void SetTilePropertyDetail(Dictionary<TileCoordinate, TilePropertyDetail> tilePropertyDictionary, TileCoordinate tileCoordinate, TilePropertyDetail tilePropertyDetail)
        {
            tilePropertyDictionary.Add(tileCoordinate, tilePropertyDetail);
        }
        private void SetTilePropertyDetail(TileCoordinate tileCoordinate, TilePropertyDetail tilePropertyDetail)
        {
            _tilePropertyDictionary.Add(tileCoordinate, tilePropertyDetail);
        }
    }
}