using UnityEngine;
using UnityEngine.Tilemaps;

 /* Controls:
  *  Pan camera: wasd, arrows, or move mouse to edge of screen
  *  Zoom camera: mouse wheel
  *  Reset camera: right-click or middle mouse button click
  */ 
public class CameraController : MonoBehaviour
{
    // Camera movement speed
    public float moveSpeed = 5f;

    // Camera zoom speed
    public float zoomSpeed = 300f;

    // Max and min zoom levels
    public float minZoom = 1f;
    public float maxZoom = 10f;

    // Original camera position and zoom level
    private Vector3 originalPosition;
    private float originalZoom;

    // Current zoom level
    private float currentZoom = 5f;

    // Edge scroll width
    public float edgeScrollWidth = 10f;

    // Tilemap to center camera on
    public Tilemap tilemap;

    // Save original camera position and zoom level when script starts
    private void Start()
    {
        originalPosition = transform.position;
        originalZoom = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Camera panning
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal, vertical, 0f) * moveSpeed * Time.deltaTime;

        // Edge scrolling
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        if (mouseX < edgeScrollWidth)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (mouseX > screenWidth - edgeScrollWidth)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (mouseY < edgeScrollWidth)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        else if (mouseY > screenHeight - edgeScrollWidth)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }

        // Camera zooming
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= zoom * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        Camera.main.orthographicSize = currentZoom;

        // Reset camera pan and zoom when middle or right mouse button is clicked
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            transform.position = originalPosition;
            Camera.main.orthographicSize = originalZoom;
            currentZoom = originalZoom;
            //CenterOnTilemap();
        }
    }

    public void CenterOnTilemap()
    {
        if (tilemap != null)
        {
            Vector3Int tilemapCenter = Vector3Int.RoundToInt(tilemap.cellBounds.center);
            Vector3 centerPosition = tilemap.CellToWorld(tilemapCenter);
            Debug.Log("Updated aftergen Center position: " + centerPosition);
            transform.position = new Vector3(centerPosition.x, centerPosition.y, transform.position.z);
        }
    }


}