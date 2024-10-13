using UnityEngine;
using SPWN;

public class MouseInteract : MonoBehaviour
{
    void OnMouseEnter()
    {
        Dbug.Log($"Mouse entered{gameObject.name}");
    }
}