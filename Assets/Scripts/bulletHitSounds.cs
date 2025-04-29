using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHitSounds : MonoBehaviour
{
    Pistolita pistolita;
    bool shoot, isEnemy;
    public AudioClip pared;
    public AudioClip enemigo;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        shoot = pistolita.shoot;
        isEnemy = pistolita.isEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        selectSFX();
        playSFX();
    }

    void selectSFX(){
        if( isEnemy == true){
                //play enemy sfx
                audioSource.clip = enemigo;
            }else{
                //play wall sfx
                audioSource.clip = pared;
            }
    }
    public void playSFX(){
        if(shoot == true){
            audioSource.Play();
        }
    }
}
