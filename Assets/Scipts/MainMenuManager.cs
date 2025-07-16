using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public Button StartButton;
    public Button exitButton;

    void Start()
    {
        Time.timeScale = 1;

        if (StartButton != null)
        {
            StartButton.onClick.RemoveAllListeners();
            StartButton.onClick.AddListener(OnStartButtonClick);
        }
        else
        {
            Debug.LogError("Play Button is not assigned in MainMenuManager!");
        }
        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Characters");
    }

    public void OnExitButtonClick()
    {
        Debug.Log("Exiting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}