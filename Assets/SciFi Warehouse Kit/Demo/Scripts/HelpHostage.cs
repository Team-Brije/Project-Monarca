using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpHostage : MonoBehaviour
{
    public GameObject hostage;
    bool canHelp;
     public static bool missionactivated = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            canHelp = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           
             canHelp = false;
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canHelp && !missionactivated)
        {
            
            hostage.gameObject.GetComponent<Hostage>().enabled = true;
            missionactivated = true;

        }
    }
}
