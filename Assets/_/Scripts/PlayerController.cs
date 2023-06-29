using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VictorUtilties;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
  #region [>>> Variables]
  [SerializeField] private CharacterStatus _status;
  [SerializeField] private Comps _comps;
  public CharacterStatus status => _status;
  private Vector2 _movementDirection;
  private float _movementSpeed;
  #endregion


  #region [>>> Unity Functions]
  private void FixedUpdate()
  {
    if (_status.characterSO != null)
    {
      Move(_comps.rb);
      Animate(_comps.spriteRenderer, _comps.animator);
    }
  }
  private void OnValidate()
  {
    if (_status.characterSO != null)
    {
      gameObject.name = _status.characterSO.name;
      Utility.EditorDelayCall(() =>
      {
        _comps.animator.runtimeAnimatorController = _status.characterSO.animatorController;
        _comps.spriteRenderer.sprite = _status.characterSO.sprite;
      });
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.TryGetComponent(out Item item))
    {
      ItemSO itemSO = item.Collect();
      Debug.Log($"itemSO: {itemSO}");

      if(itemSO.itemType == ItemType.Potion)
      {
        Drink(itemSO as PotionSO);
      }
    }
  }
  private void Drink(PotionSO potion)
  {

  }


  [ContextMenu("- GetComps")]
  private void GetComps() => _comps.GetComponents(transform);
  #endregion

  #region [>>> Custom Functions]
  /// <summary>
  /// 角色移動
  /// </summary>
  private void Move(Rigidbody2D rb, bool isSprintable = true)
  {
    _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    _movementSpeed = Mathf.Clamp(_movementDirection.magnitude, 0.0f, 1.0f) * _status.currentMoveSpeed;
    if (isSprintable)
    {
      float multiple = 1;
      if (Input.GetKey(KeyCode.LeftShift)) multiple = 2f;
      _movementSpeed *= multiple;
      _comps.animator.speed = multiple;
    }

    _movementDirection.Normalize();

    rb.velocity = _movementDirection * _movementSpeed;
  }

  /// <summary>
  /// 處理動畫
  /// </summary>
  private void Animate(SpriteRenderer spriteRenderer, Animator animator)
  {
    float directionX = Input.GetAxis("Horizontal");
    if (directionX != 0)
    {
      spriteRenderer.transform.localScale = new Vector2(directionX > 0 ? 1 : -1, 1);
    }
    animator.SetFloat("MovementSpeed", _movementSpeed);
  }
  #endregion
}

[Serializable]
public class Comps
{
  public Rigidbody2D rb;
  public SpriteRenderer spriteRenderer;
  public Animator animator;

  public void GetComponents(Transform main)
  {
    rb = main.GetComponent<Rigidbody2D>();
    spriteRenderer = main.transform.Find("SpriteRenderer").GetComponent<SpriteRenderer>();
    animator = spriteRenderer.GetComponent<Animator>();
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
