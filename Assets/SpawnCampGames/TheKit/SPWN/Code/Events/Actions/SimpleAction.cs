using UnityEngine;
using UnityEngine.Events;

namespace SPWN.Extras
{
    public class SimpleAction : MonoBehaviour
    {
        [Tooltip("Can Only be Fired from Public Function")]
        public bool TriggeredElsewhere = true;

        [Tooltip("Fired By Start if False")]
        public bool Repeatable;

        public UnityEvent ActionEvent;

        private void Start()
        {
            if (!TriggeredElsewhere && !Repeatable)
            {
                ActionEvent.Invoke(); // Invoke on Start if not triggered elsewhere and not repeatable
            }
        }

        private void OnEnable()
        {
            if (!TriggeredElsewhere && Repeatable)
            {
                ActionEvent.Invoke(); // Invoke on enable if not triggered elsewhere and repeatable
            }
        }

        public void Go()
        {
            ActionEvent.Invoke(); // Public method to invoke the action
            Dbug.Green("Action Event Fired"); // Assuming Dbug is defined in your project
        }
    }
}
