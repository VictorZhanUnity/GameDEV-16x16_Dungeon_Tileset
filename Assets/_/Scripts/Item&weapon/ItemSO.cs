using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
  public ItemType itemType;
}

public enum ItemType
{
  Weapon, Armor, Potion, Scroll
}

