using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{

    public float timer;
    public float maxtime;
    public bool canHeal;
    void Start()
    {
        if (PlayerHealth.vidaPlayer < PlayerHealth.Maxhealth)
        {
            canHeal = true;
        }
        if (PlayerHealth.vidaPlayer == PlayerHealth.Maxhealth)
        {
            canHeal = false;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canHeal)
        {
            PlayerHealth.vidaPlayer = PlayerHealth.vidaPlayer + 2;
            if (PlayerHealth.vidaPlayer >= PlayerHealth.Maxhealth)
            {
                PlayerHealth.vidaPlayer = PlayerHealth.Maxhealth;
                
            }
            Debug.Log(PlayerHealth.vidaPlayer);
            StartCoroutine(Timercooldwn());
        }
        else
        {
            return;
        }
    }
    public IEnumerator Timercooldwn()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(5f);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
   

}
