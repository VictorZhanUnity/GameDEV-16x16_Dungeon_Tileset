using System;
using System.Collections.Generic;
using VictorUtilties.Base;

namespace VictorUtilties.Managers
{
  public class ExecManager : SingletonMonoBehaviour<ExecManager>
  {
    #region [>>> Variables]
    private List<Action> _actionList = new List<Action>();
    #endregion

    /// <summary>
    /// 新增欲執行的函式
    /// </summary>
    public static void Exec(Action exec) => instance._actionList.Add(exec);

    #region [>>> Unity Functions]
    public void Update()
    {
      for (int i = 0; i < _actionList.Count; i++)
      {
        if (_actionList[i] == null) continue;
        _actionList[i].Invoke();
        _actionList.Remove(_actionList[i]);
      }
    }
    #endregion
  }
}
