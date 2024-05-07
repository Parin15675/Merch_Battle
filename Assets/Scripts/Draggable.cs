using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 startPosition;
    private ResourceBarTracker resourceBar;
    private Transform spawnArea;

    public Tile tile;
    public List<GameObject> HeroPrefab;

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

        spawnArea = GameObject.Find("HeroSpawnArea").GetComponent<Transform>();
        float topX = spawnArea.transform.localPosition.x;
        float topY = spawnArea.transform.localPosition.y;
        float w = spawnArea.transform.lossyScale.x / 2;
        float h = spawnArea.transform.lossyScale.y / 2;

        resourceBar = GameObject.Find("Track Bar").GetComponent<ResourceBarTracker>();

        bool checkForSufficientMana = resourceBar.getCurrentResource - (int)Mathf.Log(tile.number, 2) >= 0 && resourceBar.getCurrentResource >= (int)Mathf.Log(tile.number, 2);

        if (topX - w < transform.localPosition.x && transform.localPosition.x < topX + w && topY + h > transform.localPosition.y && transform.localPosition.y > topY - h && checkForSufficientMana)
        {
            //Debug.Log("w " + w + "h " + h + "topX " + topX + "topY " + topY + "posX " + transform.localPosition.x + "posY " + transform.localPosition.y);
            
            if (tile != null)
            {
                tile.DeleteTile();
                GameObject heroInstance = Instantiate(HeroPrefab[(int)Mathf.Log(tile.number, 2) - 1]);
                heroInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                heroInstance.GetComponent<RectTransform>().transform.localPosition = new Vector3(heroInstance.GetComponent<RectTransform>().localPosition.x, heroInstance.GetComponent<RectTransform>().localPosition.y, 1f);
                heroInstance.transform.SetParent(transform.parent);

                resourceBar.ChangeResourceByAmount((int)Mathf.Log(tile.number, 2) * -1);
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
