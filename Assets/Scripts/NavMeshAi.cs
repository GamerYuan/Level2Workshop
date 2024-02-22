using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAi : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //agent.Move(agent.transform.forward * Time.deltaTime * 5f);
        agent.SetDestination(destination.position);
    }
}
