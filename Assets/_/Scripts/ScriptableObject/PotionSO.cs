using UnityEditor;
using UnityEngine;
using VictorUtilties;

[CreateAssetMenu(fileName = "New PotionSO", menuName = "+ New/ScripteableObject/Potion")]
public class PotionSO : ScriptableObject
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
}
public enum PotionType
{
  Health, Mana, Stamina, Upgrade
}
