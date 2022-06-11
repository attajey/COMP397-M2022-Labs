using System.Collections;
using System.Collections.Generic;
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

    private int startingWidth;
    private int startingDepth;


    // Start is called before the first frame update
    void Start()
    {
        startingWidth = width;
        startingDepth = depth;
        BuildMap();
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
