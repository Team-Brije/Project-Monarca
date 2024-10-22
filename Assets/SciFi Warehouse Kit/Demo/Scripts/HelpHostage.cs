using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpHostage : MonoBehaviour
{
    public GameObject hostage;
    bool canHelp;
    bool missionactivated = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("In zone");
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
            Debug.Log("helping hostage");
            hostage.gameObject.GetComponent<Hostage>().enabled = true;
            missionactivated = true;

        }
    }
}
