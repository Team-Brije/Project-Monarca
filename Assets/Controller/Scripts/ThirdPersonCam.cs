using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;

    public CameraStyle currentStyle;

    public static bool canShoot;

    public Animator animator;
    public GameObject Gun;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    private void Start()
    {
        Gun.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //print("CanYou shoot: " + canShoot);
        // switch styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic); Gun.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat); Gun.SetActive(true);
        //if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.Topdown);

        //Can shoot only in Combat Camera
        
        
        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        if(currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            canShoot=false;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        else if(currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;
            playerObj.forward = dirToCombatLookAt.normalized;
            canShoot=true;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true); if (Input.GetKeyDown(KeyCode.Alpha2)) animator.SetTrigger("Draw"); 
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true); if (Input.GetKeyDown(KeyCode.Alpha1)) animator.SetTrigger("Holster"); 
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true); 

        currentStyle = newStyle;
    }
}
