using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistolita : MonoBehaviour
{   
    RaycastHit hit;
    public Animator animator;
    public int municion = 5;
    bool canReload = true;
    void Update()
    {
        if(municion <= 0){
            ThirdPersonCam.canShoot = false;
        }
        if(Input.GetKeyDown(KeyCode.R) && municion < 5 && canReload){
            StartCoroutine(recarga());
        }
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        if (ThirdPersonCam.canShoot == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Shoot");
                municion--;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<EnemyHealth>().vidaMaloso--;
                        Debug.Log("Vida del enemigo: " + hit.collider.gameObject.GetComponent<EnemyHealth>().vidaMaloso);
                    }
                }   
            }
        }
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
}
