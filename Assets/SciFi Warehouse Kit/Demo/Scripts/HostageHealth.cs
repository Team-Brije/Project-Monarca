using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageHealth : MonoBehaviour
{
    public static int vidaHostage = 5;
    public static int MaxhealthHostage = 20;
    private void Awake()
    {
        // textMeshProUGUI = GameObject.Find("PlayerLife").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        //textMeshProUGUI.text = vidaPlayer.ToString();

        if (vidaHostage == 0)
        {
            Debug.Log("Hostage is dead");
            //SceneManager.LoadScene("Menu");
        }

    }
}
