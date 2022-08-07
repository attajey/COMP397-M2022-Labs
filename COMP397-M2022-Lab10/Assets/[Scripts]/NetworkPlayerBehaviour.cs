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

    private NetworkVariable<Color> materialColor = new NetworkVariable<Color>();

    // Keeps track of local positions
    private float localHorizontal;
    private float localVertical;
    private Color localColor;

    private void Awake()
    {
        materialColor.OnValueChanged += ColorOnChange;
    }

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        RandomSpawnPositionAndColor();
        meshRenderer.material.SetColor("_Color", materialColor.Value);
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

        if (meshRenderer.material.GetColor("_Color") != materialColor.Value)
        {
            meshRenderer.material.SetColor("_Color", materialColor.Value);
        }
    }

    public void RandomSpawnPositionAndColor()
    {
        var r = Random.Range(0.0f, 1.0f);
        var g = Random.Range(0.0f, 1.0f);
        var b = Random.Range(0.0f, 1.0f);
        var color = new Color(r, g, b);
        localColor = color;

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

        if (localColor != materialColor.Value)
        {
            SetClientColorServerRpc(localColor);
        }

    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float horizontal, float vertical)
    {
        horizontalPosition.Value = horizontal;
        verticalPosition.Value = vertical;
    }

    [ServerRpc]
    public void SetClientColorServerRpc(Color color)
    {
        materialColor.Value = color;

        if (meshRenderer.material.GetColor("_Color") != materialColor.Value)
        {
            meshRenderer.material.SetColor("_Color", materialColor.Value);
        }
    }

    void ColorOnChange(Color oldColor, Color newColor)
    {
        GetComponent<MeshRenderer>().material.color = materialColor.Value;
    }

}
