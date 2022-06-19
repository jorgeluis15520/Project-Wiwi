using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;
public class Manager : MonoBehaviour
{
    public static bool isPause = false;
    private Scene currentScene;
    private string sceneName;
    public GameObject pauseMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject collectablePanel;
    public GameObject collectable1Panel;
    public GameObject collectable2Panel;
    public GameObject collectable3Panel;
    public GameObject deathPanel;
    public  bool isMainMenu = true;
    //Opciones
    public GameObject optionsMenuPanel;
    public Slider slider;
    public float sliderValue;


    public Toggle toggle;
    public TMP_Dropdown resolutionsDropDown;
    private Resolution[] resolutions;

    //audio
    //public AudioSource audioMenu;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "Tutorial" && isMainMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
            AudioListener.volume = slider.value;
        }

        

        if (sceneName != "Credits")
        {
            CheckResolution();
        }

        if (sceneName != "Credits" && Screen.fullScreen)
        {
            toggle.isOn = true;
        }

        else if (sceneName != "Credits" && !Screen.fullScreen)
        {
            toggle.isOn = false;
        }
       

        //if (isMainMenu)
        //{
        //    audioMenu.Play();
        //}

    }

    private void Update()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        

        //if (!isMainMenu)
        //{
        //    audioMenu.Stop();
        //}

        if (!isMainMenu && sceneName != "Credits")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause && isMainMenu)
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
                SceneManager.LoadScene("Tutorial");
            }
        }

      
        Death();
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMainMenu = false;
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
        isMainMenu = false;
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isMainMenu = true;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Tutorial");
        isMainMenu = true;
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1f;
        isMainMenu = false;
    }

    public void CollectablePanel()
    {
        pauseMenuPanel.SetActive(false);
        collectablePanel.SetActive(true);
    }

    public void BackPauseMenu()
    {
        collectablePanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void Collectable1()
    {
        if (Inventary.haveCollect1)
        {
            collectable1Panel.SetActive(true);
            collectablePanel.SetActive(false);
        }
    }
    public void Collectable2()
    {
        if (Inventary.haveCollect2)
        {
            collectable2Panel.SetActive(true);
            collectablePanel.SetActive(false);
        }
    }
    public void Collectable3()
    {
        if (Inventary.haveCollect3)
        {
            collectable3Panel.SetActive(true);
            collectablePanel.SetActive(false);
        }
    }

    public void Death()
    {
        if(PlayerController.isDeath == true)
        {
            deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void BacktoCollectablePanel()
    {
        if(collectable1Panel.activeSelf == true)
        {
            collectable1Panel.SetActive(false);
            collectablePanel.SetActive(true);
        }
        if (collectable2Panel.activeSelf == true)
        {
            collectable2Panel.SetActive(false);
            collectablePanel.SetActive(true);
        }
        if (collectable3Panel.activeSelf == true)
        {
            collectable3Panel.SetActive(false);
            collectablePanel.SetActive(true);
        }
    }

    public void Options()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }
    public void BackMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsMenuPanel.SetActive(false);
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        
    }

    public void RestartLevel()
    {
        PlayerController.isDeath = false;
        deathPanel.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PrincipalMenu()
    {
        PlayerController.isDeath = false;
        deathPanel.SetActive(false);
        SceneManager.LoadScene("Tutorial");
    }


    public void CheckResolution()
    {
        resolutions = Screen.resolutions;
        resolutionsDropDown.ClearOptions();
        List<string> options = new List<string>();
        int resolutionActual = 0;

        for(int i = 0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz"; ;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionActual = i;
            }
        }

        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = resolutionActual;
        resolutionsDropDown.RefreshShownValue();

        resolutionsDropDown.value = PlayerPrefs.GetInt("numberResolution", 0);
    }
    
    public void ActiveFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void ChangeResolution(int indexResolution)
    {
        PlayerPrefs.SetInt("numberResolution", resolutionsDropDown.value);

        Resolution resolution = resolutions[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
