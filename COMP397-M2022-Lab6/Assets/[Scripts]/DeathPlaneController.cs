using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = playerSpawnPoint.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}
