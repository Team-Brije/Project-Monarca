using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    //Teste codigo sirve para que lo podamos asignar al npc que necesitemos y haga los calculos necesarios
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    States currentState;

    public List<GameObject> waypoints = new List<GameObject>();
    public List<GameObject> waypointsSch = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        //podemos cambiar a que estado queremos que este desde el inicio
        currentState = new Patrol(this.gameObject, agent, anim, player,waypoints,waypointsSch);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
        Debug.Log(currentState.ToString());
    }
}
