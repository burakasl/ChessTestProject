using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinateManager : MonoBehaviour
{
    public GameObject GetTile(int rowOffset, int columnOffset, GameObject currentTile)
    {
        Vector2 tilePosition = currentTile.transform.position;
        tilePosition += Vector2.right * columnOffset;
        tilePosition += Vector2.up * rowOffset;

        return Physics2D.OverlapCircle(tilePosition, 0.1f, LayerMask.GetMask("Tile")).gameObject;
    }
}
