using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //public TextMeshProUGUI textMeshProUGUI;
    public static int vidaPlayer = 5;
    public static int Maxhealth = 10;
    private void Awake()
    {
       // textMeshProUGUI = GameObject.Find("PlayerLife").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        //textMeshProUGUI.text = vidaPlayer.ToString();
        
        if(vidaPlayer == 0)
        {
            Debug.Log("Player is dead"); 
            //SceneManager.LoadScene("Menu");
        }

    }

}
