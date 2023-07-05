using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VictorUtilties.UI
{
  [RequireComponent(typeof(Image))]
  public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
  {
    [SerializeField] private TabGroup _tabGroup;
    [SerializeField] private GameObject _tabContent;
    [SerializeField] private Image _image;

    public GameObject tabContent => _tabContent;

    public void SwitchSprite(Sprite sprite) => _image.sprite = sprite;
    public void OnPointerEnter(PointerEventData eventData) => _tabGroup.OnTabEnter(this);
    public void OnPointerClick(PointerEventData eventData) => _tabGroup.OnTabSelected(this);
    public void OnPointerExit(PointerEventData eventData) => _tabGroup.OnTabExit(this);

    private void Reset() => _image = GetComponent<Image>();
  }
}

