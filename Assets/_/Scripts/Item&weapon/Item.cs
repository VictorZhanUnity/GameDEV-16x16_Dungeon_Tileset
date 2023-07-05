using UnityEngine;

public class Item : MonoBehaviour
{
  [SerializeField] private ItemSO _item;

  public ItemSO Collect() => _item;
}
