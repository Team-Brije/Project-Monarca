using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMisiion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && HelpHostage.missionactivated)
        {
            Debug.Log("Won");
        }
    }
}

