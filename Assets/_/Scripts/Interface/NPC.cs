using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
  public void Interact()
  {
    Debug.Log($"NPC Interact: {gameObject.name}");
  }
}
