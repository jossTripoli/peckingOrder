using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] tile;
    [SerializeField] int gridHeight = 25;
    [SerializeField] int gridWidth = 25;
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
                //int randNum = Random.Range(0, tile.Length);
                int randNum = 0;

                int probability = Random.Range(0, 100);
                if(probability < 40)
                {
                    randNum = 0;
                }  else if (probability > 40 && probability < 80)
                {
                    randNum = 1;
                } else
                {
                    randNum = 2;
                }

                Debug.Log(randNum);
                var randomTile = tile[randNum];
                GameObject newTile = Instantiate(randomTile, transform);

                // not like top down where would go one unit
                // go half unit side, and quarter unit up
                // Source: https://www.youtube.com/watch?v=04oQ2jOUjkU

                float posX = (x * tileSize - y * tileSize) / 2f;
                float posY = ((x * tileSize + y * tileSize) / 4f) + 1; 
                // if water, make lower
                if (randNum == 2)
                {
                    posY = (float)(posY - 0.25);
                }
                if (randNum == 3)
                {
                    posY = (float)(posY + 0.25);
                }

                //float randomHeight = Random.Range(0f, 1f);
                //float posY = ((x * tileSize + y * tileSize) / 4f) + (randomHeight - 1);


                newTile.transform.position = new Vector2(posX, posY);
                newTile.name = x + " , " + y;

                // fix ordering for 3d
                newTile.GetComponent<SpriteRenderer>().sortingOrder = 0 - x - y;

           

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
