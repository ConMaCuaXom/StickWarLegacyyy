using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestUpgrade : MonoBehaviour
{
    public Character character;
    public Button back;
    public Button add;


    public void Back()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void AddHP()
    {
       
    }
}
