using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class States 
{
    public enum STATE{
        PATROL, PURSUE, ATTACK, SEARCHING, ALERT, IDLE
    };

    public enum EVENT{
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;

    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected List<GameObject> Waypoints = new List<GameObject>();
    protected States nextState;
    protected NavMeshAgent agent;
    protected List<GameObject> WaypointsSch = new List<GameObject>();

    float visDist = 10;
    float visDistAlert = 15;
    float visAngle = 30;
    float visAngleAlert = 40;
    float shootDist = 7;
    

    public bool veJugador= false;
    public bool resetSee=false;

    public bool disparando = false;


    public States(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, List<GameObject> _waypoints, List<GameObject> _waypointsSch)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        Waypoints = _waypoints;
        WaypointsSch = _waypointsSch;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() {stage = EVENT.EXIT; }

    public States Process(){
        if(stage == EVENT.ENTER) Enter();
        if(stage == EVENT.UPDATE) Update();
        if(stage == EVENT.EXIT){
            Exit();
            return nextState;
        }
        return this;
    }

    //funcion del profe para checar si se puede ver al jugador
    public bool CanSeePlayer(){
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        if(direction.magnitude < visDist && angle < visAngle){
            return true;
        }
        return false;
    }
    public bool CanSeePlayer2(){
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        if(direction.magnitude < visDistAlert && angle < visAngleAlert){
            return true;
        }
        return false;
    }
    //Funcion del profe para checar si puede disparar
    public bool CanAttackPlayer(){
        Vector3 direction = player.position - npc.transform.position;
        if(direction.magnitude < shootDist){
            return true;
        }
        return false;
    }
}

public class Patrol : States
{
    
    int currentIndex = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, List<GameObject> _waypoints, List<GameObject> _waypointsSch) : base(_npc, _agent, _anim, _player,_waypoints, _waypointsSch){
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    

    public override void Enter()
    {
        veJugador=false;
       
        
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < Waypoints.Count; i++)
        {
            GameObject thisWP = Waypoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }
        //tengo entendido que aqui configura e inicia el navmesh creo
        //tambien aqui suele configurar la velocidad del npc
        base.Enter();
    }

    public override void Update()
    {
        //Debug.Log(CanSeePlayer());
        if (agent.remainingDistance < 1)
        {

            if (currentIndex >= Waypoints.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(Waypoints[currentIndex].transform.position);
        }
        //aqui comprobar la distancia entre el jugador para que empieze a atacar
        //aqui ejuctamos tambien el codigo para que el navmesh funcione correctamente
        if (CanSeePlayer()){
                
                      //aqui va el codigo cuando detecta al jugador
                      
            nextState = new Pursue(npc, agent, anim, player, Waypoints,WaypointsSch);
            stage = EVENT.EXIT;
        }
        //por algun motivo no usa base.update aqui
        //base.Update();
    }
    public override void Exit()
    {
        //aqui suele usar el resset trigger anim
        
    }
}

public class Pursue : States
{
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, List<GameObject> _waypoints, List<GameObject> _waypointsSch) : base( _npc, _agent, _anim, _player,_waypoints, _waypointsSch){
        name = STATE.PURSUE;
        agent.speed = 5;
        agent.isStopped = false;
        //cambiar la velocidad del npc 
    }

    public override void Enter()
    {
        
        //alguna animacion de correr o parecido
        veJugador=true;
        base.Enter();
    }

    public override void Update()
    {
        //dice que esto evita que el enemigo se quede pensando en si nos puede ver
        agent.SetDestination(player.position);
        //dice que esto nos indica si nos esta persiguiendo
        if(agent.hasPath)
        {
            //ifpara checar si esta a distancia par atacarnos
            if(CanAttackPlayer()){
                nextState = new Attack(npc, agent, anim, player, Waypoints, WaypointsSch);
                disparando = true;
                stage = EVENT.EXIT;
            }
            else if(!CanSeePlayer()){ //comprobamos que todavia el enemigo puede ver al jugador
                resetSee=true;
                nextState = new Alert(npc, agent, anim, player, Waypoints,WaypointsSch);
                stage = EVENT.EXIT;
            }
        }
        //base.Update();
    }

    public override void Exit()
    {
        //aqui suele usar el resset trigger anim
        base.Exit();
    }
}

