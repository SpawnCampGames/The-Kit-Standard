using UnityEngine;
//SpawnCampGames.com

public class GlobalTransform : MonoBehaviour
{
     
    public Vector3 globalPosition;
     
    public Vector3 globalRotation;
     
    public Vector3 globalScale;

    private void Update()
    {
        // Get the root transform of the object
        Transform rootTransform = transform.root;

        // Start with the local scale of this transform
        Vector3 currentScale = transform.localScale;

        if(!rootTransform)
        {
            // Iterate through the parent transforms, multiplying their scales
            for(Transform parentTransform = transform.parent; parentTransform != rootTransform; parentTransform = parentTransform.parent)
            {
                parentTransform.localScale.Scale(currentScale);
                currentScale = parentTransform.localScale;
            }
        }
        else
        {
            currentScale = transform.localScale;
        }

        // Multiply the final scale by the root transform's scale
        globalScale = rootTransform.localScale;
        globalScale.Scale(currentScale);

        // Set the global position and rotation
        globalPosition = transform.position;
        globalRotation = transform.rotation.eulerAngles;
    }
}
