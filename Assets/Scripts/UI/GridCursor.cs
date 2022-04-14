using Inventory;
using Manager;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GridCursor : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private RectTransform cursorTransform;
        [SerializeField] private Image cursorImage;
        [SerializeField] private Sprite gridAvailableIcon;
        [SerializeField] private Sprite gridUnavailableIcon;
        [SerializeField] private PlayerInventoryHolder playerInventory;

        private Canvas _canvas;
        private Camera _camera;
        private bool _isPaused, _cursorIsEnabled;

        private void Start()
        {
            _camera = Camera.main;
            _canvas = GetComponentInParent<Canvas>();
            SetEnableCursor(true);
        }

        private void OnEnable()
        {
            PauseManager.OnPauseTriggered += SetInactiveGridCursor;
            SceneControllerManager.OnSceneLoaded += LoadGrid;
            Player.Player.OnAimed += ShowCursor;
            playerInventory.OnItemFocused += CheckFocusItem;
        }

        private void OnDisable()
        {
            PauseManager.OnPauseTriggered -= SetInactiveGridCursor;
            SceneControllerManager.OnSceneLoaded -= LoadGrid;
            Player.Player.OnAimed -= ShowCursor;
            playerInventory.OnItemFocused -= CheckFocusItem;
        }

        private void LoadGrid()
        {
            grid = FindObjectOfType<Grid>();
        }

        private void ShowCursor(Vector3 mousePosition)
        {
            if (_isPaused || !_cursorIsEnabled || grid == null) return;

            Vector3Int cursorGridPosition = grid.WorldToCell(_camera.ScreenToWorldPoint(mousePosition));
            Vector3Int playerGridPosition = grid.WorldToCell(Player.Player.Instance.transform.position);

            CheckCursorAvailability(cursorGridPosition, playerGridPosition);

            cursorTransform.position = GetCursorRectTransformPosition(cursorGridPosition);
        }

        private void CheckCursorAvailability(Vector3Int cursorGridPosition, Vector3Int playerGridPosition)
        {
            SetCursorAvailability(true);

            int distance = ((Vector2Int)(cursorGridPosition - playerGridPosition)).sqrMagnitude;
            if (distance > 2)
            {
                SetCursorAvailability(false);
                return;
            }

            GameObject obj = GetObjectByGridPosition((Vector2Int)cursorGridPosition);
            if (obj != null && obj.GetComponent<Player.Player>() == null) SetCursorAvailability(false);
        }

        private void SetCursorAvailability(bool isAvailable)
        {
            cursorImage.sprite = isAvailable ? gridAvailableIcon : gridUnavailableIcon;
        }

        private GameObject GetObjectByGridPosition(Vector2Int position)
        {
            Vector3 worldPosition = grid.GetCellCenterWorld(new Vector3Int(position.x, position.y, 0));
            return Physics2D.OverlapPoint(worldPosition)?.gameObject;
        }

        private Vector2 GetCursorRectTransformPosition(Vector3Int gridPosition)
        {
            Vector3 gridWorldPosition = grid.CellToWorld(gridPosition);
            Vector2 gridScreenPosition = _camera.WorldToScreenPoint(gridWorldPosition);
            return RectTransformUtility.PixelAdjustPoint(gridScreenPosition, cursorTransform, _canvas);
        }

        private void CheckFocusItem(ItemStack item)
        {
            if (item.ItemData == null || item.ItemData.IsUnusableItem())
            {
                SetEnableCursor(false);
                return;
            }

            SetEnableCursor(true);
        }

        private void SetEnableCursor(bool isEnabled)
        {
            cursorImage.color = isEnabled ? Color.white : Color.clear;
            _cursorIsEnabled = isEnabled;
        }

        private void SetInactiveGridCursor(bool isInactive)
        {
            _isPaused = isInactive;
            // May change to setActive the cursor component instead. Hmm
            SetEnableCursor(!isInactive);
        }
    }
}