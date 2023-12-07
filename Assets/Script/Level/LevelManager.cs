using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static int currentLv;

    public BuyUnit buyUnit;

    

    


    private void Awake()
    {
        switch (LevelManager.currentLv)
        {
            case 1:
                Level1(); break;
            case 2:
                Level2(); break;
            case 3:
                Level3(); break;
            case 4:
                Level4(); break;
            case 5:
                Level5(); break;
            case 6:
                Level6(); break;
            case 7:
                Level7(); break;
            case 8:
                Level8(); break;
            case 9:
                Level9(); break;
            case 10:
                Level10(); break;
        }
    }

    public void Level1()
    {
        buyUnit.buyArcher.gameObject.SetActive(false);
        buyUnit.buySpearMan.gameObject.SetActive(false);
        buyUnit.buyTitanMan.gameObject.SetActive(false);
        buyUnit.buyMagicMan.gameObject.SetActive(false);
    }
    public void Level2()
    {
        
        buyUnit.buySpearMan.gameObject.SetActive(false);
        buyUnit.buyTitanMan.gameObject.SetActive(false);
        buyUnit.buyMagicMan.gameObject.SetActive(false);
    }
    public void Level3()
    {
        
        buyUnit.buyTitanMan.gameObject.SetActive(false);
        buyUnit.buyMagicMan.gameObject.SetActive(false);
    }
    public void Level4()
    {
        
        buyUnit.buyMagicMan.gameObject.SetActive(false);
    }
    public void Level5()
    {
        
    }
    public void Level6()
    {
        
    }
    public void Level7()
    {
        
    }
    public void Level8()
    {
        
    }
    public void Level9()
    {
        
    }
    public void Level10()
    {
       
    }


}
