using System;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using static SpriteSheet_Adapter;

[Serializable]
public class BlendTreeCreator
{
  [SerializeField] private Parameter _parameter;

  /// <summary>
  /// 建立BlendTree 移動
  /// </summary>
  public void CreateBlendTree_Move(SpriteSheetItem item, float threshold = 0.1f)
  {
    string parameterName = _parameter.name_Movement;
    string idle = ActionType.Idle.ToString();
    string walk = ActionType.Walk.ToString();
    BlendTree blendTree = new BlendTree()
    {
      name = $"BlendTree_{parameterName}",
      blendType = BlendTreeType.Simple1D,
      useAutomaticThresholds = false,
      blendParameter = parameterName,
    };
    blendTree.AddChild(item.actionAnimationDict[idle], 0);
    blendTree.AddChild(item.actionAnimationDict[walk], threshold);
    item.animatorController.AddMotion(blendTree);
    item.animatorController.AddParameter(parameterName, AnimatorControllerParameterType.Float);

    Debug.Log($">>> --- 建立 {blendTree.name} [{item.characterName}]: {idle}, {walk}");
    AssetDatabase.SaveAssets();
    EditorUtility.SetDirty(blendTree);
    EditorUtility.SetDirty(item.animatorController);
  }

  [Serializable]
  public class Parameter
  {
    public string name_Movement = "MovementSpeed";
  }
}

