using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var focusedTile = GetFocusedOnTile();

        if (focusedTile.HasValue)
        {
            // position cursor to overlayTile we have gotten from raycast
            GameObject overlayTile = focusedTile.Value.collider.gameObject;
            // position cursor over overlay tile
            transform.position = overlayTile.transform.position;

            // Debug.Log("Overlay mouse: " + overlayTile.transform.position);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;

            // Set the color of the sprite to blue if the z value of the overlayTile's position is -1
            if (overlayTile.transform.position.z == 1) // water
            {
                //spriteRenderer.color = new Color(0f, 0.82f, 0.94f, 1f);
                spriteRenderer.color = Color.blue;

                // Change the sprite renderer color to the hex color FFA500 (orange)
                //spriteRenderer.color = new Color(1f, 0.65f, 0f, 1f);
            }
            else if (overlayTile.transform.position.z == 2) // land
            {
                spriteRenderer.color = Color.yellow;
            }
            else if(overlayTile.transform.position.z == 3) // mountain
            {
                spriteRenderer.color = Color.red;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }

        }
    }

    // draw raycast to find tile cursor is on
    // ? to make it optional since cursor not always on a tile
    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // convert to 2D
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        // get list of all raycasts that are hit (sometimes tiles can be layed on top of eachother, so get list)
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero); // give us straight point

        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }
}
