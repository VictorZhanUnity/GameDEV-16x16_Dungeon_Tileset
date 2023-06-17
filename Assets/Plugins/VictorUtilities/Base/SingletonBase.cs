namespace VictorUtilties.Base
{
  /// <summary>
  /// 單例模式
  /// </summary>
  public abstract class SingletonBase<T> where T : class, new()
  {
    protected static T instance;
    public static T Instance
    {
      set => instance = value;
      get
      {
        instance ??= new T();
        return instance;
      }
    }

    /// <summary>
    /// 運用建構子，將Inspector裡設定的參數引導進來，而不會新建Instance
    /// </summary>
    protected SingletonBase() => instance ??= this as T;
  }
}
