using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public Manager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Continue());
        }
    }

    IEnumerator Continue()
    {
        manager.nextLevelPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        manager.Continue();
    }
}
