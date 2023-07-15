using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameObject selectedPiece;
    private Vector2 originalPiecePosition;
    GameController gameController;
    TileManager tileManager;
    KingThreatManager kingThreatManager;
    private List<GameObject> activeTiles;

    private void Start()
    {
        tileManager = GetComponent<TileManager>();
        gameController = GetComponent<GameController>();
        kingThreatManager = GetComponent<KingThreatManager>();
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
                SetActiveTiles();
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

    private void PlacePiece(GameObject piece, Vector2 position)
    {
        piece.transform.position = position;
    }

    private void ApplyMove(GameObject activeTile)
    {
        PlacePiece(selectedPiece, activeTile.transform.position);
        HidePossibleMoves();
        gameController.ApplyMove(selectedPiece, activeTile, activeTiles);
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

    

    private void ShowPossibleMoves()
    {
        selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 2;

        foreach (GameObject tileObject in activeTiles)
        {
            Tile tile = tileObject.GetComponent<Tile>();

            if (tile.isGoodForMove)
            {
                tile.GetComponent<SpriteRenderer>().color = Color.green;
            }

            if (tile.isGoodForCapture)
            {
                tile.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void SetActiveTiles()
    {
        MoveController moveController = selectedPiece.GetComponent<MoveController>();
        Piece piece = selectedPiece.GetComponent<Piece>();

        List<GameObject> possibleMoves = moveController.CheckMoves();
        List<GameObject> possibleCaptures = moveController.CheckCaptures();
        List<GameObject> threatenedTiles = tileManager.GetThreatenedTiles(gameController.currentPlayer);
        List<GameObject> tempTiles = new List<GameObject>();

        foreach (GameObject tileObject in possibleMoves)
        {
            Tile tile = tileObject.GetComponent<Tile>();

            if (piece.type == Piece.PieceType.King && threatenedTiles.Contains(tileObject))
            {
                continue;
            }

            tile.isGoodForMove = true;
            tempTiles.Add(tileObject);
        }

        foreach (GameObject tileObject in possibleCaptures)
        {
            Tile tile = tileObject.GetComponent<Tile>();

            if (piece.type == Piece.PieceType.King && threatenedTiles.Contains(tileObject))
            {
                continue;
            }

            tile.isGoodForCapture = true;
            tempTiles.Add(tileObject);
        }

        List<Pin> pins = kingThreatManager.CheckPins(piece.player);
        Pin selectedPin = null;

        foreach (Pin pin in pins)
        {
            if (pin.pinnedPiece == selectedPiece)
            {
                selectedPin = pin;
                break;
            }
        }

        if (selectedPin == null)
        {
            activeTiles = tempTiles;
        }

        else
        {
            List<GameObject> pinTiles = selectedPin.pinTiles;

            foreach (GameObject tile in tempTiles)
            {
                if (pinTiles.Contains(tile))
                {
                    activeTiles.Add(tile);
                }
            }
        }
    }

    private void HidePossibleMoves()
    {
        if (selectedPiece != null)
        {
            selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        foreach (GameObject tile in activeTiles)
        {
            gameController.SetTilesPassive(activeTiles);
            tile.GetComponent<SpriteRenderer>().color = tile.GetComponent<Tile>().originalColor;
        }

        activeTiles.Clear();
    }

}
