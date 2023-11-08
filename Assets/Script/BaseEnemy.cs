using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public GameObject Miner;
    public GameObject Swordman;
    public GameObject Archer;
    public GameObject Spearman;
    public GameObject TitanMan;
    public GameObject Magicman;
    public void SpawnEnemy()
    {
        GameObject swordMan = Instantiate(Swordman);
        SwordMan sw = swordMan.GetComponent<SwordMan>();
        sw.agent.isEnemy = true;
        swordMan.transform.position = transform.position;       
        
    }
}
