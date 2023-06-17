using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public class SpriteSheet_Adapter : MonoBehaviour
{
  [SerializeField, Header(">>> 擷取SpriteSheet各圖案")] private SpriteAutoGetter _spriteAutoGetter;
  [SerializeField, Header(">>> 生產各個動畫Clip")] private AnimationCreator _animationCreator;
  [SerializeField, Header(">>> 生產各個BlendTree Action")] private BlendTreeCreator _blendTreeCreator;

  private List<Sprite> _headerSpritesList;
  private Dictionary<string, SpriteSheetItem> _characterDict;

  [ContextMenu("- 自動擷取SpriteSheet生成動畫Clip")]
  public void AutoGenerateAnimationSet()
  {
    Debug.Log($">>> 開始擷取SpriteSheet...");

    _headerSpritesList = _spriteAutoGetter.GetResourceSprites();
    _characterDict = _spriteAutoGetter.ClassficationSprites(_headerSpritesList);
    foreach (string characterName in _characterDict.Keys)
    {
      _animationCreator.CreateAnimationFiles(_characterDict[characterName]);
      _blendTreeCreator.CreateBlendTree_Move(_characterDict[characterName]);
      Debug.Log($">>> {characterName} 已生成完畢!");
    }
  }

  [ContextMenu("--- AutoSetActionFrameRate")]
  public void AutoSetActionFrameRate() => _animationCreator.AutoSetActionFrameRate();

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
  public enum ActionType
  {
    Idle, Walk
  }
}
