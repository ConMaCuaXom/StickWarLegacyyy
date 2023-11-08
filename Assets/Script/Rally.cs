using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rally : MonoBehaviour
{
    public BuyUnit buyUnit;
    public List<SwordMan> swords = null;
    public List<Archer> archers = null;
    public List<SpearMan> spears = null;
    public List<MagicMan> magics = null;
    public List<Titan> titans = null;
    public Dictionary<int, BaseSoldier> dic;

    public Transform[,] arrayRally = new Transform[4,12];
    public int index ;
    public float distanceS;
    
    public float distanceSpear;
    public float distanceMagic;
    public float distanceTitan;

    

    private void Awake()
    {              
        index = 0;
        buyUnit = GetComponent<BuyUnit>();
        GetIndex();
    }

    private void Update()
    {
        Rallyt1();
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
                int mgPos = (magics.Count - 1) / 4 - (i / 4);
                if (magics[i].onAttack == true)
                    return;
                magics[i].agent.SetDestination(arrayRally[i % 4, mgPos].position);
                distanceMagic = Vector3.Distance(magics[i].transform.position, arrayRally[i % 4, mgPos].position);
                if (distanceMagic > 0.2)
                    magics[i].GoToRallyPoint();
                else
                    magics[i].StopRallyPoint();
            }
        }

        if (archers != null)
        {
            for (int i = 0; i < archers.Count; i++)
            {
                int arPos = (archers.Count - 1) / 4 - (i / 4);                              
                float distanceA = Vector3.Distance(archers[i].transform.position, arrayRally[i % 4, arPos + mgPoint].position);
                if (archers[i].onAttack == false)
                {
                    archers[i].agent.agent.isStopped = false;
                    if (distanceA > 0.2)
                    {
                        archers[i].GoToRallyPoint();
                        archers[i].agent.SetDestination(arrayRally[i % 4, arPos + mgPoint].position);
                    }
                    else
                        archers[i].StopRallyPoint();
                } else
                    archers[i].agent.agent.isStopped = true;
                                          
                
            }
        }

        if (swords != null)
        {
            for (int i = 0; i < swords.Count; i++)
            {
                int swPos = (swords.Count - 1) / 4 - (i / 4);
                if (swords[i].onAttack == true)
                    return;
                swords[i].agent.SetDestination(arrayRally[i % 4, swPos + mgPoint + arPoint].position);
                distanceS = Vector3.Distance(swords[i].transform.position, arrayRally[i % 4, swPos + mgPoint + arPoint].position);                                
                if (distanceS > 0.2)
                    swords[i].GoToRallyPoint();
                else
                    swords[i].StopRallyPoint();
            }
        }
                  
        if (spears != null)
        {
            for (int i = 0; i < spears.Count; i++)
            {
                int spPos = (spears.Count - 1) / 4 - (i / 4);
                if (spears[i].onAttack == true)
                    return;
                spears[i].agent.SetDestination(arrayRally[i % 4, spPos + mgPoint + arPoint + swPoint].position);
                distanceSpear = Vector3.Distance(spears[i].transform.position, arrayRally[i % 4, spPos + mgPoint + arPoint + swPoint].position);                                   
                if (distanceSpear > 0.2)
                    spears[i].GoToRallyPoint();
                else
                    spears[i].StopRallyPoint();
            }
        }

        if (titans != null)
        {
            Vector3 add = new Vector3 (0, 0, 0.35f);
            for (int i = 0; i < titans.Count; i++)
            {
                int ttPos = (titans.Count - 1) / 2 - (i / 2);
                if (titans[i].onAttack == true)
                    return;
                if (i%2 == 0)
                {
                    titans[i].agent.SetDestination(arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position + add);
                    distanceTitan = Vector3.Distance(titans[i].transform.position, arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position + add);
                } else
                {
                    titans[i].agent.SetDestination(arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position - add);
                    distanceTitan = Vector3.Distance(titans[i].transform.position, arrayRally[i % 2, ttPos + mgPoint + arPoint + swPoint + spPoint].position - add);
                }           
                if (distanceTitan > 0.1)
                    titans[i].GoToRallyPoint();
                else
                    titans[i].StopRallyPoint();
            }
        }




    }




}
