using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardSwapManager : MonoBehaviour
{
    [SerializeField] private GameObject pieces;
    [SerializeField] private GameObject tiles;
    Dictionary<GameObject, Vector2> boardPositions;

    private void Start()
    {
        boardPositions = new Dictionary<GameObject, Vector2>();
    }

    public void SwapBoard()
    {
        MergeBoardObjects();

        foreach (KeyValuePair<GameObject, Vector2> boardObjectPosition in boardPositions)
        {
            boardObjectPosition.Key.transform.position = boardObjectPosition.Value * -1;
        }

        boardPositions.Clear();
    }

    private void MergeBoardObjects()
    {
        foreach (Transform pieceTransform in pieces.transform)
        {
            boardPositions.Add(pieceTransform.gameObject, pieceTransform.position);
        }

        foreach (Transform tileTransform in tiles.transform)
        {
            boardPositions.Add(tileTransform.gameObject, tileTransform.position);
        }
    }
}
