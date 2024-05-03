using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 startPosition;
    public Tile tile;
    public List<GameObject> HeroPrefab;
    public GameObject heroInstance;
    public Transform square; 

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
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

        float topX = square.transform.localPosition.x;
        float topY = square.transform.localPosition.y;
        float w = square.transform.lossyScale.x / 2;
        float h = square.transform.lossyScale.y / 2;


        if (topX - w < transform.localPosition.x && transform.localPosition.x < topX + w && topY + h > transform.localPosition.y && transform.localPosition.y > topY - h)
        {
            //Debug.Log("w " + w + "h " + h + "topX " + topX + "topY " + topY + "posX " + transform.localPosition.x + "posY " + transform.localPosition.y);

            if (tile != null)
            {
                tile.DeleteTile();
                heroInstance = Instantiate(HeroPrefab[(int)Mathf.Log(tile.number, 2) - 1]);
                heroInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                heroInstance.GetComponent<RectTransform>().transform.localPosition = new Vector3(heroInstance.GetComponent<RectTransform>().localPosition.x, heroInstance.GetComponent<RectTransform>().localPosition.y, 1f);
                heroInstance.transform.SetParent(transform.parent);
            }
        }
        else
        {
            tile.GetComponent<RectTransform>().transform.position = startPosition;
        }
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
