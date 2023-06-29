using System;
using UnityEditor;
using UnityEngine;
using VictorUtilties;
using VictorUtilties.Base;
using static CharacterSO;

[Serializable]
public class CharacterSOCreater : SingletonBase<CharacterSOCreater>
{
  [SerializeField] private string _urlPath = "SpriteSheet/SO";
  [SerializeField] private string _exportFolderName = "CharacterSO";

  /// <summary>
  /// 建立CharacterSO檔案，並設定值
  /// </summary>
  public static void Create_CharacterSO(CharacterSO_Data characterData)
  {
    CharacterSO characterSO = Utility.CreateScriptableObject<CharacterSO>(Instance.assetPath, $"SO_{characterData.characterName}");
    characterSO.CopyData(characterData);
  }

  #region [>>> Tool工具列]
  [MenuItem("ScriptableObject/Create/CharactorSO")]
  public static CharacterSO Create_CharacterSO()
  {
    return Utility.CreateScriptableObject<CharacterSO>(Instance.assetPath);
  }

  private string assetPath => $"_/{_urlPath}/{_exportFolderName}";
  #endregion
}
