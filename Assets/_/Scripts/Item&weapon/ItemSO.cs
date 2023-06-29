using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
  public string itemName;
  public ItemType itemType;
}

public enum ItemType
{
  Weapon, Armor, Potion, Scroll
}

