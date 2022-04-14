using System.Collections.Generic;
using Entity;
using Inventory;
using Item;
using Map;
using Player;
using UnityEngine;

namespace Manager
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private TilePropertyLayer[] tilePropertyLayers;
        [SerializeField] private PlayerInventoryHolder playerInventory;

        public Grid grid;
        private Camera _camera;
        private Dictionary<TileCoordinate, TilePropertyDetail> _tilePropertyDictionary;

        private void Start()
        {
            _camera = Camera.main;
            LoadTilePropertyLayer();
        }

        private void OnEnable()
        {
            SceneControllerManager.OnSceneLoaded += LoadGrid;
            playerInventory.OnItemUsed += UseItem;
        }

        private void OnDisable()
        {
            SceneControllerManager.OnSceneLoaded -= LoadGrid;
            playerInventory.OnItemUsed -= UseItem;
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

        private void UseItem(Vector3 mousePosition, ItemStack item)
        {
            if (item.ItemData == null || item.ItemData.IsUnusableItem()) return;

            Vector3Int cursorGridPosition = grid.WorldToCell(_camera.ScreenToWorldPoint(mousePosition));
            Vector3Int playerGridPosition = grid.WorldToCell(Player.Player.Instance.transform.position);

            int distance = ((Vector2Int)(cursorGridPosition - playerGridPosition)).sqrMagnitude;
            if (distance > 2) return;

            GameObject obj = GetObjectByGridPosition((Vector2Int)cursorGridPosition);
            if (obj != null && obj.GetComponent<Player.Player>() == null) return;

            switch (item.ItemData.ItemType)
            {
                case ItemType.Seed:
                    Vector3 cropSpawnPosition = grid.GetCellCenterWorld(cursorGridPosition);
                    CropEntity newCrop = Instantiate(
                        DataManager.GetPrefabByName("Crop"),
                        cropSpawnPosition,
                        Quaternion.identity
                    ).GetComponent<CropEntity>();

                    newCrop.Init(((SeedData)item.ItemData).CropData);

                    playerInventory.RemoveItemFromInventory(item.ItemData, 1);
                    break;
            }
        }

        private GameObject GetObjectByGridPosition(Vector2Int position)
        {
            Vector3 worldPosition = grid.GetCellCenterWorld(new Vector3Int(position.x, position.y, 0));
            return Physics2D.OverlapPoint(worldPosition)?.gameObject;
        }
    }
}