using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pieces;
    private int moveCount;
    BoardSwapManager boardSwapManager;
    Piece.Player currentPlayer;

    private void Start()
    {
        moveCount = 1;
        currentPlayer = Piece.Player.White;
        boardSwapManager = GetComponent<BoardSwapManager>();
    }

    public void ApplyMove(GameObject playedPieceObject, GameObject capturedPiece)
    {

        if (capturedPiece != null)
        {
            Destroy(capturedPiece);
        }

        Piece playedPiece = playedPieceObject.GetComponent<Piece>();
        playedPiece.SetTile();

        Tile tile = playedPiece.tile.GetComponent<Tile>();
        Debug.Log(tile.name);
        tile.SetPiece();

        SetFirstMove(playedPieceObject);
        SetCurrentPlayer();
        SetActivePieces();
        moveCount++;
        boardSwapManager.SwapBoard();
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
