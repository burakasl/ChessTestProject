using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MoveController
{
    public override List<GameObject> CheckCaptures()
    {
        List<GameObject> possibleTiles = new List<GameObject>();

        GameObject topLeftTile = tileManager.GetTile(new Vector2(-1, 1), piece.tile);

        if (topLeftTile != null &&
            tileManager.CheckRivalOccupation(topLeftTile, piece.player))
        {
            possibleTiles.Add(topLeftTile);
        }

        GameObject topRightTile = tileManager.GetTile(new Vector2(1, 1), piece.tile);

        if (topRightTile != null &&
            tileManager.CheckRivalOccupation(topRightTile, piece.player))
        {
            possibleTiles.Add(topRightTile);
        }

        return possibleTiles;
    }

    public override List<GameObject> CheckMoves()
    {
        List<GameObject> possibleTiles = new List<GameObject>();

        GameObject nearTile = tileManager.GetTile(new Vector2(0, 1), piece.tile);

        if (nearTile != null &&
            !nearTile.GetComponent<Tile>().isOccupied)
        {
            possibleTiles.Add(nearTile);
        }

        GameObject farTile;

        if (piece.isFirstMove) 
        {
            farTile = tileManager.GetTile(new Vector2(0, 2), piece.tile);
        }

        else
        {
            farTile = null;
        }

        if (farTile != null &&
            (!farTile.GetComponent<Tile>().isOccupied) && possibleTiles.Count != 0)
        {
            possibleTiles.Add(farTile);
        }

        return possibleTiles;
    }
}
