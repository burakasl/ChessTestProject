using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color originalColor;

    public enum Row
    {
        first, second, third, fourth, fifth, sixth, seventh, eighth
    }

    public enum Colummn
    {
        A, B, C, D, E, F, G, H
    }

    public Row row;
    public Colummn colummn;
    public GameObject piece;
    public bool isOccupied, isThreatened, isGoodForMove, isGoodForCapture;

    public bool isGoodForEnPassant;
    public GameObject enPassantPiece;

    private void Start()
    {
        SetPiece();
        SetRow();
        SetColumn();
        originalColor = GetComponent<SpriteRenderer>().color;
        isGoodForMove = false; isGoodForCapture = false; isThreatened = false; isGoodForEnPassant = false;
    }

    public void SetPiece()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Piece")))
        {
            piece = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Piece")).gameObject;
            isOccupied = true;
        }

        else
        {
            piece = null;
            isOccupied = false;
        }
    }

    public void SetEnPassantPiece(GameObject piece)
    {
        enPassantPiece = piece;
    }

    private void SetRow()
    {
        int rowNumber = (int)(transform.position.y + 4.5f);

        switch (rowNumber)
        {
            case 1:
                row = Row.first;
                break;

            case 2:
                row = Row.second;
                break;

            case 3:
                row = Row.third;
                break;

            case 4:
                row = Row.fourth;
                break;

            case 5:
                row = Row.fifth;
                break;

            case 6:
                row = Row.sixth;
                break;

            case 7:
                row = Row.seventh;
                break;

            case 8:
                row = Row.eighth;
                break;
        }
    }

    private void SetColumn()
    {
        int columnNumber = (int)(transform.position.x + 4.5f);

        switch (columnNumber)
        {
            case 1:
                colummn = Colummn.A;
                break;

            case 2:
                colummn = Colummn.B;
                break;

            case 3:
                colummn = Colummn.C;
                break;

            case 4:
                colummn = Colummn.D;
                break;

            case 5:
                colummn = Colummn.E;
                break;

            case 6:
                colummn = Colummn.F;
                break;

            case 7:
                colummn = Colummn.G;
                break;

            case 8:
                colummn = Colummn.H;
                break;
        }
    }
}
