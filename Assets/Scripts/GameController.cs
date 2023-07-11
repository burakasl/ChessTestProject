using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Piece;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pieces, tiles;
    private int moveCount;
    BoardSwapManager boardSwapManager;
    public Piece.Player currentPlayer;
    TileManager tileManager;

    private void Start()
    {
        moveCount = 1;
        currentPlayer = Piece.Player.White;
        boardSwapManager = GetComponent<BoardSwapManager>();
        tileManager = GetComponent<TileManager>();
    }

    public void ApplyMove(GameObject playedPieceObject, GameObject activeTile, List<GameObject> activeTiles)
    {
        Piece playedPiece = playedPieceObject.GetComponent<Piece>();
        GameObject capturedPiece = SetCapturedPiece(activeTile, playedPieceObject);

        CheckForEnPassant(playedPiece, activeTile);
        CapturePiece(capturedPiece);       
        ResetTiles(playedPiece);
        SetFirstMove(playedPieceObject);
        SetCurrentPlayer();
        DisableEnPassantTiles();
        SetActivePieces();
        SetTilesPassive(activeTiles);
        moveCount++;
        boardSwapManager.SwapBoard();
    }

    private GameObject SetCapturedPiece(GameObject activeTile, GameObject playedPiece)
    {
        Piece.Player player = playedPiece.GetComponent<Piece>().player;
        Tile activeTileScript = activeTile.GetComponent<Tile>();

        if (tileManager.CheckRivalOccupation(activeTile, player))
        {
            return activeTile.GetComponent<Tile>().piece;
        }

        if (activeTileScript.isGoodForEnPassant)
        {
            return activeTileScript.enPassantPiece;
        }

        return null;
    }

    private void CapturePiece(GameObject capturedPieceObject)
    {
        if (capturedPieceObject != null)
        {
            Piece capturedPiece = capturedPieceObject.GetComponent<Piece>();
            Tile tile = capturedPiece.tile.GetComponent<Tile>();
            tile.SetPiece();
            Destroy(capturedPieceObject);
        }
    }

    private void CheckForEnPassant(Piece playedPiece, GameObject activeTile)
    {
        if (playedPiece.type == Piece.PieceType.Pawn)
        {
            playedPiece.GetComponent<PawnController>().SetEnPassantTile(activeTile);
        }
    }

    private void ResetTiles(Piece playedPiece)
    {
        Tile oldTile = playedPiece.GetComponent<Piece>().tile.GetComponent<Tile>();
        playedPiece.SetTile();

        Tile tile = playedPiece.tile.GetComponent<Tile>();
        tile.SetPiece();
        oldTile.SetPiece();
    }

    private void DisableEnPassantTiles()
    {
        foreach (Transform tileTransform in tiles.transform)
        {
            Tile tile = tileTransform.gameObject.GetComponent<Tile>();

            if (tile.isGoodForEnPassant &&
                tile.enPassantPiece.GetComponent<Piece>().player == currentPlayer)
            {
                tile.isGoodForEnPassant = false;
                tile.enPassantPiece = null;
            }
        }
    }

    private void SetFirstMove(GameObject pieceObject)
    {
        pieceObject.GetComponent<Piece>().isFirstMove = false;
    }

    private void SetCurrentPlayer()
    {
        switch (moveCount % 2)
        {
            case 0:

                currentPlayer = Piece.Player.White;
                break;

            case 1:

                currentPlayer = Piece.Player.Black;
                break;
        }
    }

    private void SetActivePieces()
    {
        foreach (Transform pieceTransform in pieces.transform)
        {
            Piece piece = pieceTransform.gameObject.GetComponent<Piece>();

            if (piece.player == currentPlayer)
            {
                piece.canMove = true;
            }

            else
            {
                piece.canMove = false;
            }
        }
    }

    public void SetTilesActive(List<GameObject> tiles, bool isCapture)
    {
        foreach (GameObject tileObject in tiles)
        {
            Tile tile = tileObject.GetComponent<Tile>();

            if (isCapture)
            {
                tile.isGoodForCapture = true;
            }

            else
            {
                tile.isGoodForMove = true;
            }
        }
    }

    public void SetTilesPassive(List<GameObject> tiles)
    {
        foreach (GameObject tileObject in tiles)
        {
            Tile tile = tileObject.GetComponent<Tile>();
            tile.isGoodForMove = false; tile.isGoodForCapture = false;
        }
    }
}
