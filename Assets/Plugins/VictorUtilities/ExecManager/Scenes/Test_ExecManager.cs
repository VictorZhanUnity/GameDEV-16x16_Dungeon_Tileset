using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using VictorUtilties.Managers;

namespace ThreadToUnity
{
  public class Test_ExecManager : MonoBehaviour
  {
    [SerializeField] private Image img;
    [SerializeField] private Button btn;

    void Start()
    {
      btn.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
      Thread thread = new Thread(DoSomething);
      thread.Start();
    }

    private void DoSomething()
    {
      //ShowImage();
      ExecManager.Exec(ToStartCoroutine);

    }

    private void ToStartCoroutine()
    {
      StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
      int counter = 0;
      while (++counter <= 3)
      {
        Debug.Log(counter);
        yield return new WaitForSeconds(1);
      }
      ExecManager.Exec(ShowImage);
    }

    public void ShowImage()
    {
      img.gameObject.SetActive(true);
    }
  }
}