public class Attack : States
{
    bool canAttack = true;
    float cooldown = 0;
    float rotationSpeed = 5f;
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, List<GameObject> _waypoints, List<GameObject> _waypointsSch) : base( _npc, _agent, _anim, _player, _waypoints, _waypointsSch){
        name = STATE.ATTACK;
        agent.speed = 0;
        agent.isStopped = true;
        //conseguir alguna variable necesaria dentro de npc, el profe usa el ejemplo de llamar a un audio source
    }

    public override void Enter()
    {
        //animacion de ataque
        veJugador=true;
        //el profe pone el ejemplo de ejecutar el sonido aqui
        base.Enter();
    }

    public override void Update()
    {
        //Debug.Log(PlayerHealth.vidaPlayer);
        if (canAttack)
        {
            PlayerHealth.vidaPlayer -= 3;
            canAttack = false;
            cooldown = 0;
            anim.SetTrigger("Shoot");
            disparando = true; 

        }
        if (!canAttack)
        {
            cooldown += Time.deltaTime;
            if(cooldown >= 5) { canAttack = true; }
        }
        //Funciones para atacar o conseguir donde esta el jugador
        //El profe pone estos ejemplos para tanto para calcular la direccion como la rotacion en si para atacar los paso por si llegan a servir
        
        //necesitamos calcular la dirección y el ángulo entre el npc y el jugador
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        //para hacer la rotación necesitamos hacer un Quaternion
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        
        if(!CanAttackPlayer()){
            nextState = new Searching(npc, agent, anim, player, Waypoints, WaypointsSch);
            resetSee=true;
            stage = EVENT.EXIT;
        }
        //base.Update();
    }
}

public class Alert : States
{
    float cooldown = 0;
    bool canReturn = false;
    public Alert(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, List<GameObject> _waypoints, List<GameObject> _waypointsSch) : base( _npc, _agent, _anim, _player,_waypoints,_waypointsSch){
        name = STATE.ALERT;
        agent.speed = 0;
        agent.isStopped = true;
    }

    public override void Enter()
    {
        veJugador=false;
        //que se quede quieto pero con mayor visibilidad
        base.Enter();

    }

    public override void Update()
    {
        npc.transform.Rotate(0,1,0);
        if (canReturn)
        {
            nextState = new Idle(npc, agent, anim, player, Waypoints,WaypointsSch);
            stage = EVENT.EXIT;
        }
        if (!canReturn)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= 5f) { canReturn = true; }
        }
        //que se quede quieto un rato antes de volver a patrullar
        if (CanSeePlayer2()){
            nextState = new Pursue(npc, agent, anim, player, Waypoints,WaypointsSch);
            stage = EVENT.EXIT;
        }
        //base.Update();
    }

    public override void Exit()
    {
        //tendria q comentar algo mas pero no quiero
        base.Exit();
    }
}

public class Searching : States{
    int currentIndex = -1;
    float cooldown = 0;
    bool canReturn = false;
    public Searching(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, List<GameObject> _waypoints, List<GameObject> _waypointsSch) : base(_npc, _agent, _anim, _player,_waypoints,_waypointsSch){
        name = STATE.SEARCHING;
        agent.speed = 4;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        veJugador=false;
        //cambiar la velocidad
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < WaypointsSch.Count; i++)
        {
            GameObject thisWP = WaypointsSch[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }
        //tengo entendido que aqui configura e inicia el navmesh creo
        //tambien aqui suele configurar la velocidad del npc
        base.Enter();
    }
    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= WaypointsSch.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(WaypointsSch[currentIndex].transform.position);
        }
        if (canReturn)
        {
            nextState = new Alert(npc, agent, anim, player, Waypoints, WaypointsSch);
            stage = EVENT.EXIT;
        }
        if (!canReturn)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= 6f) { canReturn = true; }
        }
        //una funcion para detectar al jugador si esta cerca 
        //aqui hacer que camine una distancia mas hacia adelante y luego regresar a idle
        if (CanSeePlayer()){
            nextState = new Pursue(npc, agent, anim, player, Waypoints, WaypointsSch);
            stage = EVENT.EXIT;
        }
        //base.Update();
    }
    public override void Exit()
    {
        //resetear anmaciones o cosas asi
        base.Exit();
    }
}

public class Idle : States
{
    float cooldown = 0;
    bool canReturn = false;
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player, List<GameObject> _waypoints, List<GameObject> _waypointsSch) : base( _npc, _agent, _anim, _player, _waypoints, _waypointsSch){
        name = STATE.IDLE;
        agent.speed = 0;
        agent.isStopped = true;
    }

    public override void Enter()
    {
        //veJugador=false;
        //detenerlo que se quede pensando en las malas acciones que a realizado
        base.Enter();
    }

    public override void Update()
    {
        if (canReturn)
        {
            nextState = new Patrol(npc, agent, anim, player, Waypoints,WaypointsSch);
            stage = EVENT.EXIT;
        }
        if (!canReturn)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= 5f) { canReturn = true; }
        }
        //aqui hacer que despues de un rato regrese a patrulleo
        //meto la funcion pa que detecte si el jugador pasa por delante
        if (CanSeePlayer()){
            nextState = new Pursue(npc, agent, anim, player,Waypoints, WaypointsSch);
            stage = EVENT.EXIT;
        }
        //base.Update();
    }

    public override void Exit()
    {
        //reset de animaciones
        base.Exit();
    }
}