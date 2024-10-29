using UnityEngine;
using UnityEngine.UI;

namespace SPWN
{
    /// <summary>
    /// Initializes the Button component and adds a listener for the click event.
    /// </summary>
    public class ButtonExtended : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Debug.Log($"Button clicked: {gameObject.name}");
            // Add additional actions to perform on button click
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
