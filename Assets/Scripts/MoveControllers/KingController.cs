using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MoveController
{
    public override List<GameObject> CheckCaptures()
    {
        List<GameObject> possibleTiles = GetThreatenedTiles();
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
        List<GameObject> possibleTiles = GetThreatenedTiles();
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

    public override List<GameObject> GetThreatenedTiles()
    {
        List<GameObject> possibleTiles = new List<GameObject>();

        List<Vector2> vectors = new List<Vector2> {new Vector2(1, 0), new Vector2(0, 1),
            new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 1), new Vector2(1, -1),
            new Vector2(-1, 1), new Vector2(-1, -1)};

        foreach (Vector2 vector in vectors)
        {
            GameObject tile = tileManager.GetTile(vector, piece.tile);

            if (tile != null)
            {
                possibleTiles.Add(tile);
            }
        }

        return possibleTiles;
    }
}
