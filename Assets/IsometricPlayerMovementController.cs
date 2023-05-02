using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsometricPlayerMovementController : MonoBehaviour
{
    Rigidbody2D rbody;
    public Tilemap tilemap;
    Vector3Int currentCell;
    public float movementSpeed = 5f;
    bool isMoving = false;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        tilemap = GameObject.FindObjectOfType<Tilemap>();
        currentCell = tilemap.WorldToCell(transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving)
        {
            StartCoroutine(MoveToCell(new Vector3Int(0, 1, 0)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
        {
            StartCoroutine(MoveToCell(new Vector3Int(0, -1, 0)));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
        {
            StartCoroutine(MoveToCell(new Vector3Int(-1, 0, 0)));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
        {
            StartCoroutine(MoveToCell(new Vector3Int(1, 0, 0)));
        }
    }

    IEnumerator MoveToCell(Vector3Int direction)
    {
        isMoving = true;
        Vector3Int targetCell = currentCell + direction;
        Vector3 targetPos = tilemap.GetCellCenterWorld(targetCell);
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed * Time.deltaTime);
            yield return null;
        }
        currentCell = targetCell;
        isMoving = false;
    }
}