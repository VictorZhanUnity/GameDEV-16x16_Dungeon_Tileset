using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] private CharacterStatus _status;
  public CharacterStatus status => _status;
  [SerializeField] private Rigidbody2D _rb;
  [SerializeField] private Animator _animator;
  [SerializeField] private Transform _spriteTrans;


  private Vector2 _movementDirection;
  private float _movementSpeed;

  private void FixedUpdate()
  {
    Move();
    Animate();
  }

  private void Move(bool isSprintable = true)
  {
    _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    _movementSpeed = Mathf.Clamp(_movementDirection.magnitude, 0.0f, 1.0f) * _status.currentMoveSpeed;
    if (isSprintable)
    {
      float multiple = 1;
      if (Input.GetKey(KeyCode.LeftShift)) multiple = 1.5f;
      _movementSpeed = _movementSpeed * multiple;
    }
    _movementDirection.Normalize();
    _rb.velocity = _movementDirection * _movementSpeed;
  }

  private void Animate()
  {
    _animator.SetFloat("MovementSpeed", _movementSpeed);
    float directionX = Input.GetAxis("Horizontal");
    if (directionX != 0)
    {
      _spriteTrans.localScale = new Vector2(directionX > 0 ? 1 : -1, 1);
    }
  }


  [ContextMenu("- GetParameters")]
  private void Reset()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

}

[Serializable]
public class CharacterStatus
{
  public CharacterSO characterSO;

  public string characterName => characterSO.characterName;
  public float maxHealthPoint => characterSO.healthPoint;
  public float currentHealthPoint { get; private set; } = 0;

  public float baseAttackDamage => characterSO.baseAttackDamage;
  public float currentAttackDamage { get; private set; } = 0;

  public float baseAttackInterval => characterSO.baseAttackInterval;
  public float currenAttackInterval { get; private set; } = 0;

  public float baseMoveSpeed => characterSO.baseMoveSpeed;
  public float currentMoveSpeed => baseMoveSpeed;
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
    _switchStatus = EditorGUILayout.BeginFoldoutHeaderGroup(_switchStatus, "========== From CharacterSO ==========");
    if (_switchStatus)
    {
      List<string> col1 = new List<string>()
      {
        $"CharacterName: {status.characterName}"
        , $"BaseAttackDamage: {status.baseAttackDamage}"
        , $"BaseAttackInterval: {status.baseAttackInterval}"
        , $"BaseMoveSpeed: {status.baseMoveSpeed}"
      };

      List<string> col2 = new List<string>()
      {
        $"HP: {status.currentHealthPoint} / {status.maxHealthPoint}"
        , $"CurrentAttackDamage: {status.currentAttackDamage}"
        , $"CurrentAttackInterval: {status.currenAttackInterval}"
        , $"CurrentMoveSpeed: {status.currentMoveSpeed}"
      };

      GUILayoutOption[] option = { GUILayout.Width(200f) };
      for (int i = 0; i < col1.Count; i++)
      {
        CreateRow(option, col1[i], col2[i]);
      }
    }
    EditorGUILayout.EndFoldoutHeaderGroup();
  }

  private void CreateRow(GUILayoutOption[] option, params string[] msg)
  {
    EditorGUILayout.BeginHorizontal();
    foreach (string s in msg)
    {
      EditorGUILayout.LabelField(s, option);
    }
    EditorGUILayout.EndHorizontal();
  }
}
#endif
