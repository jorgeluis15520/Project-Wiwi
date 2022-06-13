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
    public string sceneName;
    public GameObject pauseMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject coleccionablePanel;
    public GameObject coleccionable1Panel;
    public GameObject coleccionable2Panel;
    public GameObject coleccionable3Panel;
    public  bool isMainMenu = true;

    //Opciones
    public GameObject opcionesMenuPanel;
    public Slider slider;
    public float sliderValue;


    public Toggle toggle;
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;

    //audio
    //public AudioSource audioMenu;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
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

    public void Opciones()
    {
        mainMenuPanel.SetActive(false);
        opcionesMenuPanel.SetActive(true);
    }
    public void BackMainMenu()
    {
        mainMenuPanel.SetActive(true);
        opcionesMenuPanel.SetActive(false);
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        
    }

    public void CheckResolution()
    {
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for(int i = 0; i<resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height + " @ " + resoluciones[i].refreshRate + "hz"; ;
            opciones.Add(opcion);

            if(resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }

        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();

        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }
    
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);

        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

}
