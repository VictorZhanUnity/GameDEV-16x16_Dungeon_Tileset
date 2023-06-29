using UnityEditor;
using UnityEngine;
using VictorUtilties;

[CreateAssetMenu(fileName = "New PotionSO", menuName = "+ New/ScripteableObject/Potion")]
public class PotionSO : ItemSO
{
  public Sprite iconSprite;
  public PotionType potionType;
  public float power;

  [MenuItem("ScriptableObject/Create/PotionSO")]
  public static PotionSO Create_PotionSO()
  {
    string assetPath = "ScriptableObject/PotionSO/";
    return Utility.CreateScriptableObject<PotionSO>(assetPath, "PotionSO");
  }

  private void OnValidate()
  {
    if (iconSprite != null && itemName.Trim().Length == 0)
    {
      itemName = iconSprite.name;
    }
  }
}
public enum PotionType
{
  Health, Mana, Stamina, Upgrade
}
