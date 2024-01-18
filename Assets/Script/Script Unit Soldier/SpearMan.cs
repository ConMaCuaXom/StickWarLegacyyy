using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpearMan : BaseSoldier
{
    public Character character;
    public TargetDynamicSound targetDynamicSound = null;

    public enum SoundAttack
    {
        SpearMan_Attack1,
        SpearMan_Attack2, 
        SpearMan_Attack3
    }
    private void Awake()
    {
        agent = GetComponent<Agent>();                
        attackRange = character.attackRange;
        dangerRange = character.dangerRange;
        damage = character.attackDamage;
        hp = character.hp;
        timeToDestroy = character.timeToDestroy;
        currentHP = hp;
        targetDynamicSound = GetComponent<TargetDynamicSound>();
        targetDynamicSound.Initialized();
        isDead = false;
        onAttack = false;
    }

    private void Update()
    {
        HPinCamera();
        if (onDef == true || isDead == true || pushBack == true)
            return;
        TargetOnWho();
        WiOrLo();
        

    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if (buyUnit.rally.spears.Contains(this) == true)
            {
                buyUnit.rally.spears.Remove(this);
                buyUnit.limitUnitCurrent -= 3;
            }          
        }
        if (isDead && agent.isEnemy)
        {            
            if (testEnemy.rallyE.spearsE.Contains(this) == true)
            {
                testEnemy.rallyE.spearsE.Remove(this);
                testEnemy.limitUnitCurrent -= 3;
            }
                
        }
    }
    public override void AttackOnTarget()
    {
        base.AttackOnTarget();
        SoundAttack rdSoundAttack = (SoundAttack)Random.Range(0, 3);
        soundDynamic.PlayOneShot(rdSoundAttack.ToString());
    }
    public override void DamageForBase()
    {
        base.DamageForBase();
        SoundAttack rdSoundAttack = (SoundAttack)Random.Range(0, 3);
        soundDynamic.PlayOneShot(rdSoundAttack.ToString());
    }
}
