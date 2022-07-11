using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1 : MonoBehaviour
{
    public Manager manager;
    void Update()
    {
        if (DoorAnimations.endLevel)
        {
            manager.LoadLevel1();
        }
    }
}
