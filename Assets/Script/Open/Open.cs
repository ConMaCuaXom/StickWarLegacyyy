using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Open : MonoBehaviour
{
    public Button play;   

    private void Awake()
    {
        //PlayerPrefs.SetInt("LvUnlock", 1);
        play.onClick.AddListener(PlayGame);
        if (PlayerPrefs.GetString("FirstTime") != "No")
        {
            PlayerPrefs.SetInt("LvUnlock", 1);
        }
    }

    private void OnDisable()
    {
        play.onClick.RemoveAllListeners();
    }
    private void PlayGame()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }   
}
