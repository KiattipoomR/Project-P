using UnityEngine;

[CreateAssetMenu(menuName = "Game Item/Item Data")]
public class ItemData : ScriptableObject
{
    public int id;
    public new string name;
    public int maxStackSize;
    public int runeValue;
}
