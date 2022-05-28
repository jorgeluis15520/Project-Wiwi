using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    public static bool isPause = false;
    private Scene currentScene;
    public string sceneName;
    public GameObject pauseMenuPanel;


    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName != "Menu" && sceneName != "Credits")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause)
                {
                    ResumeGame();
                }
                else if (!isPause && pauseMenuPanel != null)
                {
                    PauseGame();
                }
            }
        }

        else if (sceneName == "Credits")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
        }
        //else if(pauseMenuPanel==null)
        //{

        //}
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        isPause = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1f;
    }
}
