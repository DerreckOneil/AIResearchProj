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

    [SerializeField] private Rigidbody rbd;

    float distanceToTarget;

    [SerializeField] private bool manualTraining;
    bool hitGoal;


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.position); //Where the agent is
        sensor.AddObservation(gameObject.transform.rotation.y); //Will be the rotation in which the agent is facing.
        sensor.AddObservation(goal.transform.position); //Where the goal is
        
    }


    public override void OnActionReceived(ActionBuffers actions) //Need to study what can be done here...
    {
        int moveX = actions.DiscreteActions[0]; 
        int moveZ = actions.DiscreteActions[1];

        Vector3 addForce = new Vector3(0, 0, 0);

        switch (moveX)
        {
            case 0: addForce.x = 0; break;
            case 1: addForce.x = -1; break;
            case 2: addForce.x = 1; break;
        }
        switch (moveZ)
        {
            case 0: addForce.z = 0; break;
            case 1: addForce.z = -1; break;
            case 2: addForce.z = 1; break;
        }

        float moveSpeed = 5f;
        
        rbd.velocity = addForce * moveSpeed + new Vector3(0, rbd.velocity.y,0);

        float previousDistanceToTarget = distanceToTarget;
        distanceToTarget = Vector3.Distance(gameObject.transform.position, goal.transform.position);
        if(!hitGoal)
        {
            Debug.Log("Move towards the target!");
           //Vector3.MoveTowards(gameObject.transform.position, goal.transform.position, 1f); //Move towards the goal and don't stop until 1 unit before.
        }
        else
        {
            Debug.Log("We're done here");
            SetReward(1f);

        }
        
        if(distanceToTarget > previousDistanceToTarget) //Meaning the AI is going in reverse
        {
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            Debug.Log("Small IdlePenalty for doing nothing.");
            SetReward(0.1f); //Small IdlePenalty for doing nothing. 
        }
        
    }

    public override void OnEpisodeBegin() //Actually works kinda like an onEnable(). Starts at the beginning of runtime
    {
        distanceToTarget = Vector3.Distance(gameObject.transform.position, goal.transform.position);
        gameObject.transform.position = startPos.position; //Teleport back and try again
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("Do something here?");
        Debug.Log("So if the setting is set to Heuristics, does this code run?");

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1:
                discreteActions[0] = 1;
                break;
            case 0:
                discreteActions[0] = 0;
                break;
            case 1:
                discreteActions[0] = 2;
                break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1:
                discreteActions[1] = 1;
                break;
            case 0:
                discreteActions[1] = 0;
                break;
            case 1:
                discreteActions[1] = 2;
                break;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Goal")
        {
            Debug.Log("Collided with the goal, we're done.");
            hitGoal = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Go forward");
            transform.Translate(Vector3.forward * Time.deltaTime);

        }
        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Go Left");
            transform.Translate(Vector3.left * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Go Right");
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Go Back");
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        */
    }
}
