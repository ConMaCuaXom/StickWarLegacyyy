using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archer : BaseSoldier
{   
    
    private void Awake()
    {
        agent = GetComponent<Agent>();
        animator = GetComponent<Animator>();
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        attackRange = 10f;
        dangerRange = 15f;
        damage = 5f;
        hp = 200f;
        currentHP = hp;
        isDead = false;
        onAttack = false;
    }

    private void Update()
    {        
        targetE = GameManager.Instance.enemy.Find(e => Vector3.Distance(transform.position, e.transform.position) <= dangerRange);
        targetP = GameManager.Instance.player.Find(p => Vector3.Distance(transform.position, p.transform.position) <= dangerRange);
        if (targetE != null)
        {
            distanceE = Vector3.Distance(transform.position, targetE.transform.position);
            InDangerZone();
        }           
        if (targetP != null)
            distanceP = Vector3.Distance(transform.position, targetP.transform.position);
        
    }
    

    

    
}
