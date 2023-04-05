using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;


    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Update()
    {

        var tilemap = gameObject.GetComponentInChildren<Tilemap>();
        Debug.Log("Tilemap: " + tilemap);

        // grid based movement
        BoundsInt bounds = tilemap.cellBounds;
        Debug.Log("Bounds: " + bounds);

        // looping through all of the tiles drawn on map 
        for (int z = bounds.max.z; z < bounds.min.z; z--)
        {
            Debug.Log("z: " + z);

            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.y; x < bounds.max.x; x++)
                {
                    // capture tile location
                    var tileLocation = new Vector3Int(x, y, z);

                    // if to make sure there is a tile (in case of holes)
                    if (tilemap.HasTile(tileLocation))
                    {
                        Debug.Log("Beyonce: ");

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
    }
}
