using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMission : MonoBehaviour
{
    public static bool Missioncomp = false;
    private void Update()
    {
        if(!Missioncomp)
        {
            Debug.Log("Rescue the Hostage");
        }

    }
}
