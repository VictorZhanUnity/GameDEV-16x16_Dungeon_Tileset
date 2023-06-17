using UnityEngine;

namespace VictorUtilties.Base
{
  /// <summary>
  /// 單例模式
  /// </summary>
  public class SingletonMonoBehaviour<T> : MonoBehaviour where T: class, new()
  {
    protected static T instance;

    protected void Awake()
    {
      if (instance == null)
      {
        instance = this as T;
        DontDestroyOnLoad(gameObject);
      }
      else Destroy(gameObject);
    }
  }
}
