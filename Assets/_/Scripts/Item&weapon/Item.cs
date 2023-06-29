using UnityEngine;

public class Item : MonoBehaviour, ICollectable
{
  [SerializeField] private ItemSO _item;

  public ItemSO Collect() => _item;

  private void Start()
  {
    Debug.Log(_item.itemType);  
  }
}
