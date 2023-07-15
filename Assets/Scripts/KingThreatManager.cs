using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class KingThreatManager : MonoBehaviour
{
    [SerializeField] private GameObject whiteKing, blackKing;
    private TileManager tileManager;

    private void Start()
    {
        tileManager = GetComponent<TileManager>();
    }

    public List<Pin> CheckPins(Piece.Player player)
    {
        GameObject king;

        if (player == Piece.Player.White)
        {
            king = whiteKing;
        }

        else
        {
            king = blackKing;
        }

        List<Pin> pins = new List<Pin>();

        List<Vector2> pinVectors = new List<Vector2> {new Vector2(1, 1), new Vector2(-1, 1),
        new Vector2(-1, -1), new Vector2(1, -1), new Vector2(1, 0), new Vector2(0, 1),
        new Vector2(-1, 0), new Vector2(0, -1)};

        foreach (Vector2 vector in pinVectors)
        {
            GameObject currentTile = king.GetComponent<Piece>().tile;

            Pin pin = null;

            if (GetPin(currentTile, vector, player) != null)
            {
                pin = GetPin(currentTile, vector, player);
            }

            if (pin != null)
            {
                pins.Add(pin);
            }
        }

        return pins;
    }

    private Pin GetPin(GameObject currentTile, Vector2 vector, Piece.Player player)
    {
        Piece.PieceType currentPiece;

        if (vector.x == 0 || vector.y == 0)
        {
            currentPiece = Piece.PieceType.Rook;
        }

        else
        {
            currentPiece = Piece.PieceType.Bishop;
        }

        List<GameObject> pinTiles = new List<GameObject>();
        GameObject pinningPiece = null, pinnedPiece = null;

        currentTile = tileManager.GetTile(vector, currentTile);

        while (currentTile != null)
        {
            Tile tile = currentTile.GetComponent<Tile>();
            GameObject pieceObject = tile.piece;

            if (pieceObject == null)
            {
                pinTiles.Add(currentTile);
                currentTile = tileManager.GetTile(vector, currentTile);
                continue;
            }

            Piece piece = pieceObject.GetComponent<Piece>();

            if (piece.player == player)
            {
                pinnedPiece = pieceObject;
            }

            if (piece.player != player && (piece.type == Piece.PieceType.Queen || piece.type == currentPiece))
            {
                pinningPiece = pieceObject;
                pinTiles.Add(currentTile);
                break;
            }

            pinTiles.Add(currentTile);
            currentTile = tileManager.GetTile(vector, currentTile);
        }

        if (pinningPiece == null)
        {
            return null;
        }

        int pieceCount = 0;

        foreach (GameObject tileObject in pinTiles)
        {
            Tile tile = tileObject.GetComponent<Tile>();

            if (tile.piece != null && tile.piece != pinningPiece)
            {
                pieceCount++;
            }
        }

        Debug.Log(pinnedPiece.name);

        if (pieceCount == 1)
        {
            return new Pin(pinTiles, pinningPiece, pinnedPiece);
        }

        return null;
    }
}
