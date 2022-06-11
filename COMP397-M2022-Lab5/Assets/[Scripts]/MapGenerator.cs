using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Properties")]
    [Range(2, 10)]
    public int width;
    [Range(2, 10)]
    public int depth;

    public List<GameObject> tilesPrefab;
    public Transform tilesParent;
    public List<GameObject> randomTiles;
    public GameObject startTile;

    private int startingWidth;
    private int startingDepth;


    // Start is called before the first frame update
    void Start()
    {
        startingWidth = width;
        startingDepth = depth;
        BuildMap();

        BuildNavigationMesh();

        //foreach (var robot in robots)
        //{
        //    robot.SetActive(true);
        //}


    }



    // Update is called once per frame
    void Update()
    {
        if (width != startingWidth || depth != startingDepth)
        {
            startingWidth = width;
            startingDepth = depth;
            ResetMap();
            BuildMap();
            BuildNavigationMesh();
        }
    }

    private void BuildMap()
    {
        for (var row = 0; row < depth; row++)
        {
            for (var col = 0; col < width; col++)
            {
                if (row ==0 && col ==0)
                {
                    continue;
                }
                var randomTilePrefab = tilesPrefab[Random.Range(0, tilesPrefab.Count)];
                Vector3 tilePosition = new Vector3(col * 16.0f, 0.0f, row * 16.0f);
                Quaternion tileRotation = Quaternion.Euler(0.0f, Random.Range(0, 4) * 90.0f, 0.0f);
                var randomTile = Instantiate(randomTilePrefab, tilePosition, tileRotation);
                randomTile.transform.SetParent(tilesParent);
                randomTiles.Add(randomTile);
            }
        }
    }

    private void BuildNavigationMesh()
    {
        startTile.GetComponent<NavMeshSurface>().BuildNavMesh();

        foreach (var tile in randomTiles)
        {
            tile.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }


    private void ResetMap()
    {
        var size = randomTiles.Count;
        for (int index = 0; index < size; index++)
        {
            var tileToDestroy = randomTiles[index];
            tileToDestroy.transform.SetParent(null);
            Destroy(tileToDestroy);
        }
        randomTiles.Clear();
    }
}
