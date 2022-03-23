using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MonoBehaviour
{
    [SerializeField] private Tilemap[] tilemaps;
    [SerializeField] private TileBase grass;
    [SerializeField] private TileBase dirt;
    [SerializeField] private GameObject plantParent;
    [SerializeField] private GameObject plant;
    [SerializeField] private ItemData hoe;
    [SerializeField] private ItemData seed;
    public static FarmManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    public void InteractWithTile(Transform player, Vector3 offset) {
        for (int i = 0; i < tilemaps.Length; i++) {
            Vector3Int gridPos = tilemaps[i].WorldToCell(player.position + offset);
            TileBase tb = tilemaps[i].GetTile(gridPos);
            if (tb) {
                if (tb == grass) {
                    if (player.gameObject.GetComponent<InventoryHolder>().InventoryManager.ContainsItem(hoe, out List<InventorySlot> invSlots)) {
                        tilemaps[i].SetTile(gridPos, dirt);
                    }
                }
                else if (tb == dirt) {
                    if (player.gameObject.GetComponent<InventoryHolder>().InventoryManager.ContainsItem(seed, out List<InventorySlot> invSlots)) {
                        Instantiate(plant, tilemaps[i].CellToWorld(gridPos) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, plantParent.transform);
                    }
                } 
                print(tb);
                return;
            }
        }
    }




}
