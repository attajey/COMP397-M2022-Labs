using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    public Transform player;
    public bool isGrounded;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<PlayerBehaviour>().transform;
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            agent.enabled = true;
            agent.SetDestination(player.position);

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            isGrounded = true;

        }
    }

    /*private void OnColissionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

        }
    }
    */

}
