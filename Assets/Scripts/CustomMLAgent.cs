using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMLAgent : MonoBehaviour
{
    [SerializeField] int moveSpeed;

    [Range(-1f, 0f)]
    [SerializeField] float IdlePenalty;

    [SerializeField] private GameObject goal;

    private float points;

    private float distance;

    [SerializeField] ScriptableObject thoughtProcess;
    [SerializeField] SaveTest saveTest;

    bool moving = false;
    bool arrived = false;

    [SerializeField] int tokens;

    [SerializeField] private bool deleteSaveFileFirst;

    private bool useSaveFile = false;
    private bool started = false;

    private void Awake()
    {
        bool hasArrived = saveTest.ThoughtProcess.HasArrived;
        if (hasArrived && !deleteSaveFileFirst)
        {
            Debug.Log("go off the save file");
            useSaveFile = true;
        }

        ///if(saveTest.ThoughtProcess)
            //saveTest.ThoughtProcess.HasArrived = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (deleteSaveFileFirst)
        {
            ResetThoughtProcess();
            saveTest.Delete();
        }

        if (useSaveFile)
        {
            MoveAIUsingDirections();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
            IdleSystem();

        distance = goal.transform.position.z - transform.position.z;

        //Debug.Log("distance: " + distance);


        if (!arrived && !useSaveFile)
        {
            //ChooseADirection(transform.position, distance);
            ThinkThenMoveAI(transform.position, distance);
        }
        else if (!arrived && useSaveFile && !started)
        {
            MoveAIUsingDirections();
            started = true;
        }


    }

    private void ResetThoughtProcess()
    {
        saveTest.ThoughtProcess.Directions.Clear();
        saveTest.ThoughtProcess.HasArrived = false;
    }
    private void MoveAIUsingDirections()
    {
        List<CardinalDirection> directions = saveTest.ThoughtProcess.Directions;
        /*
        foreach(CardinalDirection direction in directions)
        {
            MoveAI((int) direction);
        }
        */

        StartCoroutine(MoveAISmoothly(directions));
    }

    IEnumerator MoveAISmoothly(List<CardinalDirection> directions)
    {
        int i = 0;
        foreach (CardinalDirection direction in directions)
        {
            if (!arrived)
            {
                MoveAI((int) direction);
                yield return null;
            }
            else
            {
                Debug.Log("Made it at iteration: " + i);
                break;
            }
            Debug.Log(i);
            i++;
        }
    }

    private void ThinkThenMoveAI(Vector3 currentPos, float dist)
    {
        //Pick a cardinal direction that would get me closer to the goal and reward me for doing so.
        float previousDistance = 0;
        Vector3 direction = (goal.transform.position - transform.position).normalized;
        Debug.Log("I should be going in this direction: " + direction);

        int directionToMove = (int) direction.z;
        Debug.Log("This correlates to: " + (CardinalDirection) directionToMove);

        saveTest.ThoughtProcess.Directions.Add((CardinalDirection) directionToMove);
        previousDistance = goal.transform.position.z - transform.position.z;
        MoveAI(directionToMove);
        RewardOrPunishAI(previousDistance, goal.transform.position.z - transform.position.z);
    }

    void IdleSystem()
    {
        Debug.Log("I'm not moving, penalty time");
        points -= IdlePenalty;
    }

    void RewardOrPunishAI(float previousDistance, float newDistance)
    {
        if (newDistance < previousDistance)
        {
            Debug.Log("Reward AI");
            tokens++;
        }
        else
        {
            Debug.Log("Punish AI");
            tokens--;
        }
    }
    void ChooseADirection(Vector3 previousPos, float previousDist)
    {
        int randomNum = Random.Range(0, 4);
        Debug.Log("Random Num: " + randomNum);
        Debug.Log("This correlates to: " + (CardinalDirection) randomNum);
        saveTest.ThoughtProcess.Directions.Add((CardinalDirection) randomNum);
        MoveAI(randomNum);
        float currentDist = goal.transform.position.z - transform.position.z;
        if (currentDist > previousDist)
        {
            Debug.Log("I'm going the wrong way");
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            tokens--;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            tokens++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == goal.name)
        {
            arrived = true;
            saveTest.ThoughtProcess.HasArrived = true;
            Debug.Log("I've arrived!");
        }
    }
    private void MoveAI(int randomNum)
    {
        moving = true;
        switch (randomNum)
        {
            case 0:
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
                break;
            case 1:
                transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
                break;
            case 2:
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                break;
            case 3:
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                break;
            default:
                Debug.Log("Unexpected shit happened");
                break;
        }
    }
}
