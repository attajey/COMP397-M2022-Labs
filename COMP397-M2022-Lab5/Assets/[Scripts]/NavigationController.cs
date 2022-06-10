using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public GameObject startTile;
    public List<GameObject> tiles;
    public List<GameObject> robots;

    // Start is called before the first frame update
    void Start()
    {
        startTile.GetComponent<NavMeshSurface>().BuildNavMesh();
        foreach (var tile in tiles)
        {
            tile.GetComponent<NavMeshSurface>().BuildNavMesh();
        }

        foreach (var robot in robots)
        {
            robot.SetActive(true);
        }
    }
}
