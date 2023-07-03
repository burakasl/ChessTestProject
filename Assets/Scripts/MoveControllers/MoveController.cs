using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    protected GameObject scripts;
    protected TileManager tileManager;
    protected Piece piece;

    private void Start()
    {
        scripts = GameObject.Find("Scripts");
        tileManager = scripts.GetComponent<TileManager>();
        piece = GetComponent<Piece>();
    }

    public abstract List<GameObject> CheckCaptures();
    public abstract List<GameObject> CheckMoves();
}
