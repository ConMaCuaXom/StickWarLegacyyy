using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiny : BaseSoldier
{
    public Character character;
    public MagicMan daddy;
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
        if (onDef == true || pushBack == true || isDead == true)
            return;
        TargetOnWho();
        WiOrLo();
        
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        if (isDead && agent.isPlayer)
        {
            if (daddy.rally.tinys.Contains(this) == true)
            {
                daddy.rally.tinys.Remove(this);
                daddy.numberOfSoldier--;
            }           
        }
        if (isDead && agent.isEnemy)
        {
            if (testEnemy.rallyE.tinysE.Contains(this) == true)
            {
                testEnemy.rallyE.tinysE.Remove(this);
                daddy.numberOfSoldier--;
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
