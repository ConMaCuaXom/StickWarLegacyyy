using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        buyUnit = GameManager.Instance.buyUnit;
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
        if (isDead == true)
            return;
        List<BaseSoldier> listEnemy = GameManager.Instance.enemy.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToList();
        if (listEnemy.Count > 0 && agent.isPlayer == true)
        {
            if (Vector3.Distance(transform.position, listEnemy[0].transform.position) <= dangerRange)
            {
                onAttack = true;
                targetE = listEnemy[0];
                distanceE = Vector3.Distance(transform.position, targetE.transform.position);
                InDangerZone();
            }
        }
        if (listEnemy.Count == 0 && agent.isPlayer == true)
        {
            onAttack = false;
            animator.SetBool("Attack", false);
            targetE = null;
            agent.agent.isStopped = false;
        }


        List<BaseSoldier> listPlayer = GameManager.Instance.player.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).ToList();
        if (listPlayer.Count > 0 && agent.isEnemy == true)
        {
            if (Vector3.Distance(transform.position, listPlayer[0].transform.position) <= dangerRange)
            {
                onAttack = true;
                targetP = listPlayer[0];
                distanceP = Vector3.Distance(transform.position, targetP.transform.position);
                InDangerZone();
            }
        }
        if (listPlayer.Count == 0 && agent.isEnemy == true)
        {
            onAttack = false;
            animator.SetBool("Attack", false);
            targetP = null;
            agent.agent.isStopped = false;
        }
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (currentHP <= 0 && isDead == false)
        {
            isDead = true;
            animator.SetTrigger("Death");
            agent.agent.enabled = false;
            if (agent.isEnemy)
            {
                GameManager.Instance.enemy.Remove(this);
            }
            if (agent.isPlayer)
            {
                GameManager.Instance.player.Remove(this);
                buyUnit.rally.magics.Remove(this);
            }
            this.DelayCall(timeToDestroy, () =>
            {
                Destroy(gameObject);

            });
        }
    }
}
