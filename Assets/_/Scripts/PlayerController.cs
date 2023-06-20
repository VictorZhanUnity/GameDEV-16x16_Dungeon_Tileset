using System;
using UnityEditor;
using UnityEngine;
using static PlayerController;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
  public CharacterStatus status;

  [Serializable]
  public class CharacterStatus
  {
    public CharacterSO characterSO;

    public string characterName => characterSO.characterName;
    public float maxHealthPoint => characterSO.healthPoint;
    public float baseAttack => characterSO.baseAttack;
    public float baseAttackInterval => characterSO.baseAttackInterval;
    public float baseMoveSpeed => characterSO.baseMoveSpeed;

    private float _currentHealthPoint = 0;
    public float currentHealthPoint => _currentHealthPoint;
  }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerController))]
public class Editor_PlayerController : Editor
{
  private bool _switchStatus = true;

  public override void OnInspectorGUI()
  {
    DrawDefaultInspector();
    PlayerController playerController = target as PlayerController;

    CharacterStatus status = playerController.status;
    if (status.characterSO == null) return;
    _switchStatus = EditorGUILayout.BeginFoldoutHeaderGroup(_switchStatus, ">>> Status");
    if (_switchStatus)
    {
      EditorGUILayout.LabelField($"CharacterName: {status.characterName}");
      EditorGUILayout.BeginHorizontal();
      GUILayoutOption[] options = { GUILayout.Width(200f) };

      EditorGUILayout.LabelField($"HP: {status.currentHealthPoint} / {status.maxHealthPoint}", options);
      EditorGUILayout.LabelField($"BaseAttack: {status.baseAttack}", options);
      EditorGUILayout.EndHorizontal();
    }
    EditorGUILayout.EndFoldoutHeaderGroup();
  }
}
#endif
