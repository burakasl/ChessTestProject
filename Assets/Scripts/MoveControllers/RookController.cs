using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookController : MoveController
{
    public override List<GameObject> CheckCaptures()
    {
        List<GameObject> possibleTiles = GetPossibleTiles();
        List<GameObject> possibleCaptures = new List<GameObject>();

        foreach (GameObject tileObject in possibleTiles)
        {
            if (tileManager.CheckRivalOccupation(tileObject, piece.player))
            {
                possibleCaptures.Add(tileObject);
            }
        }

        return possibleCaptures;
    }

    public override List<GameObject> CheckMoves()
    {
        List<GameObject> possibleTiles = GetPossibleTiles();
        List<GameObject> possibleMoves = new List<GameObject>();

        foreach (GameObject tileObject in possibleTiles)
        {
            if (!tileObject.GetComponent<Tile>().isOccupied)
            {
                possibleMoves.Add(tileObject);
            }
        }

        return possibleMoves;
    }

    private List<GameObject> GetPossibleTiles()
    {
        List<GameObject> possibleTiles = new List<GameObject>();

        List<Vector2> vectors = new List<Vector2> {new Vector2(1, 0), new Vector2(0, 1),
            new Vector2(-1, 0), new Vector2(0, -1)};

        foreach (Vector2 vector in vectors)
        {
            GameObject possibleTile = tileManager.GetTile(vector, piece.tile);

            while (possibleTile != null)
            {
                possibleTiles.Add(possibleTile);

                if (possibleTile.GetComponent<Tile>().isOccupied)
                {
                    break;
                }

                possibleTile = tileManager.GetTile(vector, possibleTile);
            }
        }

        return possibleTiles;
    }
}
