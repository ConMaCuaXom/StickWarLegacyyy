using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRes : MonoBehaviour
{
    public Text text;
    public BasePlayer player;

    private void Awake()
    {
        player = GameManager.Instance.basePlayer;
    }



    private void Update()
    {
        text.text = player.currentGold.ToString();
       
    }
}
