using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using static SpriteSheet_Adapter;

[Serializable]
public class AnimationCreator
{
  [SerializeField] private string _folderUrl = "SpriteSheetAnimation";
  [SerializeField] private List<ActionFramerateItem> _actionFrameratesList;

  /// <summary>
  /// 建立各項動作的AnimationClip檔案
  /// </summary>
  public void CreateAnimationFiles(SpriteSheetItem item)
  {
    string folderUrl = CheckFolderExist($"_/{_folderUrl}/{item.characterName}");

    // 创建AnimatorController
    item.animatorController = AnimatorController.CreateAnimatorControllerAtPath($"{folderUrl}/{item.characterName}.controller");

    string filePath;
    int frameRate;
    foreach (string action in item.actionSpriteDict.Keys)
    {
      filePath = $"{folderUrl}/{action}";
      frameRate = GetFrameRate(action);
      item.actionAnimationDict[action] = CreateAnimationClip(filePath, item.actionSpriteDict[action], frameRate);
      Debug.Log($">>> --- 建立AnimationClip: {item.characterName} - {action}");
    }
  }
  private AnimationClip CreateAnimationClip(string filePath, List<Sprite> sprites, int frameRate = 10)
  {
    // 創建AnimationClip
    AnimationClip animationClip = new AnimationClip()
    {
      frameRate = frameRate,
    };
    SetLoopTime(animationClip, true);

    // 設定關鍵幀
    ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[sprites.Count];

    for (int i = 0; i < sprites.Count; i++)
    {
      keyframes[i] = new ObjectReferenceKeyframe()
      {
        time = i / (float)animationClip.frameRate,
        value = sprites[i],
      };
    }

    AnimationUtility.SetObjectReferenceCurve(animationClip, new EditorCurveBinding()
    {
      type = typeof(SpriteRenderer),
      path = "",
      propertyName = "m_Sprite"
    }, keyframes);

    // 儲存AnimationClip
    AssetDatabase.CreateAsset(animationClip, filePath + ".anim");
    AssetDatabase.SaveAssets();
    return animationClip;
  }

  /// <summary>
  /// 建立資料夾
  /// </summary>
  private string CheckFolderExist(string url)
  {
    string folderPath = $"Assets/{url}/"; // 資料夾的路徑

    //刪除舊檔案，在UnityEditor裡需要按Ctrl+R重新整理
    if (Directory.Exists(folderPath))
    {
      Directory.Delete(folderPath, true);
      Debug.LogWarning($"XXX 刪除舊資料: {folderPath}");
    }

    Directory.CreateDirectory(folderPath);
    Debug.Log($"+++ 創建資料夾: {folderPath}");

    return folderPath;
  }

  private int GetFrameRate(string action)
  {
    foreach (ActionFramerateItem item in _actionFrameratesList)
    {
      if (item.actionName == action) return item.frameRate;
    }
    return 0;
  }

  /// <summary>
  /// AnimationClip設定檔，設定為Loop循環
  /// </summary>
  private void SetLoopTime(AnimationClip clip, bool loopTime)
  {
    AnimationClipSettings clipSettings = AnimationUtility.GetAnimationClipSettings(clip);
    clipSettings.loopTime = loopTime;
    AnimationUtility.SetAnimationClipSettings(clip, clipSettings);
  }

  public void AutoSetActionFrameRate()
  {
    _actionFrameratesList = new List<ActionFramerateItem>()
    {
      new ActionFramerateItem(ActionType.Idle.ToString(), 5),
      new ActionFramerateItem(ActionType.Walk.ToString(), 10),
    };
  }

  [Serializable]
  public class ActionFramerateItem
  {
    public string actionName;
    public int frameRate;

    public ActionFramerateItem(string actionName, int frameRate)
    {
      this.actionName = actionName;
      this.frameRate = frameRate;
    }
  }

}
