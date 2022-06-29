using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioBoss;
    [SerializeField] private AudioClip[] audioEnemy;
    [SerializeField] private AudioClip[] audioObjects;
    [SerializeField] private AudioClip[] audioPlayer;
    [SerializeField] private AudioClip[] audioUi;
    [SerializeField] private AudioClip[] audioPantallas;
    private static AudioSource audioControl;

   
    private void Awake()
    {
        audioControl = GetComponent<AudioSource>();
    }
    

    public void SeleccionAudioBoss (int indice, float volumen)
    {
        audioControl.PlayOneShot(audioBoss[indice], volumen);
    }
    public void SeleccionAudioEnemy(int indice, float volumen)
    {
        audioControl.PlayOneShot(audioEnemy[indice], volumen);
    }
    public void SeleccionAudioObjects(int indice, float volumen)
    {
        audioControl.PlayOneShot(audioObjects[indice], volumen);
    }
    public void SeleccionAudioPlayer(int indice, float volumen)
    {
        audioControl.PlayOneShot(audioPlayer[indice], volumen);
    }
    public void SeleccionAudioUI(int indice, float volumen)
    {
        audioControl.PlayOneShot(audioUi[indice], volumen);
    }
    public void PressButton()
    {
        audioControl.PlayOneShot(audioUi[0], 0.5f);
    }
    public void SelectedButton()
    {
        audioControl.PlayOneShot(audioUi[1], 0.5f);
    }
    public void Lighter()
    {
        audioControl.PlayOneShot(audioObjects[13], 0.5f);
    }

    public void GrabObject()
    {
        audioControl.PlayOneShot(audioUi[2], 0.5f);
    }

    public void Death()
    {
        audioControl.PlayOneShot(audioPlayer[1], 0.5f);
    }
    public void ScreenDeath()
    {
        audioControl.PlayOneShot(audioUi[1], 0.5f);
    }
    public void Rack()
    {
        audioControl.PlayOneShot(audioEnemy[1], 1f); 
    }
    public void Tv()
    {
        audioControl.PlayOneShot(audioUi[0], 0.5f);
    }
}

