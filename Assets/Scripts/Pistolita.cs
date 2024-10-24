using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistolita : MonoBehaviour
{   
    RaycastHit hit;

    void Update()
    {
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        if (ThirdPersonCam.canShoot == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
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
}
