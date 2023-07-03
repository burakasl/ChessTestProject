using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject GetTile(Vector2 offset, GameObject currentTile)
    {
        Vector2 tilePosition = currentTile.transform.position;
        tilePosition += Vector2.right * offset.x;
        tilePosition += Vector2.up * offset.y;

        if (Physics2D.OverlapCircle(tilePosition, 0.1f, LayerMask.GetMask("Tile")))
        {
            return Physics2D.OverlapCircle(tilePosition, 0.1f, LayerMask.GetMask("Tile")).gameObject;
        }

        return null;
    }

    public bool CheckRivalOccupation(GameObject tileObject, Piece.Player player)
    {
        Tile tile = tileObject.GetComponent<Tile>();

        if (tile.isOccupied)
        {
            Piece piece = tile.piece.GetComponent<Piece>();

            if (piece.player != player)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckOwnOccupation(GameObject tileObject, Piece.Player player)
    {
        Tile tile = tileObject.GetComponent<Tile>();

        if (tile.isOccupied)
        {
            Piece piece = tile.piece.GetComponent<Piece>();

            if (piece.player == player)
            {
                return true;
            }
        }

        return false;
    }
}
