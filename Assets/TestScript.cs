using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform des;
    public Transform des2;

    public bool stoped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.J))
        {
            agent.SetDestination(des.position);
        }
        if (Input.GetKey(KeyCode.K))
        {
            agent.isStopped = false;
        }
        if (Input.GetKey(KeyCode.L))
        {
            agent.isStopped = true;
        }
        if(Input.GetKey(KeyCode.M))
        {
            agent.Move(des2.position - transform.position);
        }
    }
}
