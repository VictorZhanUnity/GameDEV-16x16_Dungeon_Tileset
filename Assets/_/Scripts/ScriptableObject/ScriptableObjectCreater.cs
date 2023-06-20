using System;
using UnityEditor;
using UnityEngine;
using VictorUtilties.Base;
using VictorUtilties.Managers;
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
    CharacterSO characterSO = CreateScriptableObject<CharacterSO>(Instance._exportFolderName.characterSO, $"SO_{characterData.characterName}");
    characterSO.CopyData(characterData);
  }

  #region [>>> Tool工具列]
  [MenuItem("ScriptableObject/Create/CharactorSO")]
  public static CharacterSO Create_CharacterSO() => CreateScriptableObject<CharacterSO>(Instance._exportFolderName.characterSO);
  #endregion

  #region [>>> Private Functions]
  /// <summary>
  /// 建立ScriptableObject實體檔案
  /// </summary>
  /// <typeparam name="T">ScriptableObject類別</typeparam>
  /// <param name="folderName">資料夾名稱</param>
  /// <param name="fileName">檔名</param>
  private static T CreateScriptableObject<T>(string folderName, string fileName = "NewSO") where T : ScriptableObject
  {
    // 創建一個新的 CharactorSO 的實例
    T so = ScriptableObject.CreateInstance<T>();

    // 儲存 ScriptableObject 為檔案
    string path = $"Assets/_/SpriteSheet/SO/{folderName}";
    FileManager.CheckFolderExist(path, false);
    AssetDatabase.CreateAsset(so, $"{path}/{fileName}.asset");
    AssetDatabase.SaveAssets();

    // 在 Unity 編輯器中高亮顯示新創建的檔案
    EditorUtility.FocusProjectWindow();
    Selection.activeObject = so;

    Debug.Log($">>> 建立ScriptableObject檔案: {fileName}");
    return so;
  }
  #endregion

  [Serializable]
  public class FolderName
  {
    public string characterSO = "CharacterSO";
  }
}
