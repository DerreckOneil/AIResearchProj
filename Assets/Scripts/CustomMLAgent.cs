using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMLAgent : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] int penalty;

    private int points;

    [SerializeField] ScriptableObject thoughtProcess;
    [SerializeField] SaveTest saveTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PointSystem();
        ChooseADirection();
    }

    void PointSystem()
    {
        points -= penalty;
    }

    void ChooseADirection()
    {
        int randomNum = Random.Range(0, 4);
        Debug.Log("Random Num: " + randomNum);
        Debug.Log("This correlates to: " + (CardinalDirection)randomNum);
        saveTest.ThoughtProcess.Directions.Add((CardinalDirection)randomNum);
        MoveAI(randomNum);
    }

    private void MoveAI(int randomNum)
    {
        switch(randomNum)
        {
            case 0:
                transform.Translate(Vector3.forward);
                break;
            case 1:
                transform.Translate(Vector3.back);
                break;
            case 2:
                transform.Translate(Vector3.left);
                break;
            case 3:
                transform.Translate(Vector3.right);
                break;
            default:
                Debug.Log("Unexpected shit happened");
                break;
        }
    }
}
