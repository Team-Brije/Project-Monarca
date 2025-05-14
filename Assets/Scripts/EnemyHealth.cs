using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    AudioSource deathSound;   
    public int vidaMaloso = 5;
    public bool isDead = false;
    public GameObject enemyfull;
    private void Update()
    {
        if(vidaMaloso == 0)
        {
            isDead = true;
            deathSound.Play();
            Destroy(enemyfull);
        }
    }
}
       
            
    
 

