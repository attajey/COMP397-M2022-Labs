using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerBehaviour : NetworkBehaviour
{
    public float speed;
    public MeshRenderer meshRenderer;

    private NetworkVariable<float> verticalPosition = new NetworkVariable<float>();
    private NetworkVariable<float> horizontalPosition = new NetworkVariable<float>();

    // Keeps track of local positions
    private float localHorizontal;
    private float localVertical;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        RandomSpawnPosition();
    }

    void Update()
    {
        if (IsServer)
        {
            // Server Update
            ServerUpdate();
        }
        if (IsClient && IsOwner)
        {
            // Client Update
            ClientUpdate();
        }
    }

    void ServerUpdate()
    {
        transform.position = new Vector3(transform.position.x + horizontalPosition.Value,
            transform.position.y, transform.position.z + verticalPosition.Value);
    }

    public void RandomSpawnPosition()
    {
        var r = Random.Range(0.0f, 1.0f);
        var g = Random.Range(0.0f, 1.0f);
        var b = Random.Range(0.0f, 1.0f);
        var color = new Color(r, g, b);
        meshRenderer.material.SetColor("_Color", color);

        var x = Random.Range(-10.0f, 10.0f);
        var z = Random.Range(-10.0f, 10.0f);
        transform.position = new Vector3(x, 1.0f, z);
    }

    public void ClientUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        // Network Update - I need a network var for this 
        if (localHorizontal != horizontal || localVertical != vertical)
        {
            localHorizontal = horizontal;
            localVertical = vertical;

            // Update the client position on the network
            UpdateClientPositionServerRpc(horizontal, vertical);
        }

    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float horizontal, float vertical)
    {
        horizontalPosition.Value = horizontal;
        verticalPosition.Value = vertical;
    }
}
