using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject cam;
    public Transform whereTo;

    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cam.transform.position = whereTo.position;
            StartCoroutine(Stop());
        }
    }

    public IEnumerator Stop()
    {
        col.enabled = false;
        yield return new WaitForSeconds(2);
        col.enabled = true;
    }
}
