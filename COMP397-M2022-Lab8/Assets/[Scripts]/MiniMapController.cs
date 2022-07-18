using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, player.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
