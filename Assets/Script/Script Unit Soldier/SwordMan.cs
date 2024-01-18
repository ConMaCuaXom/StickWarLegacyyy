using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordMan : BaseSoldier
{
    public Character character;
    public TargetDynamicSound targetDynamicSound = null;

    public enum SoundAttack
    {
        SwordMan_Attack1,
        SwordMan_Attack2,
        SwordMan_Attack3,
        SwordMan_Attack4
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
        onDef = false;
        nearBase = false;
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
            if (buyUnit.rally.swords.Contains(this) == true)
            {
                buyUnit.rally.swords.Remove(this);
                buyUnit.limitUnitCurrent--;
            }          
        }
        if (isDead && agent.isEnemy)
        {
            if (testEnemy.rallyE.swordsE.Contains(this) == true)
            {
                testEnemy.rallyE.swordsE.Remove(this);
                testEnemy.limitUnitCurrent--;
            }
                
        }
    }
    public override void AttackOnTarget()
    {
        base.AttackOnTarget();
        SoundAttack rdSoundAttack = (SoundAttack)Random.Range(0, 4);
        soundDynamic.PlayOneShot(rdSoundAttack.ToString());
    }
    public override void DamageForBase()
    {
        base.DamageForBase();
        SoundAttack rdSoundAttack = (SoundAttack)Random.Range(0, 4);
        soundDynamic.PlayOneShot(rdSoundAttack.ToString());
    }
}
