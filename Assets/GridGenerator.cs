using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] int gridHeight = 10;
    [SerializeField] int gridWidth = 10;
    [SerializeField] float tileSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject newTile = Instantiate(tile, transform);

                // not like top down where would go one unit
                // go half unit side, and quarter unit up
                // Source: https://www.youtube.com/watch?v=04oQ2jOUjkU

                float posX = (x * tileSize + y * tileSize) / 2f;
                float posY = (x * tileSize - y * tileSize) / 4f;

                newTile.transform.position = new Vector2(posX, posY);
                newTile.name = x + " , " + y;
            }
        }

    }
}
