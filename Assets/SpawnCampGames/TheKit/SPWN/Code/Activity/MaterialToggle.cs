using UnityEngine;

namespace SPWN
{
    public class MaterialToggle : MonoBehaviour
    {
        public Renderer targetRenderer;
        public Material onMaterial;
        public Material offMaterial;

        private bool isOn;

        public bool IsOn
        {
            get => isOn;
            set
            {
                if(isOn == value || targetRenderer == null) return;
                isOn = value;
                UpdateMaterial();
            }
        }

        private void Start()
        {
            if(targetRenderer == null) targetRenderer = GetComponent<Renderer>();
            UpdateMaterial();
        }

        private void UpdateMaterial()
        {
            if(targetRenderer != null && onMaterial != null && offMaterial != null)
                targetRenderer.sharedMaterial = isOn ? onMaterial : offMaterial;
        }
    }
}
