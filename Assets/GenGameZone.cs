using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenGameZone : MonoBehaviour
{
    public CameraController cameraController;

    public int seed = 123456789;

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
    // 5 - tree

    // dictionary of tiles
    public Dictionary<Vector2Int, OverlayTile> map;


    // Sample terrain to be generated
    /*List<List<int>> gameWorld = new List<List<int>>
    {
        new List<int> { 0, 0, 0, 4, 1},
        new List<int> { 0, 0, 0, 3, 1},
        new List<int> { 0, 0, 0, 1, 4},
        new List<int> { 1, 4, 1, 1, 5},
        new List<int> { 4, 1, 1, 1, 4},
    };*/

    public int gameLength = 100;
    public int gameWidth = 100;
    public float scale = 10f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;

    List<List<int>> gameWorld = new List<List<int>>();

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

        // randomly generate grid with water along edges for premade grid
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


        // randomly generate grid give specific height and width to follow
        /*
        for (int i = 0; i < gameLength; i++)
        {
            List<int> row = new List<int>();

            for (int j = 0; j < gameWidth; j++)
            {
                int randNum = 0;

                Random.InitState(seed);

                int probability = Random.Range(0, 100);
                if (probability < 20)
                {
                    randNum = 0;
                }
                else if (probability > 20 && probability < 60)
                {
                    randNum = 1;
                }
                else if (probability > 60 && probability < 85)
                {
                    randNum = 2;
                }
                else if (probability > 85 && probability < 92)
                {
                    randNum = 3;
                }
                else if (probability > 92 && probability < 96)
                {
                    randNum = 4;
                }
                else
                {
                    randNum = 5;
                }

                row.Add(randNum);
                /*if(i == 0 || j == 0 || i == 4 || j == 4)
                {
                    gameWorld[i][j] = 0;
                } else
                {
                    gameWorld[i][j] = Random.Range(1, 6); 
                }
            }
            gameWorld.Add(row);

        }*/

        gameWorld = GeneratePerlinList(seed, scale, octaves, persistence, lacunarity);

        // generate it 
        for (int x = 0; x < gameWorld.Count; x++)
        {
            for (int y = 0; y < gameWorld[x].Count; y++)
            {
                //tilemap.SetTile(new Vector3Int(x, y, 0), (gameWorld[x][y] == 0 ? tileWater : tileLand));
                if (gameWorld[x][y] < 0.05)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileLand);
                }
                else if (gameWorld[x][y] < 0.5)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileLand);
                }
                else if (gameWorld[x][y] < 0.6)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileFlower);
                }
                else if (gameWorld[x][y] < 0.7)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileLavender);
                }
                else if (gameWorld[x][y] < 0.9)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 1), tileMountain);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, -1), tileWater);
                }
            }
        }

        Debug.Log("After generating ");

        // Instantiate map
        map = new Dictionary<Vector2Int, OverlayTile>();
        // grid based movement
        BoundsInt bounds = tilemap.cellBounds;

        //Debug.Log("Bounds min z: " + bounds.min.z);
        //Debug.Log("Bounds max z: " + bounds.max.z);

        //Debug.Log("Bounds min y: " + bounds.min.y);
        //Debug.Log("Bounds max y: " + bounds.max.y);

        //Debug.Log("Bounds min x: " + bounds.min.x);
        //Debug.Log("Bounds max x: " + bounds.max.x);


        // looping through all of the tiles drawn on map 
        for (int z = bounds.max.z; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y <= bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x <= bounds.max.x; x++)
                {
                    // Debug.Log("Location is: " + x + ", " + y + ", " + z);

                    // capture tile location
                    var tileLocation = new Vector3Int(x, y, z);

                    var tileKey = new Vector2Int(x, y);
                    // if to make sure there is a tile (in case of holes)
                    if (tilemap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                    {
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);

                        // get world position
                        var cellWorldPosition = tilemap.GetCellCenterWorld(tileLocation);

                        // position the overlay tile in the correct spot
                        // create new vector tree to put it 1 z higher so it always render in front of it
                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 2);
                        // make sure everything is on the same sorting order 
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;

                        map.Add(tileKey, overlayTile);
                        // Debug.Log("Instantiated");

                    }

                }
            }
            cameraController.CenterOnTilemap();


        }

    }

    public List<List<int>> GenerateRandomList(int seed)
    {
        Random.InitState(seed);

        List<List<int>> randomList = new List<List<int>>();

        for (int row = 0; row < gameLength; row++)
        {
            List<int> newRow = new List<int>();
            for (int col = 0; col < gameWidth; col++)
            {
                int randNum = 0;

                int probability = Random.Range(0, 100);
                Debug.Log("Random probability: " + probability);

                if (probability < 20)
                {
                    randNum = 0;
                }
                else if (probability > 20 && probability < 50)
                {
                    randNum = 1;
                }
                else if (probability > 50 && probability < 80)
                {
                    randNum = 2;
                }
                else if (probability > 80 && probability < 94)
                {
                    randNum = 3;
                }
                else if (probability > 94 && probability < 98)
                {
                    randNum = 4;
                }
                else if(probability > 98 && probability < 100)
                {
                    randNum = 5;
                } 
                else
                {
                    randNum = 0;

                }

                newRow.Add(randNum);

            }
            randomList.Add(newRow);
        }

        return randomList;
    }

    public List<List<int>> GeneratePerlinList(int seed, float scale, int octaves, float persistence, float lacunarity)
    {
        List<List<int>> world = new List<List<int>>();

        for (int x = 0; x < gameWidth; x++)
        {
            List<int> row = new List<int>();
            for (int y = 0; y < gameLength; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (float)x / scale * frequency;
                    float sampleY = (float)y / scale * frequency;
                    float perlinValue = Mathf.PerlinNoise(sampleX + seed, sampleY + seed) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;
                }
                row.Add((int)(noiseHeight * 100));
            }
            world.Add(row);
        }

        return world;
    }



}
