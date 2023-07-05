using System;
using System.Collections.Generic;
using UnityEngine;

namespace VictorUtilties.UI
{
  public class TabGroup : MonoBehaviour
  {
    //用SerializeField方式，較方便安排TabButton的排序
    [SerializeField] private List<TabButton> _tabButtonList;
    [SerializeField] private Comps _comps;

    private TabButton _currentTabButton = null;

    private void Start()
    {
      foreach (TabButton tabButton in _tabButtonList)
      {
        tabButton.tabContent.SetActive(false);
      }
      OnTabSelected(_tabButtonList[0]);
    }
    public void OnTabEnter(TabButton tabButton)
    {
      if (IsCurrentTabButton(tabButton)) return;
      tabButton.SwitchSprite(_comps.tabSprite_Enter);
    }

    public void OnTabSelected(TabButton tabButton)
    {
      ResetLastSelectedTabButton();
      _currentTabButton = tabButton;
      _currentTabButton.tabContent.SetActive(true);
      _currentTabButton.SwitchSprite(_comps.tabSprite_Selected);
    }

    public void OnTabExit(TabButton tabButton)
    {
      if (IsCurrentTabButton(tabButton)) return;
      tabButton.SwitchSprite(_comps.tabSprite_Exit);
    }

    private void ResetLastSelectedTabButton()
    {
      if (_currentTabButton == null) return;
      _currentTabButton.tabContent.SetActive(false);
      TabButton lastTabButton = _currentTabButton;
      _currentTabButton = null;
      OnTabExit(lastTabButton);
    }

    private bool IsCurrentTabButton(TabButton tabButton)
    {
      if (_currentTabButton != null)
      {
        if (_currentTabButton == tabButton) return true;
      }
      return false;
    }

    [Serializable]
    public class Comps
    {
      public Sprite tabSprite_Enter;
      public Sprite tabSprite_Selected;
      public Sprite tabSprite_Exit;
    }
  }
}
