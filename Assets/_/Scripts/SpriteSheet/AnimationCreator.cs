using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VictorUtilties.Managers;
using static SpriteSheet_Adapter;

[Serializable]
public class AnimationCreator
{
  [SerializeField] private string _exportFolderUrl = "Animation";
  [SerializeField] private List<ActionFramerateItem> _actionTypeFrameratesList;

  /// <summary>
  /// 建立各項動作的AnimationClip檔案
  /// </summary>
  public void CreateAnimationFiles(SpriteSheetItem item)
  {
    string folderUrl = FileManager.CheckFolderExist($"Assets/_/SpriteSheet/{_exportFolderUrl}/{item.characterName}");
    Debug.Log($"folderUrl:{folderUrl}");
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

  #region [>>> Private Functions]
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
    EditorUtility.SetDirty(animationClip);
    return animationClip;
  }

  private int GetFrameRate(string action)
  {
    foreach (ActionFramerateItem item in _actionTypeFrameratesList)
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
  #endregion

  public void AutoSetActionFrameRate()
  {
    _actionTypeFrameratesList = new List<ActionFramerateItem>()
    {
      new ActionFramerateItem(ActionType.Idle.ToString(), 5),
      new ActionFramerateItem(ActionType.Walk.ToString(), 10),
    };
  }

  [Serializable]
  private class ActionFramerateItem
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
