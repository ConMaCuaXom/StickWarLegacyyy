using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings.Switch;

public class Open : MonoBehaviour
{
    public Button play;
    public Button exit;

    private void Awake()
    {
        play.onClick.AddListener(PlayGame);
    }

    private void OnDisable()
    {
        play.onClick.RemoveAllListeners();
    }
    private void PlayGame()
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }
}
