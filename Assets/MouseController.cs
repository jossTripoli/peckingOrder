using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var focusedTile = GetFocusedOnTile();
        
        if(focusedTile.HasValue)
        {
            // position cursor to overlayTile we have gotten from raycast
            GameObject overlayTile = focusedTile.Value.collider.gameObject;
            // position cursor over overlay tile
            transform.position = overlayTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
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

        if(hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }

        return null;
    }
}
