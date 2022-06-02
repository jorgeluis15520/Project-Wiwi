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
    public GameObject mainMenuPanel;
    public GameObject coleccionablePanel;
    public GameObject coleccionable1Panel;
    public GameObject coleccionable2Panel;
    public GameObject coleccionable3Panel;
    public  bool isMainMenu = true;

    

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "SampleScene" && isMainMenu)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (!isMainMenu && sceneName != "Credits")
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
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMainMenu = false;
        //Time.timeScale = 1f;
        //isPause = false;
        //isMainMenu = false;
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
        SceneManager.LoadScene("SampleScene");
        isMainMenu = true;
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1f;
        isMainMenu = false;
    }

    public void ColeccionablePanel()
    {
        pauseMenuPanel.SetActive(false);
        coleccionablePanel.SetActive(true);
    }

    public void BackPauseMenu()
    {
        coleccionablePanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void Coleccionable1()
    {
        if (Inventary.haveCollect1)
        {
            coleccionable1Panel.SetActive(true);
            coleccionablePanel.SetActive(false);
        }
    }
    public void Coleccionable2()
    {
        if (Inventary.haveCollect2)
        {
            coleccionable2Panel.SetActive(true);
            coleccionablePanel.SetActive(false);
        }
    }
    public void Coleccionable3()
    {
        if (Inventary.haveCollect3)
        {
            coleccionable3Panel.SetActive(true);
            coleccionablePanel.SetActive(false);
        }
    }

    public void BacktoColeccionablePanel()
    {
        if(coleccionable1Panel.activeSelf == true)
        {
            coleccionable1Panel.SetActive(false);
            coleccionablePanel.SetActive(true);
        }
        if (coleccionable2Panel.activeSelf == true)
        {
            coleccionable2Panel.SetActive(false);
            coleccionablePanel.SetActive(true);
        }
        if (coleccionable3Panel.activeSelf == true)
        {
            coleccionable3Panel.SetActive(false);
            coleccionablePanel.SetActive(true);
        }
    }
}
