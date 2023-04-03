using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject highlight;

    /*
    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
    */

    private void OnMouseDown()
    {
        FindAnyObjectByType<GridGenerator>().GetTileAtPosition(this.transform.position);
    }
}
