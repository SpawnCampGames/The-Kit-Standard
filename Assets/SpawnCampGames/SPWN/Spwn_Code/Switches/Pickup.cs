using UnityEngine;
using UnityEngine.Events;
using SPWN;

public class Pickup : MonoBehaviour, IInteractable
{
    public UnityEvent onPickup = new UnityEvent();

    bool hasPickedUp;

    public void Interact()
    {
        if(!hasPickedUp)
        {
            // functionallity of Pickup
            onPickup?.Invoke();
        }
    }
}
