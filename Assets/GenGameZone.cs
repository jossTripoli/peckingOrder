using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenGameZone : MonoBehaviour
{

    // If you have more layers to handle then do a List<Tilemap>
    public Tilemap tilemap;

    // If you have a lot of tiles, think about some list, dictionary or structure
    public TileBase tileLand;
    public TileBase tileWater;

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;
    
    // KEY
    // 0 - water
    // 1 - grass

    // Sample terrain to be generated
    List<List<int>> gameWorld = new List<List<int>>
    {
        new List<int> { 1, 0, 0, 0, 0},
        new List<int> { 0, 1, 1, 1, 0},
        new List<int> { 0, 1, 1, 1, 0},
        new List<int> { 0, 1, 1, 1, 0},
        new List<int> { 0, 0, 0, 0, 0},
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
        // generate it 
        for (int x = 0; x < gameWorld.Count; x++)
        {
            for (int y = 0; y < gameWorld[x].Count; y++)
            {
                // tilemap.SetTile(new Vector3Int(x, y, 0), (gameWorld[x][y] == 0 ? tileWater : tileLand));
                if (gameWorld[x][y] == 0)
                {
                    tilemap.SetTile(new Vector3Int(x, y, -1), tileWater);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileLand);
                }
            }
        }

        // grid based movement
        BoundsInt bounds = tilemap.cellBounds;

        // looping through all of the tiles drawn on map 
        /*for(int z = bounds.max.z; z < bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.y; x < bounds.max.x; x++)
                {
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
                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                        // make sure everything is on the same sorting order 
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                    }

                }
            }


        } */



        Debug.Log("Start function executed.");
        StartCoroutine(CallAfterStart());


    }



    IEnumerator CallAfterStart()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second
        Debug.Log("Function called after 1 second.");

        // grid based movement
        BoundsInt bounds = tilemap.cellBounds;

        Debug.Log("Bounds min z: " + bounds.min.z);
        Debug.Log("Bounds max z: " + bounds.max.z);

        Debug.Log("Bounds min y: " + bounds.min.y);
        Debug.Log("Bounds max y: " + bounds.max.y);

        Debug.Log("Bounds min x: " + bounds.min.x);
        Debug.Log("Bounds max x: " + bounds.max.x);

        Debug.Log("Bjork");

        // looping through all of the tiles drawn on map 
        for (int z = bounds.max.z; z > bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    Debug.Log("BEYONCE AND ADELE");

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
                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                        // make sure everything is on the same sorting order 
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                    }

                }
            }


        }
        // Call your function here
    }

}
