using UnityEngine;
using UnityEditor;
using SPWN;

[ExecuteInEditMode]
public class ExtraArrows : MonoBehaviour
{
    Transform targetTransform;
    bool show = true;
    bool showNegatives = false;

    float baseArrowSize = .5f;
    float sizeMultiplier = 0.1f;
    float labelOffset = 0.2f;

    [SpawnButton("GO GO GIZMOS",true)]
    void GoGoGizmos()
    {
        show = !show;
    }

    [SpawnButton("SHOW NEG",true)]
    void GoGoReverse()
    {
        showNegatives = !showNegatives;
    }

    void OnDrawGizmos()
    {
        if(!Application.isEditor) return;
        targetTransform = gameObject.transform;

        if(show && targetTransform != null)
        {
            Camera sceneCamera = SceneView.lastActiveSceneView?.camera ?? Camera.main;
            if(sceneCamera == null) return;

            float distance = Vector3.Distance(sceneCamera.transform.position,targetTransform.position);

            float arrowSize = baseArrowSize + (distance * sizeMultiplier);

            Vector3 position = targetTransform.position;
            Vector3 forward = targetTransform.forward;
            Vector3 up = targetTransform.up;
            Vector3 right = targetTransform.right;

            DrawArrow(position,forward,arrowSize,Color.blue,"+Z");       
            DrawArrow(position,up,arrowSize,Color.green,"+Y");          
            DrawArrow(position,right,arrowSize,Color.red,"+X");      

            if(showNegatives)
            {
                DrawArrow(position,-forward,arrowSize,new Color(0.6f,0.6f,1f),"-Z");
                DrawArrow(position,-up,arrowSize,new Color(0.6f,1f,0.6f),"-Y");
                DrawArrow(position,-right,arrowSize,new Color(1f,0.6f,0.6f),"-X");
            }
        }
    }

    void DrawArrow(Vector3 position,Vector3 direction,float size,Color color,string label)
    {
        Handles.color = color;
        Handles.ArrowHandleCap(
            0,
            position,
            Quaternion.LookRotation(direction),
            size,
            EventType.Repaint
        );

        Vector3 labelPosition = position + direction * size + direction * labelOffset;
        Handles.Label(labelPosition,label);
    }
}
