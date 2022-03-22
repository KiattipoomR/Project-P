using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tile Data")]
public class TileData : ScriptableObject
{
   public TileBase[] tiles;
   public IInteractable interactScript;
}
