using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rally : MonoBehaviour
{
    public BuyUnit buyUnit;
    public List<BaseSoldier> swords = null;
    public List<BaseSoldier> archers = null;
    public List<BaseSoldier> spears = null;
    public List<BaseSoldier> magics = null;
    public List<BaseSoldier> titans = null;
    public List<Miner> miners = null;
    public Dictionary<string, List<BaseSoldier>> dic = new Dictionary<string, List<BaseSoldier>>();
    public Transform[,] arrayRally = new Transform[4,12];

    public Toggle attackForward;
    public Toggle rallying;
    public Toggle defense;
    public int index ;

    
    
    


    private void Awake()
    {
        //BaseSoldier sword = swords[index];
        //SwordMan aa = sword as SwordMan;

        AddDic();
        index = 0;
        buyUnit = GetComponent<BuyUnit>();
        GetIndex();
    }

    private void Update()
    {       
        if (attackForward.isOn == true)        
            AttackForward();                                   
        if (rallying.isOn == true)       
            Rallyt1();                      
        if (defense.isOn == true)        
            Defense();       
    }

    public void AddDic()
    {        
        dic.Add("Archer", archers);        
        dic.Add("Sword", swords);        
        dic.Add("Spear", spears);        
        dic.Add("Magic", magics);       
        dic.Add("Titan", titans);
    }

    public void Defense()
    {        
        foreach (var soldier in dic.Values)
        {
            foreach (var whichSoldier in soldier)
            {     
                whichSoldier.agent.obstacle.enabled = false;
                whichSoldier.agent.agent.enabled = true;
                whichSoldier.agent.DefenseBase();
                whichSoldier.agent.animator.SetBool("Attack", false);
                whichSoldier.onDef = true;
            }
        }
    }

    public void AttackForward()
    {       
        foreach (var soldier in dic.Values)
        {
            foreach (var whichSoldier in soldier)
            {
                whichSoldier.onDef = false;
                if (whichSoldier.onAttack == false)               
                    whichSoldier.agent.AttackBase();                                                    
            }
        }
    }

    public void GetIndex()
    {
        for (int j = 0; j < 12; j++)
        {           
            for (int i = 0; i < 4; i++)
            {
                arrayRally[i, j] = GameManager.Instance.rallyPoint[index];         
                index++;
            }           
        }
    }

    

    public void Rallyt1()
    {        
        int mgPoint = (magics.Count + 3) / 4;
        int arPoint = (archers.Count + 3) / 4;
        int swPoint = (swords.Count + 3) / 4;
        int spPoint = (spears.Count + 3) / 4;
        if (magics.Count > 0)
        {
            for (int i = 0; i < magics.Count; i++)
            {
                magics[i].onDef = false;
                int mgPos = (magics.Count - 1) / 4 - (i / 4);
                float distanceM = Vector3.Distance(magics[i].transform.position, arrayRally[i % 4, mgPos].position);
                if (magics[i].onAttack == true)
                    return;
                magics[i].agent.agent.isStopped = false;                
                if (distanceM > 0.2)
                    magics[i].agent.SetDestination(arrayRally[i % 4, mgPos].position);
                else
                    magics[i].StopRallyPoint();
            }
        }

        if (archers != null)
        {
            for (int i = 0; i < archers.Count; i++)
            {
                archers[i].onDef = false;
                int arPos = (archers.Count - 1) / 4 - (i / 4);                              
                float distanceA = Vector3.Distance(archers[i].transform.position, arrayRally[i % 4, arPos + mgPoint].position);
                if (archers[i].onAttack == false)
                {
                    archers[i].agent.agent.isStopped = false;                    
                    if (distanceA > 0.2)                                           
                        archers[i].agent.SetDestination(arrayRally[i % 4, arPos + mgPoint].position);                   
                    else
                        archers[i].StopRallyPoint();
                }                                                        
            }
        }

        if (swords != null)
        {
            for (int i = 0; i < swords.Count; i++)
            {
                swords[i].onDef = false;
                int swPos = (swords.Count - 1) / 4 - (i / 4);                               
                float distanceSw = Vector3.Distance(swords[i].transform.position, arrayRally[i % 4, swPos + mgPoint + arPoint].position);
                if (swords[i].onAttack == false)
                {
                    swords[i].agent.agent.isStopped = false;                    
                    if (distanceSw > 0.2)                                   
                        swords[i].agent.SetDestination(arrayRally[i % 4, swPos + mgPoint + arPoint].position);                                           
                    else                    
                        swords[i].StopRallyPoint();                                                                
                }                
            }
        }
                  
        if (spears != null)
        {
            for (int i = 0; i < spears.Count; i++)
            {
                spears[i].onDef = false;
                int spPos = (spears.Count - 1) / 4 - (i / 4);
                if (spears[i].onAttack == true)
                    return;
                spears[i].agent.agent.isStopped = false;                
                float distanceSp = Vector3.Distance(spears[i].transform.position, arrayRally[i % 4, spPos + mgPoint + arPoint + swPoint].position);                                   
                if (distanceSp > 0.2)
                    spears[i].agent.SetDestination(arrayRally[i % 4, spPos + mgPoint + arPoint + swPoint].position);
                else
                    spears[i].StopRallyPoint();
            }
        }

        if (titans != null)
        {
            Vector3 add = new Vector3 (0, 0, 0.35f);
            for (int i = 0; i < titans.Count; i++)
            {
                titans[i].onDef = false;
                int ttPos = (titans.Count - 1) / 2 - (i / 2);
                if (titans[i].onAttack == true)
                    return;
                titans[i].agent.agent.isStopped = false;               
                if (i%2 == 0)
                {
                    float distanceTt = Vector3.Distance(titans[i].transform.position, arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position + add);
                    if (distanceTt > 0.1)
                        titans[i].agent.SetDestination(arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position + add);
                    else
                        titans[i].StopRallyPoint();                                   
                } 
                else
                {
                    float distanceTt = Vector3.Distance(titans[i].transform.position, arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position - add);
                    if (distanceTt > 0.1)
                        titans[i].agent.SetDestination(arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position - add);
                    else
                        titans[i].StopRallyPoint();                                     
                }           
                
            }
        }
    }
}
