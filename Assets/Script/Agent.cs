using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Agent : MonoBehaviour
{
    public NavMeshAgent agent;
    public BaseEnemy BaseEnemy = null;
    public BasePlayer BasePlayer = null;
    public bool isPlayer = false;
    public bool isEnemy = false;

    public float angularSpeed = 20f;   
    
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();       
        BaseEnemy = GameManager.Instance.baseEnemy;
        BasePlayer = GameManager.Instance.basePlayer;       
    }

    private void Update()
    {
        
    }

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }
   

    public void MoveForward()
    {            
        agent.Move(transform.forward * Time.deltaTime);
    } 

    public void RotationOnTarget(Vector3 target)
    {
        Quaternion quaternionTarget =  Quaternion.LookRotation(target, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternionTarget,angularSpeed * Time.deltaTime);
    }

    public void LookAtYourBase()
    {      
       if (isPlayer == true)                            
            RotationOnTarget(BasePlayer.transform.position - transform.position);       
       if (isEnemy == true) 
            RotationOnTarget(BaseEnemy.transform.position - transform.position);
    }

    public void LookAtEnemyBase()
    {
        if (isEnemy == true)        
            RotationOnTarget(BasePlayer.transform.position - transform.position);       
        if (isPlayer == true)
            RotationOnTarget(BaseEnemy.transform.position - transform.position);
    }
    


    
}
