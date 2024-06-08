using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SendNavmeshAgentTo : MonoBehaviour
{
    public Vector3 destination;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    [ContextMenu("SendAgent")]
    public void SendAgent(){
        agent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
