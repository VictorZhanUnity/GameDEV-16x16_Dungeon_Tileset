using UnityEngine;
 
public class Chest : MonoBehaviour, IInteractable
{
  public void Interact()
  {
    Debug.Log($"Chest Interact: {gameObject.name}");
  }
}
