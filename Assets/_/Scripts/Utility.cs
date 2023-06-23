using System;
using UnityEditor;
using UnityEngine;
using VictorUtilties.Managers;

namespace VictorUtilties
{
  public class Utility
  {
    private static Action _delayCallAction;
    public static void EditorDelayCall(Action action)
    {
      _delayCallAction = action;
      EditorApplication.delayCall += OnDelayCall;
    }

    private static void OnDelayCall()
    {
      _delayCallAction.Invoke();
      EditorApplication.delayCall -= OnDelayCall;
    }

    public static T CreateScriptableObject<T>(string assetPath, string fileName = "NewSO") where T : ScriptableObject
    {
      // 創建一個新的 CharactorSO 的實例
      T so = ScriptableObject.CreateInstance<T>();

      // 儲存 ScriptableObject 為檔案
      string path = $"Assets/{assetPath}";
      FileManager.CheckFolderExist(path, false);
      AssetDatabase.CreateAsset(so, $"{path}/{fileName}.asset");
      AssetDatabase.SaveAssets();
      EditorUtility.SetDirty(so);

      // 在 Unity 編輯器中高亮顯示新創建的檔案
      EditorUtility.FocusProjectWindow();
      Selection.activeObject = so;

      Debug.Log($">>> 建立ScriptableObject檔案: {fileName}");
      return so;
    }
  }
}
