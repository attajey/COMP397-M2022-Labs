using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public List<GameObject> tiles;
    public List<GameObject> robots;
    // Start is called before the first frame update
    void Start()
    {
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
