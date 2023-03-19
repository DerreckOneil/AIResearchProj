using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAITest : MonoBehaviour
{
    [SerializeField] GameObject basicNav;
    [SerializeField] GameObject target;
    [SerializeField] int moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(basicNav.transform.position, target.transform.position, Time.deltaTime * moveSpeed);
    }
}
