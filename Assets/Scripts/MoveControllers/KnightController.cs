using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MoveController
{
    public override List<GameObject> CheckCaptures()
    {
        List<GameObject> possibleMoves = CheckMoves();
        List<GameObject> possibleCaptures = new List<GameObject>();

        foreach (GameObject tileObject in possibleMoves)
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
        List<GameObject> possibleMoves = new List<GameObject>();

        List<Vector2> possibleTiles = new List<Vector2> { new Vector2(1, 2), new Vector2(2, 1),
        new Vector2(1, -2), new Vector2(2, -1), new Vector2(-1, 2), new Vector2(-2, 1),
        new Vector2(-1, -2), new Vector2(-2, -1)};

        foreach (Vector2 tileVector in possibleTiles)
        {
            GameObject tile = tileManager.GetTile(tileVector, piece.tile);

            if (tile == null)
            {
                continue;
            }

            else if (!tileManager.CheckOwnOccupation(tile, piece.player))
            {
                possibleMoves.Add(tile);
            }
        }

        return possibleMoves;
    }
}
