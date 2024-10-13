using UnityEngine;
using SPWN;

public class PhysxTest : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] int testDistance = 10;

    void Start()
    {
       //Dbug.Physx("Physx Started");
    }

    void Update()
    {

        if (Physx.OverlapCylinder(Target.transform.position, transform.position, testDistance))
        {
            Dbug.Physics($"{Target.name} is in the Airspace of {transform.name} within a lateral distance of {testDistance}");
        }
    }

    void OnDrawGizmos()
    {
        Dbug.Circle(transform.position, testDistance, Vector3.up, Utils.HexToColor("FFFF00"));
    }
}












