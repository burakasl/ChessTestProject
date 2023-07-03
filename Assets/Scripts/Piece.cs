using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PieceType
    {
        Pawn, Knight, Bishop, Rook, Queen, King
    }

    public enum Player
    {
        White, Black
    }

    public PieceType type;
    public Player player;
    public GameObject tile;
    public bool isFirstMove;

    private void Start()
    {
        SetTile();
    }

    private void SetTile()
    {
        tile = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Tile")).gameObject;
    }
}
