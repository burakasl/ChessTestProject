using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    private GameObject selectedPiece;
    MoveController moveController;
    private List<GameObject> changedTiles;

    private void Start()
    {
        changedTiles = new List<GameObject>();
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
            ResetSelectedPiece();
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
        selectedPiece = Physics2D.OverlapCircle(GetMousePosition(), 0.1f, LayerMask.GetMask("Piece")).gameObject;

        if (selectedPiece != null)
        {
            ShowPossibleMoves();
        }
    }

    private void MovePiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.transform.position = GetMousePosition();
        }
    }

    private void ResetSelectedPiece()
    {
        HidePossibleMoves();
        selectedPiece = null;
    }

    private void ShowPossibleMoves()
    {
        SetControllerType();

        List<GameObject> possibleMoves = moveController.CheckMoves();
        List<GameObject> possibleCaptures = moveController.CheckCaptures();

        foreach (GameObject tile in possibleMoves)
        {
            tile.GetComponent<SpriteRenderer>().color = Color.green;
            changedTiles.Add(tile);
        }

        foreach (GameObject tile in possibleCaptures)
        {
            tile.GetComponent <SpriteRenderer>().color = Color.red;
            changedTiles.Add(tile);
        }

        
    }

    private void HidePossibleMoves()
    {
        foreach (GameObject tile in changedTiles)
        {
            tile.GetComponent<SpriteRenderer>().color = tile.GetComponent<Tile>().originalColor;
        }
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
