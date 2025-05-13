using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

public class Pistolita : MonoBehaviour
{   
    RaycastHit hit;
    public Animator animator;
    public int municion = 5;
    bool canReload = true;

    

    [Header("Bullet Sounds")]
    public AudioSource audioSource;
    [SerializeField]
    AudioSource reloadSource;
    public bool shoot = false;
    public bool isEnemy = false;
    public float Cooldown = 0;

    private float nextTimeToFire = 0.3f;

    public AudioClip pared;
    public AudioClip enemigo;
    

    bulletHitSounds bulletHitSounds = new bulletHitSounds();

    public GameObject SoundBullet;
    void Update()
    {

        Cooldown -= Time.deltaTime;
        selectSound();
        if(municion <= 0){
            ThirdPersonCam.canShoot = false;
        }
        if(Input.GetKeyDown(KeyCode.R) && municion < 5 && canReload){
            StartCoroutine(recarga());
            reloadSource.Play();
        }
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        if (ThirdPersonCam.canShoot == true)
        {
            if(Input.GetMouseButtonDown(0) && Cooldown <= 0)
            {
                animator.SetTrigger("Shoot");
                StartCoroutine(shooting());
                audioSource.PlayDelayed(0.1f);
                Cooldown = nextTimeToFire;

                municion--;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        audioSource.clip=enemigo;
                        hit.collider.gameObject.GetComponent<EnemyHealth>().vidaMaloso--;
                        Debug.Log("Vida del enemigo: " + hit.collider.gameObject.GetComponent<EnemyHealth>().vidaMaloso);
                        
                    } 
                     else if(hit.collider.gameObject.tag != "Enemy"){
                        audioSource.clip = pared;
                    }
                }   
            }
        }

        moveHitSound();
    }

    public IEnumerator recarga(){
        animator.SetBool("canShoot", false);
        animator.SetTrigger("Reload");
        canReload=false;
        yield return new WaitForSeconds(1.5f);
        municion = 5;
        canReload = true;
        animator.SetBool("canShoot", true);
    }

    public IEnumerator shooting(){
        shoot = true;
        yield return new WaitForSeconds(0.1f);
        shoot = false;
    }

    void moveHitSound(){
        if (Physics.Raycast(transform.position, transform.forward, out hit)){
            SoundBullet.transform.position=hit.point;
      }
    }
    void selectSound(){

        if (Physics.Raycast(transform.position, transform.forward, out hit)){
            if (hit.collider.gameObject.tag == "Enemy")
                    {
                        audioSource.clip=enemigo;
                    } else if(hit.collider.gameObject.tag != "Enemy"){
                        audioSource.clip = pared;
                    }
        }
}}

