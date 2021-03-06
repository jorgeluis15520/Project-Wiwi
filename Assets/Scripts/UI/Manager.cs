using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

using TMPro;
public class Manager : MonoBehaviour
{
    public AudioSource MenuAudio;
    public AudioClip clipTuto;
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
    public GameObject nextLevelPanel;
    public GameObject panelLevelLoad;
    public GameObject pauseOptions;
    public GameObject selectNiveles;
    public GameObject continuePanel;
    public static bool isMainMenu = true;
    //Opciones
    public GameObject optionsMenuPanel;
    public Slider slider;
    public float sliderValue;

    public bool Menu_Options = false;
    public bool Pause_Options = false;
    //
    public PlayableDirector Pd;
    public TimelineClip clip;



    public Toggle toggle;
    //public TMP_Dropdown resolutionsDropDown;
    //private Resolution[] resolutions;

    public int width;
    public int height;

    public int sceneIndex;

    //audio
    //public AudioSource audioMenu;
    private void Awake()
    {
        if(mainMenuPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
    }
    private void Start()
    {
        

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "Tutorial2" && isMainMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
            AudioListener.volume = slider.value;
        }

        if (sceneName != "Tutorial2")
        {
            mainMenuPanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMainMenu = false;

        }

        if (sceneName == "Nivel_1")
        {
            StartCoroutine(Level1Ready());
        }
        if ( sceneName == "Nivel_2")
        {
            StartCoroutine(Level1Ready());
        }
        //if (sceneName != "Credits")
        //{
        //    CheckResolution();
        //}

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
                    if(CloseVideo.videoPlaying == true)

                    {

                    }
                    else
                    {
                        PauseGame();
                    }
                    
                }
            }
        }

        if (sceneName == "Credits")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Tutorial2");
                Time.timeScale = 0f;
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
        MenuAudio.clip = clipTuto;
        MenuAudio.Play();
        if(Pd != null)
        {
            Pd.Play();
        }
        
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
        if (panelLevelLoad.activeSelf)
        {
            panelLevelLoad.SetActive(false);
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Tutorial2");
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

    public void OptionsPause()
    {
        pauseOptions.SetActive(true);
        pauseMenuPanel.SetActive(false);
        Pause_Options = true;
    }

    public void BackPauseMenu()
    {
        collectablePanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void Collectable1()
    {
        if (CheckCollects.haveCollect1)
        {
            collectable1Panel.SetActive(true);
            collectablePanel.SetActive(false);
        }
    }
    public void Collectable2()
    {
        if (CheckCollects.haveCollect2)
        {
            collectable2Panel.SetActive(true);
            collectablePanel.SetActive(false);
        }
    }
    public void Collectable3()
    {
        if (CheckCollects.haveCollect3)
        {
            collectable3Panel.SetActive(true);
            collectablePanel.SetActive(false);
        }
    }

    public void Death()
    {
        if (PlayerController.isDeath == true)
        {
            deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void BacktoCollectablePanel()
    {
        if (collectable1Panel.activeSelf == true)
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
        Menu_Options = true;
    }
    public void BackMainMenu()
    {
        if(Menu_Options == true)
        {
            mainMenuPanel.SetActive(true);
            optionsMenuPanel.SetActive(false);
            Menu_Options = false;
        }

        if(Pause_Options == true)
        {
            pauseMenuPanel.SetActive(true);
            optionsMenuPanel.SetActive(false);
            Pause_Options = false;
        }
        
    }

    public void BackMenuPause()
    {
        pauseMenuPanel.SetActive(true);
        pauseOptions.SetActive(false);
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
        Time.timeScale = 0f;
        SceneManager.LoadScene("Tutorial2");
        continuePanel.SetActive(false);
    }

    public void EscenariosButton()
    {
        mainMenuPanel.SetActive(false);
        selectNiveles.SetActive(true);

    }

    public void SelectTutorial()
    {
        selectNiveles.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMainMenu = false;
        MenuAudio.clip = clipTuto;
        MenuAudio.Play();
    }
    public void SelectNivel1()
    {
        SceneManager.LoadScene("Nivel_1");
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMainMenu = false;
        MenuAudio.Play();
    }
    public void SelectNivel2()
    {
        SceneManager.LoadScene("Nivel_2");
        Time.timeScale = 1f;
        isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMainMenu = false;
        MenuAudio.Play();
    }

    public void SelectNiveles_Menu()
    {
        selectNiveles.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void LoadLevel1()
    {
        StartCoroutine(Level1Load());
    }


    IEnumerator Level1Load()
    {
        yield return new WaitForSeconds(1f);
        nextLevelPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator Level1Ready()
    {
        panelLevelLoad.SetActive(true);
        yield return new WaitForSeconds(1f);
        panelLevelLoad.SetActive(false);
    }

    public void Continue()
    {
        Time.timeScale = 0f;
        continuePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ActiveFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void SetWidth(int newWidth)
    {
        width = newWidth;

    }

    public void SetHeight(int newHeight)
    {
        height = newHeight;

    }


    public void ChangeResolution()
    {

        Screen.SetResolution(width, height, Screen.fullScreen);

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Tutorial2");
    }
}