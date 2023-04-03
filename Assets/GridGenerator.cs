using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] tile;
    [SerializeField] int gridHeight = 10;
    [SerializeField] int gridWidth = 10;
    [SerializeField] float tileSize = 1f;

    private Dictionary<Vector2, GameObject> tiles;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, GameObject>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                var randomTile = tile[Random.Range(0, tile.Length - 1)];
                GameObject newTile = Instantiate(randomTile, transform);

                // not like top down where would go one unit
                // go half unit side, and quarter unit up
                // Source: https://www.youtube.com/watch?v=04oQ2jOUjkU

                float posX = (x * tileSize - y * tileSize) / 2f;
                float posY = (x * tileSize + y * tileSize) / 4f;

                newTile.transform.position = new Vector3(posX, posY, 1);
                newTile.name = x + " , " + y;

                tiles[new Vector2(x, y)] = newTile;
            }
        }

    }

    public void GetTileAtPosition(Vector2 pos)
    {
        /*foreach (var t in tiles)
        {
            Debug.Log("Key = " + t.Key + "Value = " + t.Value);
        }*/
        // convert isometric world space into cartesian
        Vector2 dictionaryKey = new Vector2(2 * pos.y + pos.x, 2 * pos.y - pos.x);
        Debug.Log(tiles[dictionaryKey]);
        // return tiles[dictionaryKey];
        // tile to right: x+1   , to left: x-1
    }
}
