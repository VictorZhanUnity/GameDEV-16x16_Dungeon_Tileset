using UnityEngine;

public class Player : MonoBehaviour
{
  private IInteractable _interactable;

  private void Update()
  {
    InteractCheck();
    Debug.Log($"deltaTime: {Time.deltaTime}");
    Debug.Log($"time: {Time.time}");
  }

  private void InteractCheck()
  {
    if (Input.GetKeyDown(KeyCode.E) && _interactable != null)
    {
      _interactable.Interact();
    }
  }

  private void OnTriggerEnter2D(Collider2D other) => other.TryGetComponent(out _interactable);
  private void OnTriggerExit2D(Collider2D other) => _interactable = null;
}
