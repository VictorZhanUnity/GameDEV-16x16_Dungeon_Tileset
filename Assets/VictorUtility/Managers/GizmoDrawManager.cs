using System.Collections.Generic;
using UnityEngine;

namespace VictorUtilties.Managers
{
  public class GizmoDrawManager : MonoBehaviour
  {
    [SerializeField] private bool isDrawing = false;
    [SerializeField] private List<GameObject> _drawGizmos = new List<GameObject>();

    private void OnDrawGizmos()
    {
      if (isDrawing == false) return;
      foreach (GameObject gizmos in _drawGizmos)
      {
        if(gizmos == null) continue;
        if (gizmos.TryGetComponent(out IDrawGizmos drawGizmos))
        {
          drawGizmos.DrawGizmos();
        }
      }
    }
  }

  public interface IDrawGizmos
  {
    /// <summary>
    /// 供GizmoDrawManager進行Gizmo繪圖
    /// </summary>
    void DrawGizmos();
  }

  public abstract class GizmoItem
  {
    protected Color color;
    protected Vector3 pos;
    public GizmoItem(Color color, Vector3 pos)
    {
      this.color = color;
      this.pos = pos;
    }
    public abstract void Draw();
  }
  /// <summary>
  /// Draw WireSphere
  /// </summary>
  public class GizmoItem_WireSphere : GizmoItem
  {
    private float _radius;
    public GizmoItem_WireSphere(Color color, Vector3 pos, float radius) : base(color, pos) => _radius = radius;
    public override void Draw()
    {
      Gizmos.color = color;
      Gizmos.DrawWireSphere(pos, _radius);
    }
  }
}
