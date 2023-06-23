using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class TriggerHandler : MonoBehaviour
{
  #region [>>> Variables]
  [SerializeField] private TriggerSetting _triggerSetting;
  #endregion

  public float triggleRange { set => _triggerSetting.triggleRange = value; }
  public float offsetY { set => _triggerSetting.offsetY = value; }
  protected Sprite iconSprite { get => _triggerSetting.spriteRenderer.sprite; set => _triggerSetting.spriteRenderer.sprite = value; }

  protected abstract void OnTriggerBegin(Collider2D target);
  protected virtual void OnTrigger(Collider2D target) { }
  protected virtual void OnTriggerEnd(Collider2D target) { }

  #region [>>> Unity Functions]
  protected void OnValidate() => _triggerSetting.OnValidate();
  private void OnTriggerEnter2D(Collider2D target) => OnTriggerBegin(target);
  private void OnTriggerStay2D(Collider2D target) => OnTrigger(target);
  private void OnTriggerExit2D(Collider2D target) => OnTriggerEnd(target);
  #endregion

  #region [>>> ContextMenu]
  [ContextMenu("- GetComponent")]
  private void GetComps() => _triggerSetting.GetComps(transform);
  #endregion

  [Serializable]
  public class TriggerSetting
  {
    [Range(0.1f, 10f)] public float triggleRange = 1.0f;
    [Range(-5f, 5f)] public float offsetY = 0f;
    public CircleCollider2D circleCollider2D;
    public SpriteRenderer spriteRenderer;

    public void OnValidate()
    {
      if (circleCollider2D == null) return;
      circleCollider2D.radius = triggleRange;
      Vector2 offset = circleCollider2D.offset;
      offset.y = offsetY;
      circleCollider2D.offset = offset;
    }


    public void GetComps(Transform parent)
    {
      circleCollider2D = parent.GetComponent<CircleCollider2D>();
      circleCollider2D.isTrigger = true;
      spriteRenderer = parent.Find("SpriteRenderer").GetComponent<SpriteRenderer>();
    }
  }
}
