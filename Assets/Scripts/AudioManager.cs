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
    private AudioSource audioControl;

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
}

