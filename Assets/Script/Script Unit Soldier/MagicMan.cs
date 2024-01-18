using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MagicMan : BaseSoldier
{
    public Character character;
    public GameObject soldierOfMe;
    public GameObject explosion;
    public ParticleSystem destroyedMid;
    public Rally rally => GameManager.Instance.rally;
    public RallyEnemy rallyEnemy => GameManager.Instance.rallyEnemy;
    public Transform spawnPoint;
    public TargetDynamicSound targetDynamicSound = null;

    public float timeForSpawn;
    public int numberOfSoldier;
    public int maxTiny;
    public float timeForHololo => character.timeForHololo;
    
    private void Awake()
    {
        agent = GetComponent<Agent>();                                    
        attackRange = character.attackRange;
        dangerRange = character.dangerRange;
        damage = character.attackDamage;
        hp = character.hp;
        timeToDestroy = character.timeToDestroy;
        currentHP = hp;
        maxTiny = character.maxTiny;
        targetDynamicSound = GetComponent<TargetDynamicSound>();
        targetDynamicSound.Initialized();
        isDead = false;
        onAttack = false;
        numberOfSoldier = 0;
        timeForSpawn = 0;
    }

    private void Update()
    {
        HPinCamera();
        if (onDef == true || hulolo == true || pushBack == true || isDead  == true)
            return;
        TargetOnWho();
        SpawnSoldier();
        WiOrLo();
        

    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if(rally.magics.Contains(this) == true)
            {
                rally.magics.Remove(this);
                buyUnit.limitUnitCurrent -= 5;
            }         
        }
        if (isDead && agent.isEnemy)
        {           
            if (testEnemy.rallyE.magicsE.Contains(this) == true)
            {
                testEnemy.rallyE.magicsE.Remove(this);
                testEnemy.limitUnitCurrent -= 5;
            }
                
        }
    }

    public void Explosion()
    {
        if (GameManager.Instance.allMagicAndEffect.Contains(destroyedMid) == false)
        {
            GameObject explosionn = Instantiate(explosion);
            destroyedMid = explosionn.GetComponent<ParticleSystem>();
            GameManager.Instance.allMagicAndEffect.Add(destroyedMid);
        }
        else
        {
            destroyedMid.gameObject.SetActive(true);
        }
        if (agent.isPlayer && targetE!=null)
        {                      
            destroyedMid.gameObject.transform.position = targetE.transform.position;           
            for (int i = GameManager.Instance.enemy.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.enemy[i];
                if (Vector3.Distance(soldier.transform.position, targetE.transform.position) <= 2f)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }
        }
        if (agent.isEnemy && targetP != null)
        {                      
            destroyedMid.gameObject.transform.position = targetP.transform.position;
            for (int i = GameManager.Instance.player.Count - 1; i >= 0; i--)
            {
                BaseSoldier soldier = GameManager.Instance.player[i];
                if (Vector3.Distance(soldier.transform.position, targetP.transform.position) <= 2f)
                {
                    soldier.TakeDamage(damage);
                    soldier.PushBack();
                }
            }
        }
        soundDynamic.PlayOneShot("MagicMan_Explosion");
    }

    

    public void SpawnSoldier()
    {
        timeForSpawn += Time.deltaTime;
        if (timeForSpawn >= timeForHololo && numberOfSoldier < maxTiny)
        {
            hulolo = true;
            if (agent.agent.enabled == true)
                agent.agent.isStopped = true;
            agent.animator.SetTrigger("SpawnTiny");                       
            timeForSpawn = 0;
        }
    }

    public void HololoBegin()
    {       
        GameObject Soldier = Instantiate(soldierOfMe, spawnPoint.position, transform.rotation);
        Tiny tiny = Soldier.GetComponent<Tiny>();              
        tiny.daddy = this;       
        numberOfSoldier++;       
        if (agent.isPlayer)
        {
            tiny.agent.isPlayer = true;
            tiny.agent.isEnemy = false;
            tiny.WhichColor();
            tiny.WhichBody("Body1");
            tiny.WhichHead("Head1");
            tiny.WhichWeapon("Weapon1");
            rally.tinys.Add(tiny);            
            GameManager.Instance.player.Add(tiny);            
        }
        if (agent.isEnemy)
        {
            tiny.agent.isPlayer = false;
            tiny.agent.isEnemy = true;
            tiny.WhichColor();
            tiny.WhichBody("Body0");
            tiny.WhichHead("Head0");
            tiny.WhichWeapon("Weapon0");
            rallyEnemy.tinysE.Add(tiny);            
            GameManager.Instance.enemy.Add(tiny);            
        }
        soundDynamic.PlayOneShot("MagicMan_SpawnTiny");
    }

    public void HololoFinish()
    {
        hulolo = false;
        if (agent.agent.enabled == true)
            agent.agent.isStopped = false;
    }

    public override void AttackOnBaseEnemy()
    {
        if (baseEnemy.currentHP <= 0)
            return;
        if (agent.isPlayer && onAttack == false)
        {
            if (Vector3.Distance(transform.position, agent.baseEnemy.transform.position) <= attackRange)
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
        if (agent.isEnemy && onAttack == false)
        {
            if (Vector3.Distance(transform.position, agent.basePlayer.transform.position) <= attackRange)
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
    public override void TargetIsNull()
    {
        onAttack = false;
        if (agent.isPlayer && Vector3.Distance(transform.position, agent.baseEnemy.transform.position) >= attackRange)
            agent.agent.isStopped = false;
        if (agent.isEnemy && Vector3.Distance(transform.position, agent.basePlayer.transform.position) >= attackRange)
            agent.agent.isStopped = false;
        agent.animator.SetBool("Attack", false);
        if (agent.isEnemy)
            targetP = null;
        if (agent.isPlayer)
            targetE = null;
    }

    public override void AttackingOff()
    {
        attacking = false;
        if (agent.agent.enabled == true && attackOnBase == false)
            agent.agent.isStopped = false;
    }
    public override void HowToAttackE(List<BaseSoldier> list)
    {
        if (list.Count > 0)
        {
            if (Vector3.Distance(transform.position, list[0].transform.position) <= dangerRange)
            {
                onAttack = true;
                targetE = list[0];
                distanceE = Vector3.Distance(transform.position, targetE.transform.position);
                InDangerZone();
            }
            else
            {
                TargetIsNull();
            }
            if (targetE != null && baseEnemy.currentHP > 0)
            {
                if (Vector3.Distance(transform.position, agent.baseEnemy.transform.position) <= Vector3.Distance(transform.position, targetE.transform.position))
                {
                    onAttack = false;
                    AttackOnBaseEnemy();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, agent.baseEnemy.transform.position) <= attackRange)
                    AttackOnBaseEnemy();
            }
        }

        if (list.Count == 0)
        {
            TargetIsNull();
        }
    }

    public override void HowToAttackP(List<BaseSoldier> list)
    {
        if (list.Count > 0)
        {
            if (Vector3.Distance(transform.position, list[0].transform.position) <= dangerRange)
            {
                onAttack = true;
                targetP = list[0];
                distanceP = Vector3.Distance(transform.position, targetP.transform.position);
                InDangerZone();
            }
            else
            {
                TargetIsNull();
            }
            if (targetP != null)
            {
                if (Vector3.Distance(transform.position, agent.basePlayer.transform.position) <= Vector3.Distance(transform.position, targetP.transform.position))
                {
                    onAttack = false;
                    AttackOnBaseEnemy();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, agent.basePlayer.transform.position) <= attackRange)
                    AttackOnBaseEnemy();
            }
        }
        if (list.Count == 0)
        {
            TargetIsNull();
        }
    }

    public override void DamageForBase()
    {
        base.DamageForBase();
        soundDynamic.PlayOneShot("MagicMan_Explosion");
    }
}
