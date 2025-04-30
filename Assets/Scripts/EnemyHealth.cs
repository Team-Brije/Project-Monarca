using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    AudioSource LastBreath;
    public int vidaMaloso = 5;
    public bool isDead = false;
    public GameObject enemyfull;
    private void Update()
    {
        if(vidaMaloso == 0)
        {
            isDead = true;
            LastBreath.Play();
            Destroy(enemyfull,2f);
        }
    }
}
       
            
    
 

