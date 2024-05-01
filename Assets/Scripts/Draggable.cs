using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentAfterDrag;
    private Vector3 startPosition;
    public GameManeger gameManager; // Reference to the GameManager

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        startPosition = transform.position; // Store start position to preserve z value
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPosition.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z);

        gameManager.NewGame();

    }

    private bool IsOverGameBoard(PointerEventData eventData)
    {
        // Assuming you have some way to detect if over the game board
        // You might need a collider or a specific tag to identify the game board area
        if (eventData.pointerCurrentRaycast.gameObject != null &&
            eventData.pointerCurrentRaycast.gameObject.CompareTag("GameBoard"))
        {
            return true;
        }
        return false;
    }
}
