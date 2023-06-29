using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static CharacterSO;

public class SpriteSheet_Adapter : MonoBehaviour
{
  [SerializeField] private Comps _comps;

  [SerializeField, Header(">>> 生產各個動畫Clip與Framerate設定")] private AnimationCreator _animationCreator;
  [SerializeField, Header(">>> Character相關數據")] private CharacterDataSetting _characterDataSetting;

  private Dictionary<string, SpriteSheetItem> _characterDict;

  [ContextMenu("★ 自動擷取SpriteSheet生成動畫Clip")]
  public void AutoGenerateAnimationSet()
  {
    Debug.Log($">>> 開始擷取SpriteSheet...");
    _characterDict = _comps.ClassficationSprites();
    
    foreach (string characterName in _characterDict.Keys)
    {
      _animationCreator.CreateAnimationFiles(_characterDict[characterName]);
      _comps.CreateBlendTree_Move(_characterDict[characterName]);

      CreateCharacterSO(_characterDict[characterName]);

      Debug.Log($">>> {characterName} 已生成完畢!");
    }
  }

  #region [>>>> Private Functions]
  /// <summary>
  /// 建立角色的ScriptableObject設定檔
  /// </summary>
  private void CreateCharacterSO(SpriteSheetItem item)
  {
    CharacterSO_Data characterData = CharacterDataSetting.GetCharacterStatus(item);
    CharacterSOCreater.Create_CharacterSO(characterData);
  }
  #endregion

  #region [>>> ContextMenu]
  [ContextMenu("--- AutoSetActionFrameRate")]
  public void AutoSetActionFrameRate() => _animationCreator.AutoSetActionFrameRate();
  #endregion

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
  
  
  [Serializable]
  private class Comps
  {
    [SerializeField, Header(">>> 擷取SpriteSheet各圖案")] private SpriteAutoGetter _spriteAutoGetter;
    [SerializeField, Header(">>> 生產各個BlendTree Action")] private BlendTreeCreator _blendTreeCreator;
    [SerializeField, Header(">>> 生產CharacterSO")] private CharacterSOCreater _soCreater;

    /// <summary>
    /// 擷取SpriteSheet，並依角色名分類為SpriteSheetItem
    /// </summary>
    public Dictionary<string, SpriteSheetItem> ClassficationSprites()
    {
      List<Sprite> spriteList = _spriteAutoGetter.GetResourceSprites();
      Dictionary<string, SpriteSheetItem> characterDict = _spriteAutoGetter.ClassficationSprites(spriteList);
      return characterDict;
    }

    /// <summary>
    /// 創建BlendTree
    /// </summary>
    /// <param name="item"></param>
    public void CreateBlendTree_Move(SpriteSheetItem item) => _blendTreeCreator.CreateBlendTree_Move(item);
  }
}
