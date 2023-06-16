using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;


/********************************************************************
 * 
 * sp_Boss_Idle_1
 * 
 * ******************************************************************/
public class SpriteSheetManager : MonoBehaviour
{
  public string resourceFoldeName = "SpriteSheet";
  public Sprite spriteSheet; // 在Inspector窗口中将spritesheet拖拽到该变量上

  public string spriteHeader = "sp_";
  public string keyWord;

  private List<Sprite> _headerSpritesList;
  private Dictionary<string, SpriteSheetItem> _characterDict;

  [ContextMenu("- GenerateAnimationClip")]
  public void GenerateAnimationClip()
  {
    _headerSpritesList = GetResourceSprites();
    _characterDict = ClassficationSprites();
    foreach (string characterName in _characterDict.Keys)
    {
      CreateCharacterAnimation(_characterDict[characterName]);
      CreateBlendTree_Move(_characterDict[characterName]);
    }
  }

  /// <summary>
  /// 擷取SpriteSheet上的所有包含Header的Sprite
  /// </summary>
  private List<Sprite> GetResourceSprites()
  {
    List<Sprite> result = new List<Sprite>();
    Sprite[] sprites = Resources.LoadAll<Sprite>($"{resourceFoldeName}/{spriteSheet.texture.name}"); // 注意，这里假设spritesheet已经作为资源导入到了Unity的资源文件夹中
    if (sprites.Length == 0)
    {
      Debug.LogError($">>> 无法加载{spriteSheet.texture.name}的sprite！");
      return null;
    }
    foreach (Sprite sprite in sprites)
    {
      if (sprite.name.Contains(spriteHeader))
      {
        result.Add(sprite);
      }
    }
    if (result.Count == 0)
    {
      Debug.LogError($">>> 並無包含{spriteHeader}的sprite！");
      return null;
    }
    return result;
  }

  /// <summary>
  /// 建立BlendTree 移動
  /// </summary>
  private void CreateBlendTree_Move(SpriteSheetItem item, float threshold = 0.1f)
  {
    string parameterName = "MovementSpeed";
    BlendTree blendTree = new BlendTree()
    {
      name = $"BlendTree_{parameterName}",
      blendType = BlendTreeType.Simple1D,
      useAutomaticThresholds = false,
      blendParameter = parameterName,
    };
    blendTree.AddChild(item.actionAnimationDict["Idle"], 0);
    blendTree.AddChild(item.actionAnimationDict["Walk"], threshold);
    item.animatorController.AddMotion(blendTree);
    item.animatorController.AddParameter(parameterName, AnimatorControllerParameterType.Float);
  }



  /// <summary>
  /// 依照{角色名}_{動作}_{序號}進行歸類成Dictionary
  /// </summary>
  private Dictionary<string, SpriteSheetItem> ClassficationSprites()
  {
    Dictionary<string, SpriteSheetItem> result = new Dictionary<string, SpriteSheetItem>();
    string character, action;
    foreach (Sprite sprite in _headerSpritesList)
    {
      character = sprite.name.Split('_')[1];
      action = sprite.name.Split('_')[2];

      // 依照character建立SpriteSheetItem
      if (result.ContainsKey(character) == false)
      {
        result[character] = new SpriteSheetItem()
        {
          characterName = character
          , iconSprite = sprite
        };
      }

      // 依照action建立SpriteSheetItem裡的各個spriteDict，並儲存sprite
      if (result[character].actionSpriteDict.ContainsKey(action) == false)
      {
        result[character].actionSpriteDict[action] = new List<Sprite>();
      }
      result[character].actionSpriteDict[action].Add(sprite);
    }
    return result;
  }

  /// <summary>
  /// 建立各項動作的AnimationClip
  /// </summary>
  /// <param name="item"></param>
  private void CreateCharacterAnimation(SpriteSheetItem item)
  {
    string folderUrl = CreateFolder($"_/Animations/{item.characterName}");

    // 创建AnimatorController
    item.animatorController = AnimatorController.CreateAnimatorControllerAtPath($"{folderUrl}/{item.characterName}.controller");

    string filePath;
    int frameRate;
    foreach (string key in item.actionSpriteDict.Keys)
    {
      filePath = $"{folderUrl}/{key}";
      frameRate = (int)Enum.Parse(typeof(Enum_ActionType), key);
      item.actionAnimationDict[key] = CreateAnimationClip(filePath, item.actionSpriteDict[key], frameRate);
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
  /// 設定AnimationClip為Loop循環
  /// </summary>
  private void SetLoopTime(AnimationClip clip, bool loopTime)
  {
    AnimationClipSettings clipSettings = AnimationUtility.GetAnimationClipSettings(clip);
    clipSettings.loopTime = loopTime;
    AnimationUtility.SetAnimationClipSettings(clip, clipSettings);
  }

  /// <summary>
  /// 建立資料夾
  /// </summary>
  private string CreateFolder(string url)
  {
    string folderPath = $"Assets/{url}/"; // 資料夾的路徑
    if (!Directory.Exists(folderPath))
    {
      Directory.CreateDirectory(folderPath);
      Debug.Log($">>> 創建 '{folderPath}' 資料夾！");
    }
    return folderPath;
  }

  public class SpriteSheetItem
  {
    /// <summary>
    /// 角色名
    /// </summary>
    public string characterName;

    public Sprite iconSprite;

    /// <summary>
    /// 各項動作的Sprites
    /// </summary>
    public Dictionary<string, List<Sprite>> actionSpriteDict = new Dictionary<string, List<Sprite>>();

    public AnimatorController animatorController;

    /// <summary>
    /// 各項動作的AnimationClip
    /// </summary>
    public Dictionary<string, AnimationClip> actionAnimationDict = new Dictionary<string, AnimationClip>();
  }

  public enum Enum_ActionType
  {
    Idle = 5,
    Walk = 10,
  }
}
