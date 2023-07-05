using System;
using Unity.VisualScripting;

[Serializable]
public class InventoryManager
{
  private PlayerController _playerController;

  public InventoryManager(PlayerController playerController)
  {
    _playerController = playerController;
  }

  public void CollectItem(ItemSO item)
  {
    switch (item.itemType)
    {
      case ItemType.Weapon:
      case ItemType.Armor:
        break;
      case ItemType.Potion: DrinkPotion(item as PotionSO); break;
      case ItemType.Scroll:
        break;
      default:
        break;
    }
  }

  private void DrinkPotion(PotionSO potionSO)
  {
    switch (potionSO.potionType)
    {
      case PotionType.Health: _playerController.AdjustHealth(potionSO.power); break;
      case PotionType.Mana: _playerController.AdjustMana(potionSO.power); break;
      case PotionType.Stamina: _playerController.AdjustStamina(potionSO.power); break;
      case PotionType.Upgrade:
        break;
      default: break;
    }
  }
}
