using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishMisiion : MonoBehaviour
{
    public GameObject Panel;
    public AudioSource AudioWin;
    public AudioSource ChasingMusic;
    //public GameObject ZonaVictoria;
    


    private void Update()
    {
        if (HelpHostage.missionactivated)
        {
            Debug.Log("Hostage Rescatado");
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && HelpHostage.missionactivated)
        {
            Panel.SetActive(true);
            ChasingMusic.Stop();
            AudioWin.Play();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            Debug.Log("Won");
        }
    }
}

