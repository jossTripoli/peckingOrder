using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenGameZone : MonoBehaviour
{

    // Once have more layers to handle then do a List<Tilemap>
    public Tilemap tilemap;

    // switch to dictionary once have a bunch of tile
    public TileBase tileLand;
    public TileBase tileWater;
    public TileBase tileMountain;
    public TileBase tileFlower;
    public TileBase tileLavender;
    public TileBase tileTree;

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    // KEY
    // 0 - water
    // 1 - grass
    // 2 - flower
    // 3 - lavender 
    // 4 - mountain

    // dictionary of tiles
    public Dictionary<Vector2Int, OverlayTile> map;

  
    // Sample terrain to be generated
    List<List<int>> gameWorld = new List<List<int>>
    {
        new List<int> { 0, 1, 2, 1, 1},
        new List<int> { 1, 1, 1, 3, 1},
        new List<int> { 1, 4, 0, 1, 1},
        new List<int> { 1, 1, 1, 1, 5},
        new List<int> { 1, 1, 1, 1, 1},
    };

    private static GenGameZone _instance;
    public static GenGameZone Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        // randomly generate grid with water along edges
        /*for (int i = 0; i < gameWorld.Count; i++)
        {
            for (int j = 0; j < gameWorld[i].Count; j++)
            {
                if(i == 0 || j == 0 || i == 4 || j == 4)
                {
                    gameWorld[i][j] = 0;
                } else
                {
                    gameWorld[i][j] = Random.Range(1, 6); ;
                }
            }
        }*/


        // generate it 
        for (int x = 0; x < gameWorld.Count; x++)
        {
            for (int y = 0; y < gameWorld[x].Count; y++)
            {
                //tilemap.SetTile(new Vector3Int(x, y, 0), (gameWorld[x][y] == 0 ? tileWater : tileLand));
                if (gameWorld[x][y] == 0)
                {
                    tilemap.SetTile(new Vector3Int(x, y, -1), tileWater);
                } else if(gameWorld[x][y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileLand);
                } else if (gameWorld[x][y] == 2)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileFlower);
                } else if (gameWorld[x][y] == 3)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileLavender);
                } else if (gameWorld[x][y] == 4)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileMountain);
                } else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileTree);
                }
            }
        }

        Debug.Log("After generating ");


        // grid based movement
        BoundsInt bounds = tilemap.cellBounds;

        Debug.Log("Bounds min z: " + bounds.min.z);
        Debug.Log("Bounds max z: " + bounds.max.z);

        Debug.Log("Bounds min y: " + bounds.min.y);
        Debug.Log("Bounds max y: " + bounds.max.y);

        Debug.Log("Bounds min x: " + bounds.min.x);
        Debug.Log("Bounds max x: " + bounds.max.x);


        // looping through all of the tiles drawn on map 
        for (int z = bounds.max.z; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y <= bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x <= bounds.max.x; x++)
                {
                    Debug.Log("Location is: " + x + ", " + y + ", " + z);

                    // capture tile location
                    var tileLocation = new Vector3Int(x, y, z);

            
                    // if to make sure there is a tile (in case of holes)
                    if (tilemap.HasTile(tileLocation))
                    {
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);

                        // get world position
                        var cellWorldPosition = tilemap.GetCellCenterWorld(tileLocation);

                        // position the overlay tile in the correct spot
                        // create new vector tree to put it 1 z higher so it always render in front of it
                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 2);
                        // make sure everything is on the same sorting order 
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;

                        Debug.Log("Instantiated");

                    }

                }
            }

    }

    }

}
