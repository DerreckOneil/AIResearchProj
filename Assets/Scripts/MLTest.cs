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
    float distanceToTarget;
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.position); //Where I am
        sensor.AddObservation(gameObject.transform.rotation.y); //Will be the rotation in which the agent is facing.
        sensor.AddObservation(goal.transform.position); //Where the goal is
    }


    public override void OnActionReceived(ActionBuffers actions) //Need to study what can be done here...
    {
        float previousDistanceToTarget = distanceToTarget;
        distanceToTarget = Vector3.Distance(gameObject.transform.position, goal.transform.position);
        if(gameObject.transform.position != goal.transform.position)
        {
            Debug.Log("Move towards the target!");
            Vector3.MoveTowards(gameObject.transform.position, goal.transform.position, 1f); //Move towards the goal and don't stop until 1 unit before.
        }
        else
        {
            Debug.Log("We're done here");
        }
        
        if(distanceToTarget > previousDistanceToTarget) //Meaning the AI is going in reverse
        {
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            SetReward(0.1f);
        }
        
    }

    public override void OnEpisodeBegin() //Actually works kinda like an onEnable(). Starts at the beginning of runtime
    {
        Debug.Log("Does this work like start or onenable?"); 
        distanceToTarget = Vector3.Distance(gameObject.transform.position, goal.transform.position);
        gameObject.transform.position = startPos.position; //Teleport back and try again
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {

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
