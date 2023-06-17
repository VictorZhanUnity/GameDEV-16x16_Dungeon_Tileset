using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VictorUtilties.Base;

namespace VictorUtilties.Managers
{
  [Serializable]
  public class DebugManager:SingletonBase<DebugManager>
  {
    #region [Variables >>>]
    [SerializeField] private bool _isShowDebug = true;
    public static bool IsShowDesbug
    {
      get => Instance._isShowDebug;
      set
      {
        if (value == false) Log(">>> Debug Mode : OFF");
        Instance._isShowDebug = value;
        Instance.OnValidate();
      }
    }

    [SerializeField, Tooltip("依據Debug是否需要顯示的物件")] private List<GameObject> _visualObjects = new List<GameObject>();
    #endregion

    #region [Custom Functions >>>]
    public static void Log(string message) => LogCheck(() => Debug.Log(message));
    public static void LogError(string message) => LogCheck(() => Debug.LogError(message));
    public static void LogWarning(string message) => LogCheck(() => Debug.LogWarning(message));
    private static void LogCheck(Action action)
    {
      if (Instance._isShowDebug) action?.Invoke();
    }

    /// <summary>
    /// 清空Console Log訊息
    /// </summary>
    public static void ClearConsoleLog()
    {
      bool isRuntime = true;
#if UNITY_EDITOR
      isRuntime = false;
      Type assemblyType;
      if (isRuntime) assemblyType = typeof(UnityEditor.ActiveEditorTracker);
      else assemblyType = typeof(UnityEditor.Editor);

      var assembly = Assembly.GetAssembly(assemblyType);
      var type = assembly.GetType("UnityEditor.LogEntries");
      var method = type.GetMethod("Clear");
      method.Invoke(new object(), null);
#endif

    }


    /// <summary>
    /// OnValidate只對非static值有效，static
    /// </summary>
    public void OnValidate()
    {
      foreach (GameObject obj in _visualObjects)
      {
        obj.SetActive(_isShowDebug);
      }
    }
    /// <summary>
    /// 是否為Debug模式
    /// </summary>
    public bool isDebugging
    {
      set
      {
        _isShowDebug = value;
        OnValidate();
      }
      get => _isShowDebug;
    }
    #endregion
  }
}
