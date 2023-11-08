using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagicMan : BaseSoldier
{

    private void Awake()
    {
        agent = GetComponent<Agent>();
        animator = GetComponent<Animator>();
        basePlayer = GameManager.Instance.basePlayer;
        baseEnemy = GameManager.Instance.baseEnemy;
        attackRange = 10f;
        damage = 5f;
        hp = 200f;
        currentHP = hp;
    }

    private void Update()
    {
        
        targetE = GameManager.Instance.enemy.Find(e => Vector3.Distance(transform.position, e.transform.position) <= attackRange);
        targetP = GameManager.Instance.player.Find(e => Vector3.Distance(transform.position, e.transform.position) <= attackRange);
    }


}
