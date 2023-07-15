using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin
{
    public GameObject pinningPiece, pinnedPiece;
    public List<GameObject> pinTiles;

    public Pin(List<GameObject> _pinTiles, GameObject _pinningPiece, GameObject _pinnedPiece)
    {
        pinningPiece = _pinningPiece;
        pinTiles = _pinTiles;
        pinnedPiece = _pinnedPiece;
    }
}
