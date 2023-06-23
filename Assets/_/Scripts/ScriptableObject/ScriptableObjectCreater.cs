using System;
using UnityEditor;
using UnityEngine;
using VictorUtilties;
using VictorUtilties.Base;
using static CharacterSO;

[Serializable]
public class ScriptableObjectCreater : SingletonBase<ScriptableObjectCreater>
{
  [SerializeField] private FolderName _exportFolderName;

  /// <summary>
  /// 建立CharacterSO檔案，並設定值
  /// </summary>
  public static void Create_CharacterSO(CharacterSO_Data characterData)
  {
    string assetPath = $"_/SpriteSheet/SO/{Instance._exportFolderName.characterSO}";
    CharacterSO characterSO = Utility.CreateScriptableObject<CharacterSO>(assetPath, $"SO_{characterData.characterName}");
    characterSO.CopyData(characterData);
  }

  #region [>>> Tool工具列]
  [MenuItem("ScriptableObject/Create/CharactorSO")]
  public static CharacterSO Create_CharacterSO()
  {
    string assetPath = $"_/SpriteSheet/SO/{Instance._exportFolderName.characterSO}";
    return Utility.CreateScriptableObject<CharacterSO>(assetPath);
  }
  #endregion

  [Serializable]
  public class FolderName
  {
    public string characterSO = "CharacterSO";
  }
}
