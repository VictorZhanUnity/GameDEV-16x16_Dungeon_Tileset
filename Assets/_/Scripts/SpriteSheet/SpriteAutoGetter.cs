using System;
using System.Collections.Generic;
using UnityEngine;
using static SpriteSheet_Adapter;

/********************************************************************
 * SpriteSheet裡，圖檔檔名格式：{header}{角色名}_{動作名}_{序號}
 * ******************************************************************/
[Serializable]
public class SpriteAutoGetter
{
  [Tooltip("SpriteSheet檔案")]
  [SerializeField] private Sprite _spriteSheetFile;
  [Tooltip("放在Resources內的資料夾名稱")]
  [SerializeField] private string _resourceFoldeName = "SpriteSheet";
  [Tooltip("SpriteSheet檔案")]
  [SerializeField] private string _targetHeader = "sp_";

  /// <summary>
  /// 擷取SpriteSheet上的所有包含Header的Sprite
  /// </summary>
  public List<Sprite> GetResourceSprites()
  {
    List<Sprite> result = new List<Sprite>();
    string fileUrl = $"{_resourceFoldeName}/{_spriteSheetFile.texture.name}";
    Sprite[] sprites = Resources.LoadAll<Sprite>(fileUrl);
    if (sprites.Length == 0)
    {
      Debug.LogWarning($">>> 无法加载{_spriteSheetFile.texture.name}的sprite！");
      return null;
    }
    foreach (Sprite sprite in sprites)
    {
      if (sprite.name.Contains(_targetHeader))
      {
        result.Add(sprite);
      }
    }

    if (result.Count == 0)
    {
      Debug.LogWarning($">>> 並無包含{_targetHeader}的sprite！");
      return null;
    }
    else
    {
      Debug.Log($">>> Resources資料夾：{fileUrl}");
      Debug.Log($">>> 包含標題[  {_targetHeader} ] - 總共: {result.Count} 張Sprites");
    }

    return result;
  }

  /// <summary>
  /// 依照{角色名}_{動作}_{序號}進行歸類成Dictionary
  /// </summary>
  public Dictionary<string, SpriteSheetItem> ClassficationSprites(List<Sprite> headerSpritesList)
  {
    Dictionary<string, SpriteSheetItem> result = new Dictionary<string, SpriteSheetItem>();
    string character, action;
    foreach (Sprite sprite in headerSpritesList)
    {
      character = sprite.name.Split('_')[1]; //角色名
      action = sprite.name.Split('_')[2];   //動作名

      // 依照character建立SpriteSheetItem
      if (result.ContainsKey(character) == false)
      {
        result[character] = new SpriteSheetItem()
        {
          characterName = character,
          iconSprite = sprite
        };
      }

      // 依照action建立SpriteSheetItem裡的各個spriteDict，並儲存sprite
      if (result[character].actionSpriteDict.ContainsKey(action) == false)
      {
        result[character].actionSpriteDict[action] = new List<Sprite>();
      }
      result[character].actionSpriteDict[action].Add(sprite);
    }

    //顯示已擷取的Sprite
    foreach (string characterKey in result.Keys)
    {
      foreach (string actionKey in result[characterKey].actionSpriteDict.Keys)
      {
        Debug.Log($">>> --- 已擷取Sprite： {characterKey} - {actionKey}");
      }
    }
    return result;
  }
}
