using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RallyEnemy : MonoBehaviour
{   
    public List<BaseSoldier> swordsE = null;
    public List<BaseSoldier> archersE = null;
    public List<BaseSoldier> spearsE = null;
    public List<BaseSoldier> magicsE = null;
    public List<BaseSoldier> titansE = null;
    public List<BaseSoldier> tinysE = null;
    public List<BaseSoldier> minersE = null;
    public List<CrossbowmanDefender> crbE = null;
    public Dictionary<string, List<BaseSoldier>> dicE = new Dictionary<string, List<BaseSoldier>>();
    public Transform[,] arrayRallyE = new Transform[4, 12];

    public Toggle attackForwardE;
    public Toggle rallyingE;
    public Toggle defenseE;
    public int indexE;

    private void Awake()
    {
        //BaseSoldier sword = swords[index];
        //SwordMan aa = sword as SwordMan;

        AddDicE();
        indexE = 0;
        crbE = GameManager.Instance.crossbowmanDefendersE;
        GetIndexE();
    }

    private void Update()
    {
        if (attackForwardE.isOn == true)
            AttackForwardE();
        if (rallyingE.isOn == true)
            Rallyt1E();
        if (defenseE.isOn == true)
            DefenseE();
    }

    public void AddDicE()
    {
        dicE.Add("Archer", archersE);
        dicE.Add("Sword", swordsE);
        dicE.Add("Spear", spearsE);
        dicE.Add("Magic", magicsE);
        dicE.Add("Titan", titansE);
        dicE.Add("Tiny", tinysE);
    }

    public void DefenseE()
    {
        foreach (var soldier in dicE.Values)
        {
            foreach (var whichSoldier in soldier)
            {
                whichSoldier.agent.obstacle.enabled = false;
                whichSoldier.agent.agent.enabled = true;
                whichSoldier.agent.DefenseBase();
                whichSoldier.agent.animator.SetBool("Attack", false);
                whichSoldier.agent.animator.SetBool("AttackOnBase", false);
                whichSoldier.onDef = true;
                whichSoldier.onAttack = false;
            }
        }
        foreach (CrossbowmanDefender crb in crbE)
        {
            crb.GoAttackPoint();
            crb.AttackDef();
        }
    }

    public void AttackForwardE()
    {
        foreach (var soldier in dicE.Values)
        {
            foreach (var whichSoldier in soldier)
            {
                whichSoldier.onDef = false;
                if (whichSoldier.onAttack == false)
                {
                    whichSoldier.AttackOnBaseEnemy();
                }
            }
        }
        foreach (CrossbowmanDefender crb in crbE)
        {
            crb.GoDefensePoint();
        }

    }

    public void GetIndexE()
    {
        for (int j = 0; j < 12; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                arrayRallyE[i, j] = GameManager.Instance.rallyPointE[indexE];
                indexE++;
            }
        }
    }



    public void Rallyt1E()
    {
        foreach (CrossbowmanDefender crb in crbE)
        {
            crb.GoDefensePoint();
        }
        int mgPoint = (magicsE.Count + 3) / 4;
        int arPoint = (archersE.Count + 3) / 4;
        int swPoint = (swordsE.Count + 3) / 4;
        int spPoint = (spearsE.Count + 3) / 4;
        int tnPoint = (tinysE.Count + 3) / 4;
        if (magicsE.Count > 0)
        {
            for (int i = 0; i < magicsE.Count; i++)
            {
                magicsE[i].onDef = false;
                int mgPos = (magicsE.Count - 1) / 4 - (i / 4);
                float distanceM = Vector3.Distance(magicsE[i].transform.position, arrayRallyE[i % 4, mgPos].position);
                if (magicsE[i].onAttack == true || magicsE[i].hulolo == true)
                    return;
                magicsE[i].agent.agent.isStopped = false;
                if (distanceM > 0.2)
                    magicsE[i].agent.SetDestination(arrayRallyE[i % 4, mgPos].position);
                else
                    magicsE[i].StopRallyPoint();
            }
        }

        if (archersE != null)
        {
            for (int i = 0; i < archersE.Count; i++)
            {
                archersE[i].onDef = false;
                int arPos = (archersE.Count - 1) / 4 - (i / 4);
                float distanceA = Vector3.Distance(archersE[i].transform.position, arrayRallyE[i % 4, arPos + mgPoint].position);
                if (archersE[i].onAttack == false)
                {
                    archersE[i].agent.agent.isStopped = false;
                    if (distanceA > 0.2)
                        archersE[i].agent.SetDestination(arrayRallyE[i % 4, arPos + mgPoint].position);
                    else
                        archersE[i].StopRallyPoint();
                }
            }
        }

        if (swordsE != null)
        {
            for (int i = 0; i < swordsE.Count; i++)
            {
                swordsE[i].onDef = false;
                int swPos = (swordsE.Count - 1) / 4 - (i / 4);
                float distanceSw = Vector3.Distance(swordsE[i].transform.position, arrayRallyE[i % 4, swPos + mgPoint + arPoint].position);
                if (swordsE[i].onAttack == false)
                {
                    swordsE[i].agent.agent.isStopped = false;
                    if (distanceSw > 0.2)
                        swordsE[i].agent.SetDestination(arrayRallyE[i % 4, swPos + mgPoint + arPoint].position);
                    else
                        swordsE[i].StopRallyPoint();
                }
            }
        }

        if (spearsE != null)
        {
            for (int i = 0; i < spearsE.Count; i++)
            {
                spearsE[i].onDef = false;
                int spPos = (spearsE.Count - 1) / 4 - (i / 4);
                if (spearsE[i].onAttack == true)
                    return;
                spearsE[i].agent.agent.isStopped = false;
                float distanceSp = Vector3.Distance(spearsE[i].transform.position, arrayRallyE[i % 4, spPos + mgPoint + arPoint + swPoint].position);
                if (distanceSp > 0.2)
                    spearsE[i].agent.SetDestination(arrayRallyE[i % 4, spPos + mgPoint + arPoint + swPoint].position);
                else
                    spearsE[i].StopRallyPoint();
            }
        }

        if (tinysE.Count > 0)
        {
            for (int i = 0; i < tinysE.Count; i++)
            {
                tinysE[i].onDef = false;
                int tnPos = (tinysE.Count - 1) / 4 - (i / 4);
                float distanceTn = Vector3.Distance(tinysE[i].transform.position, arrayRallyE[i % 4, tnPos + mgPoint + arPoint + swPoint + spPoint].position);
                if (tinysE[i].onAttack == false)
                {
                    tinysE[i].agent.agent.isStopped = false;
                    if (distanceTn > 0.2)
                        tinysE[i].agent.SetDestination(arrayRallyE[i % 4, tnPos + mgPoint + arPoint + swPoint + spPoint].position);
                    else
                        tinysE[i].StopRallyPoint();
                }
            }
        }

        if (titansE != null)
        {
            Vector3 add = new Vector3(0, 0, 0.35f);
            for (int i = 0; i < titansE.Count; i++)
            {
                titansE[i].onDef = false;
                int ttPos = (titansE.Count - 1) / 2 - (i / 2);
                if (titansE[i].onAttack == true)
                    return;
                titansE[i].agent.agent.isStopped = false;
                if (i % 2 == 0)
                {
                    float distanceTt = Vector3.Distance(titansE[i].transform.position, arrayRallyE[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint + tnPoint].position + add);
                    if (distanceTt > 0.1)
                        titansE[i].agent.SetDestination(arrayRallyE[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint + tnPoint].position + add);
                    else
                        titansE[i].StopRallyPoint();
                }
                else
                {
                    float distanceTt = Vector3.Distance(titansE[i].transform.position, arrayRallyE[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint + tnPoint].position - add);
                    if (distanceTt > 0.1)
                        titansE[i].agent.SetDestination(arrayRallyE[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint + tnPoint].position - add);
                    else
                        titansE[i].StopRallyPoint();
                }

            }
        }
    }
}
