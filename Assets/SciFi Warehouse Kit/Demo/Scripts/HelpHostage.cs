using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpHostage : MonoBehaviour
{
    Hostage hostage;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("In zone");
            if(Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("helping hostage");
                
            }
        }
    }
}
