using System.Collections.Generic;
using Crop;
using Item;
using UnityEngine;
using Dialogue;

namespace Manager
{
  public class DataManager : MonoBehaviour
  {
    private static Dictionary<string, ItemData> _itemDataDictionary;
    private static Dictionary<string, GameObject> _prefabDictionary;
    private static Dictionary<string, CropData> _cropDictionary;
    private static Dictionary<string, CutsceneEntry> _cutscenesDictionary;

    private void Awake()
    {
      LoadItemData();
      LoadPrefabData();
      LoadCutsceneData();

      // TODO : Remove later
      LoadCropData();
    }

    private static void LoadItemData()
    {
      _itemDataDictionary = new Dictionary<string, ItemData>();
      ItemData[] itemData = Resources.LoadAll<ItemData>("ScriptableObjects/Items");

      foreach (ItemData item in itemData)
      {
        if (item.ItemID == "") continue;
        _itemDataDictionary.Add(item.ItemID, item);
      }
    }

    private static void LoadPrefabData()
    {
      _prefabDictionary = new Dictionary<string, GameObject>();
      GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs");

      foreach (GameObject gameObject in gameObjects)
      {
        _prefabDictionary.Add(gameObject.name, gameObject);
      }
    }

    // TODO : Remove later
    private static void LoadCropData()
    {
      _cropDictionary = new Dictionary<string, CropData>();
      CropData[] cropData = Resources.LoadAll<CropData>("ScriptableObjects/Crops");

      foreach (CropData crop in cropData)
      {
        _cropDictionary.Add(crop.ID, crop);
      }
    }

    private static void LoadCutsceneData()
    {
      _cutscenesDictionary = new Dictionary<string, CutsceneEntry>();
      CutsceneEntry[] cutscenes = Resources.LoadAll<CutsceneEntry>("ScriptableObjects/Cutscenes");

      foreach (CutsceneEntry cutscene in cutscenes)
      {
        _cutscenesDictionary.Add(cutscene.sceneName, cutscene);
      }
    }

    // TODO : Remove later
    public static CropData GetCropDataByCropID(string cropID)
    {
      return _cropDictionary.TryGetValue(cropID, out CropData crop) ? crop : null;
    }

    public static ItemData GetItemDataByItemID(string itemID)
    {
      return _itemDataDictionary.TryGetValue(itemID, out ItemData item) ? item : null;
    }

    public static GameObject GetPrefabByName(string prefabName)
    {
      return _prefabDictionary.TryGetValue(prefabName, out GameObject prefab) ? prefab : null;
    }

    public static CutsceneEntry GetCutsceneEntryByName(string sceneName)
    {
      return _cutscenesDictionary.TryGetValue(sceneName, out CutsceneEntry cutscene) ? cutscene : null;
    }
  }
}