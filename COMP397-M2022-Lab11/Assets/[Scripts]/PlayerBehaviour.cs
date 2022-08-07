using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Onscreen Controls")]
    public Joystick leftJoystick;
    


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //GameSaveManager.Instance().SaveGame(transform);

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        // Keyboard Input ( fallbaclk ) + Onscreen Joystick
        float x = Input.GetAxis("Horizontal") + leftJoystick.Horizontal;
        float z = Input.GetAxis("Vertical") + leftJoystick.Vertical;

        // Onscreen Joystick 
        

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * maxSpeed * Time.deltaTime);
        if (Input.GetButton("Jump") && isGrounded )
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.L))
        {
            //controller.enabled = false;
            GameSaveManager.Instance().LoadGame(transform);
            //controller.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //controller.enabled = false;
            GameSaveManager.Instance().SaveGame(transform);
            //controller.enabled = true;
        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void OnAButton_Pressed()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public void OnSaveButton_Pressed()
    {
        GameSaveManager.Instance().SaveGame(transform);

    }

    public void OnLoadButton_Pressed()
    {
        GameSaveManager.Instance().LoadGame(transform);

    }
}
