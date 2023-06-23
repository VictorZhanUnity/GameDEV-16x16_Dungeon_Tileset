using DG.Tweening;
using UnityEngine;

public class Potion : TriggerHandler
{
  [SerializeField, Space(10)] private PotionSO _potionSO;

  protected override void OnTriggerBegin(Collider2D target)
  {
    if (target.TryGetComponent(out PlayerController player))
    {
      player.CollectPotion(_potionSO);
    }
    ToDestroy();
  }

  protected void OnValidate()
  {
    base.OnValidate();
    if (iconSprite == null && _potionSO != null)
    {
      iconSprite = _potionSO.iconSprite;
    }
  }

  private void ToDestroy()
  {
    Destroy(gameObject);
  }
}



