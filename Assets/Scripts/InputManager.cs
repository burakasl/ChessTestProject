using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    private GameObject selectedPiece;

    private void Start()
    {
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
    }

    private void MovePiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.transform.position = GetMousePosition();
            selectedPiece.GetComponent<PawnMovementController>().PrepareMove();
        }
    }

    private void ResetSelectedPiece()
    {
        selectedPiece.GetComponent<PawnMovementController>().CancelMove();
        selectedPiece = null;
    }
}
