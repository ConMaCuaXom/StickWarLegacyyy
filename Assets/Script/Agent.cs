using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Agent : MonoBehaviour
{
    public NavMeshAgent agent;   
    public NavMeshObstacle obstacle;
    public Animator animator;
    public BaseEnemy BaseEnemy = null;
    public BasePlayer BasePlayer = null;
    public Transform DefensePointP = null;
    public Transform DefensePointE = null;
    public bool isPlayer = false;
    public bool isEnemy = false;

    public float moveSpeed = 4;
    public float angularSpeed = 20f;           

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); 
        animator = GetComponent<Animator>();
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        BaseEnemy = GameManager.Instance.baseEnemy;
        BasePlayer = GameManager.Instance.basePlayer;
        DefensePointP = GameManager.Instance.defensePointP;
        DefensePointE = GameManager.Instance.defensePointE;
    }

    private void Update()
    {
        
    }

    public void SetDestination(Vector3 target)
    {
        RotationOnTarget(target - transform.position);
        agent.SetDestination(target);
        animator.SetBool("Run", true);
    }
   

    public void MoveForward()
    {            
        agent.Move(transform.forward * moveSpeed * Time.deltaTime);
        animator.SetBool("Run", true);
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

    public void AttackBase()
    {
        if (isEnemy == true)                    
            SetDestination(BasePlayer.transform.position);                                
        if (isPlayer == true)                    
            SetDestination(BaseEnemy.transform.position);                             
    }

    public void DefenseBase()
    {
        if (isEnemy == true)        
            SetDestination(DefensePointE.transform.position);        
        if (isPlayer == true)       
            SetDestination(DefensePointP.transform.position);        
    }        
}
