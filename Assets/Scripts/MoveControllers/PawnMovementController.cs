using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovementController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject GameController;
    TileCoordinateManager tileCoordinateManager;
    PieceManager pieceManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileCoordinateManager = GameController.GetComponent<TileCoordinateManager>();
        pieceManager = GetComponent<PieceManager>();
    }

    public void PrepareMove()
    {
        spriteRenderer.sortingOrder = 2;
    }

    public void CancelMove()
    {
        spriteRenderer.sortingOrder = 1;
    }

    private void ShowPossibleMoves()
    {

    }

    private bool[] CheckCaptures()
    {
        GameObject tileToLeft = tileCoordinateManager.GetTile(-1, 1, pieceManager.tile);
        GameObject tileToRight = tileCoordinateManager.GetTile(1, 1, pieceManager.tile);

        bool[] captureBools = new bool[2];
        captureBools[0] = false; captureBools[1] = false;

        if (tileToLeft != null)
        {
            captureBools[0] = true;
        }

        if (tileToRight != null)
        {
            captureBools[1] = true;
        }

        return captureBools;
    }

    private bool[] CheckMoves()
    {
        GameObject nearTile = tileCoordinateManager.GetTile(0, 1, pieceManager.tile);
        GameObject farTile = tileCoordinateManager.GetTile(0, 2, pieceManager.tile);
    }
}
