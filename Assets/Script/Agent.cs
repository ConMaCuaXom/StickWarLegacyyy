using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Agent : MonoBehaviour
{
    public NavMeshAgent agent;       
    public Animator animator;
    public BaseEnemy baseEnemy => GameManager.Instance.baseEnemy;
    public BasePlayer basePlayer => GameManager.Instance.basePlayer;
    public Transform defensePointP => GameManager.Instance.defensePointP;
    public Transform defensePointE => GameManager.Instance.defensePointE;
    public bool isPlayer = false;
    public bool isEnemy = false;

    public float moveSpeed = 4;
    public float angularSpeed = 20f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); 
        animator = GetComponent<Animator>();              
    }
    private void Start()
    {
        
    }
    private void Update()
    {
    }

    public void SetDestination(Vector3 target)
    {
        //RotationOnTarget(target - transform.position);
        agent.SetDestination(target);
        animator.SetBool("Run", true);
        animator.SetBool("AttackOnBase", false);
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
            RotationOnTarget(basePlayer.transform.position - transform.position);       
       if (isEnemy == true) 
            RotationOnTarget(baseEnemy.transform.position - transform.position);
    }

    public void LookAtEnemyBase()
    {
        if (isEnemy == true)        
            RotationOnTarget(basePlayer.transform.position - transform.position);       
        if (isPlayer == true)
            RotationOnTarget(baseEnemy.transform.position - transform.position);
    }

    public void AttackBase()
    {
        if (isEnemy == true)                    
            SetDestination(basePlayer.transform.position);                                
        if (isPlayer == true)                    
            SetDestination(baseEnemy.transform.position);                             
    }

    public void DefenseBase()
    {
        if (isEnemy == true)        
            SetDestination(defensePointE.transform.position);        
        if (isPlayer == true)       
            SetDestination(defensePointP.transform.position);        
    }        

    public void GoToYourBase()
    {
        if (isEnemy == true)
        {
            SetDestination(baseEnemy.transform.position);
        }
        if (isPlayer == true)
        {
            SetDestination(basePlayer.transform.position);
        }
    }
}
