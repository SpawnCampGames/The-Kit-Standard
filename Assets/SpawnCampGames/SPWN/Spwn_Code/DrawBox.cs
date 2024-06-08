using UnityEngine;

public class DrawBox : MonoBehaviour
{
    public BoxCollider boxCollider;
    public AudioClip clip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.matrix = boxCollider.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
    }
}
