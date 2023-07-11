using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject pieces;

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

    public List<GameObject> GetThreatenedTiles(Piece.Player player)
    {
        List<GameObject> threatenedTiles = new List<GameObject>();

        foreach (Transform pieceTransform in pieces.transform)
        {
            Piece piece = pieceTransform.gameObject.GetComponent<Piece>();

            if (piece.player != player)
            {
                MoveController moveController = pieceTransform.gameObject.GetComponent<MoveController>();

                List<GameObject> tempList = moveController.GetThreatenedTiles();

                foreach (GameObject tile in tempList)
                {
                    threatenedTiles.Add(tile);
                }
            }
        }

        return threatenedTiles;
    }
}
