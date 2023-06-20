using System;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterSO", menuName = "+ New/ScripteableObject/Character")]
public class CharacterSO : ScriptableObject
{
  public string characterName;
  [SerializeField]
  [Range(1, 9999)] public float healthPoint;
  [Range(1, 999)] public float baseAttack;
  [Range(1, 9)] public float baseAttackInterval;
  [Range(1, 99)] public float baseMoveSpeed;

  public Sprite sprite;
  public AnimatorController animatorController;

  public void CopyData(CharacterSO_Data data)
  {
    characterName = data.characterName;
    healthPoint = data.healthPoint;
    baseAttack = data.baseAttack;
    baseAttackInterval = data.baseAttackInterval;
    baseMoveSpeed = data.baseMoveSpeed;
    sprite = data.sprite;
    animatorController = data.animatorController;
  }

  [Serializable]
  public class CharacterSO_Data
  {
    public string characterName;
    [Range(1, 9999)] public float healthPoint;
    [Range(1, 999)] public float baseAttack;
    [Range(1, 9)] public float baseAttackInterval;
    [Range(1, 99)] public float baseMoveSpeed;

    [HideInInspector] public Sprite sprite;
    [HideInInspector] public AnimatorController animatorController;
  }
}

