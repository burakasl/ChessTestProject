using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    private GameObject selectedPiece;
    private Vector2 originalPiecePosition;
    TileManager tileManager;
    GameController gameController;
    MoveController moveController;
    private List<GameObject> activeTiles;

    private void Start()
    {
        tileManager = GetComponent<TileManager>();
        gameController = GetComponent<GameController>();
        activeTiles = new List<GameObject>();
        selectedPiece = null;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CheckPiece();
        }

        if (Input.GetButton("Fire1"))
        {
            MovePiece();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            GameObject activeTile = GetActiveTile();

            if (activeTile != null)
            {
                ApplyMove(activeTile);
            }

            else
            {
                CancelMove();
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
    }

    private void CheckPiece()
    {
        if (Physics2D.OverlapCircle(GetMousePosition(), 0.1f, LayerMask.GetMask("Piece")))
        {
            selectedPiece = Physics2D.OverlapCircle(GetMousePosition(), 0.1f, LayerMask.GetMask("Piece")).gameObject;

            if (selectedPiece.GetComponent<Piece>().canMove)
            {
                originalPiecePosition = selectedPiece.transform.position;
                ShowPossibleMoves();
            }

            else
            {
                selectedPiece = null;
            }
        }
    }

    private void MovePiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.transform.position = GetMousePosition();
        }
    }

    private GameObject GetActiveTile()
    {
        Vector3 mousePosition = GetMousePosition();

        GameObject tileObject = null;

        if (Physics2D.OverlapCircle(mousePosition, 0.1f, LayerMask.GetMask("Tile")))
        {
            tileObject = Physics2D.OverlapCircle(mousePosition, 0.1f, LayerMask.GetMask("Tile")).gameObject;

            if (activeTiles.Contains(tileObject))
            {
                return tileObject;
            }
        }

        return null;
    }

    private void ApplyMove(GameObject activeTile)
    {
        PlacePiece(selectedPiece, activeTile.transform.position);

        Tile oldTile = selectedPiece.GetComponent<Piece>().tile.GetComponent<Tile>();
        oldTile.SetPiece();

        Piece.Player player = selectedPiece.GetComponent<Piece>().player;
        GameObject capturedPiece = null;

        if (tileManager.CheckRivalOccupation(activeTile, player))
        {
            capturedPiece = activeTile.GetComponent<Tile>().piece;
        }

        gameController.ApplyMove(selectedPiece, capturedPiece);
        gameController.SetTilesPassive(activeTiles);
        HidePossibleMoves();
        selectedPiece = null;
    }

    private void CancelMove()
    {
        if (selectedPiece != null)
        {
            PlacePiece(selectedPiece, originalPiecePosition);
        }

        gameController.SetTilesPassive(activeTiles);
        HidePossibleMoves();
        selectedPiece = null;
    }

    private void PlacePiece(GameObject piece, Vector2 position)
    {
        piece.transform.position = position;
    }

    private void ShowPossibleMoves()
    {
        SetControllerType();

        List<GameObject> possibleMoves = moveController.CheckMoves();
        List<GameObject> possibleCaptures = moveController.CheckCaptures();

        foreach (GameObject tile in possibleMoves)
        {
            tile.GetComponent<Tile>().isGoodForMove = true;
            tile.GetComponent<SpriteRenderer>().color = Color.green;
            activeTiles.Add(tile);
        }

        foreach (GameObject tile in possibleCaptures)
        {
            tile.GetComponent<Tile>().isGoodForCapture = true;
            tile.GetComponent <SpriteRenderer>().color = Color.red;
            activeTiles.Add(tile);
        }

        
    }

    private void HidePossibleMoves()
    {
        foreach (GameObject tile in activeTiles)
        {
            gameController.SetTilesPassive(activeTiles);
            tile.GetComponent<SpriteRenderer>().color = tile.GetComponent<Tile>().originalColor;
        }

        activeTiles.Clear();
    }

    private void SetControllerType()
    {
        Piece.PieceType type = selectedPiece.GetComponent<Piece>().type;

        switch (type)
        {
            case Piece.PieceType.Pawn:

                moveController = selectedPiece.GetComponent<PawnController>();
                break;

            case Piece.PieceType.Knight:

                moveController = selectedPiece.GetComponent<KnightController>();
                break;

            case Piece.PieceType.Bishop:

                moveController = selectedPiece.GetComponent<BishopController>();
                break;

            case Piece.PieceType.Rook:

                moveController = selectedPiece.GetComponent<RookController>();
                break;

            case Piece.PieceType.Queen:

                moveController = selectedPiece.GetComponent<QueenController>();
                break;

            case Piece.PieceType.King:

                moveController= selectedPiece.GetComponent<KingController>();
                break;
        }
    }
}
