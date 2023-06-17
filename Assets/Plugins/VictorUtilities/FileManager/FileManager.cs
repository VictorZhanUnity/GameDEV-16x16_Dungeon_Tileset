using System.IO;
using UnityEngine;

namespace VictorUtilties.Managers
{
  public class FileManager
  {
    /// <summary>
    /// 建立資料夾
    /// </summary>
    public static string CheckFolderExist(string folderPath, bool isReplace = true)
    {
      //刪除舊檔案，在UnityEditor裡需要按Ctrl+R重新整理
      if (Directory.Exists(folderPath) && isReplace)
      {
        Directory.Delete(folderPath, true);
        Debug.LogWarning($"XXX 刪除舊資料: {folderPath}");
      }

      Directory.CreateDirectory(folderPath);
      Debug.Log($"+++ 創建資料夾: {folderPath}");
      return folderPath;
    }
  }
}
