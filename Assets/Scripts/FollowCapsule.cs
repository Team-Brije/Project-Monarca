using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCapsule : MonoBehaviour
{
    public Transform Capsule;
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Capsule != null)
        {
            gameObject.transform.position = Capsule.position;
            gameObject.transform.rotation = Capsule.rotation;
        }
        else
        {
            Debug.LogWarning("Capsule reference is missing.");
        }
    }
}
