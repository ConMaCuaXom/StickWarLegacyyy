using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperTitan : BaseSoldier
{
    public Character character;
    private void Awake()
    {
        agent = GetComponent<Agent>();
        attackRange = character.attackRange;
        dangerRange = character.dangerRange;
        damage = character.attackDamage;
        hp = character.hp;
        timeToDestroy = character.timeToDestroy;
        currentHP = hp;
        isDead = false;
        onAttack = false;
    }

    private void Update()
    {
        AttackOnBaseEnemy();
        TargetOnWho();
        WiOrLo();
    }
    public override void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0 && isDead == false)
        {            
            isDead = true;
            RandomDeath();
            agent.agent.isStopped = true;          
            GameManager.Instance.enemy.Remove(this);
            wol.CampaignComplete();
            this.DelayCall(timeToDestroy, () =>
            {
                Destroy(gameObject);
            });
        }
    }
    public override void AttackOnTarget()
    {     
        if (targetP != null)
        {
            for (int i = GameManager.Instance.player.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.player[i];
                if (Vector3.Distance(soldier.transform.position, targetP.transform.position) <= 2f && soldier != null)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }
        }
    }

    public override void AttackOnBaseEnemy()
    {       
        if (onAttack == false)
        {
            if (nearBase)
            {
                agent.agent.isStopped = true;
                attackOnBase = true;
                agent.animator.SetBool("Run", false);
                agent.animator.SetBool("AttackOnBase", true);
                agent.LookAtEnemyBase();
            }
            else
            {
                agent.AttackBase();
            }
        }
    }
}
