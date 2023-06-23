using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Floating : MonoBehaviour
{
  [SerializeField, Range(0.01f, 5f)] private float _offsetY = 0.1f;
  [SerializeField, Range(0.1f, 5f)] private float _duration = 1f;
  [SerializeField] private Ease _easyType = Ease.OutCirc;

  private Vector3 _originalPos;
  private Vector3 _fromPos;
  private Tween _tween;

  private void Awake()
  {
    _originalPos = _fromPos = transform.position;
    _fromPos.y += _offsetY;
    _tween = transform.DOMoveY(_originalPos.y, _duration).From(_fromPos)
      .SetLoops(-1, LoopType.Yoyo).SetEase(_easyType);
  }

  private void OnDestroy()
  {
    _tween.Kill();
  }
}
