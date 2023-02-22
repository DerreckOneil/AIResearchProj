using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.destination = target.transform.position;
    }
}
