using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MLTest : Agent
{
    [SerializeField] private GameObject goal;
    [SerializeField] private Transform startPos;
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.position);
        sensor.AddObservation(gameObject.transform.rotation.y); //Will be the rotation in which the agent is facing.
        sensor.AddObservation(goal.transform.position);
    }


    public override void OnActionReceived(ActionBuffers actions) //Need to study what can be done here...
    {
        
    }

    public override void OnEpisodeBegin()
    {
        gameObject.transform.position = startPos.position; //Teleport back and try again
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
