using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat1 : MonoBehaviour
{
    public static bool Melee = false;

    private void Start()
    {
        Melee = false;
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log(Melee);
        if (Melee == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (other.CompareTag("Enemy"))
                {
                    other.GetComponent<EnemyHealth>().vidaMaloso--;
                    Debug.Log("Vida del enemigo: "+ other.GetComponent<EnemyHealth>().vidaMaloso);
                }
            }
        }
        
    }
}
