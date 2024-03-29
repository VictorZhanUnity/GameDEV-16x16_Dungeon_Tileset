using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Scripting;

[CreateAssetMenu(fileName = "New CharacterSO", menuName = "+ New/ScripteableObject/Character"), Serializable, Preserve]
public class CharacterSO : ScriptableObject
{
  public string characterName;
  [Range(1, 9999)] public float healthPoint;
  [Range(1, 999)] public float baseAttackDamage;
  [Range(1, 9)] public float baseAttackInterval;
  [Range(1, 99)] public float baseMoveSpeed;

  public Sprite sprite;
  public AnimatorController animatorController;

  public void CopyData(CharacterSO_Data data)
  {
    characterName = data.characterName;
    healthPoint = data.healthPoint;
    baseAttackDamage = data.baseAttack;
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

    public CharacterSO_Data() { }
    public CharacterSO_Data(string characterName, float healthPoint, float baseAttack, float baseAttackInterval, float baseMoveSpeed)
    {
      this.characterName = characterName;
      this.healthPoint = healthPoint;
      this.baseAttack = baseAttack;
      this.baseAttackInterval = baseAttackInterval;
      this.baseMoveSpeed = baseMoveSpeed;
    }
  }
}

