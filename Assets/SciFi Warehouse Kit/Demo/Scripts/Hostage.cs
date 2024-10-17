using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage : MonoBehaviour
{
    public bool ishostage = true;
    public Transform Player;
    public NavMeshAgent hostage;
    Vector3 dest;

    private void Update()
    {
        followplayer();
    }

    public void followplayer()
    {
        dest = Player.position;
        hostage.destination = dest;
    }
}
