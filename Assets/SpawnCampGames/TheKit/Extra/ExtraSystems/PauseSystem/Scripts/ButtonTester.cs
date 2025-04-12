using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTester : MonoBehaviour
{
    public EventSystem currentEventSystem;

    public void ButtonPress()
    {
        Debug.Log("You Successfully Pressed the Button.");
        StartCoroutine(ClearSelection());
    }

    private System.Collections.IEnumerator ClearSelection()
    {
        yield return null;
        currentEventSystem.SetSelectedGameObject(null);
    }
}
