using System;
using static CharacterSO;
using static SpriteSheet_Adapter;

[Serializable]
public class CharacterDataSetting
{
  public static CharacterSO_Data GetCharacterStatus(SpriteSheetItem item)
  {
    CharacterSO_Data result = new CharacterSO_Data()
    {
      characterName = item.characterName,
      sprite = item.iconSprite,
      animatorController = item.animatorController,
      //temp 暫時設定，之後要用外部cvs檔設定
      healthPoint = TempGetRandomValue(item, 10, 15), 
      baseAttack = TempGetRandomValue(item, 1, 1.3f),
      baseAttackInterval = TempGetRandomValue(item, 1, 1.2f),
      baseMoveSpeed = TempGetRandomValue(item, 1, 2f),
    };
    return result;
  }

  private static float TempGetRandomValue(SpriteSheetItem item, float minValue, float maxValue)
  {
    int multiple = 1;
    if (item.characterName.Contains("Boss")) multiple = 10;
    else if (item.characterName.Contains("2")) multiple = 2;
    else if (item.characterName.Contains("3")) multiple = 3;
    minValue *= multiple;
    maxValue *= multiple;
    return UnityEngine.Random.Range(minValue, maxValue);
  }
}
