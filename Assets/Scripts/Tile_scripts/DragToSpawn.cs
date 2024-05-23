using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragToSpawn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 startPosition;
    private ResourceBarTracker resourceBar;
    private Transform spawnArea;

    private TileBoard tileBoard;
    private Renderer placableArea;

    public Tile tile;
    public List<GameObject> HeroPrefab;

    AudioManeger audioManeger;

    private void Awake()
    {
        audioManeger = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManeger>();
    }

    private bool CheckMoreThanOneTile()
    {
        if (tileBoard.tiles.Count > 1) return true;
        return false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        tileBoard = GameObject.Find("Board").GetComponent<TileBoard>();
        resourceBar = GameObject.Find("Track Bar").GetComponent<ResourceBarTracker>();

        if (CheckMoreThanOneTile())
        {
            Debug.Log("Begin drag");
            placableArea = GameObject.Find("HeroSpawnArea").GetComponent<Renderer>();
            placableArea.enabled = true;
            startPosition = transform.position; // Store start position to preserve z value
            transform.SetParent(GameObject.Find("Panel").transform);
            transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CheckMoreThanOneTile())
        {
            Debug.Log("Dragging");
            resourceBar.renderManaNeed((int)Mathf.Log(tile.number, 2));
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPosition.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CheckMoreThanOneTile())
        {
            Debug.Log("End drag");
            resourceBar.renderManaNeed(0);
            placableArea.enabled = false;
            spawnArea = GameObject.Find("HeroSpawnArea").GetComponent<Transform>();
            float topX = spawnArea.transform.localPosition.x - 30;
            float topY = spawnArea.transform.localPosition.y;
            float w = spawnArea.transform.lossyScale.x / 2;
            float h = spawnArea.transform.lossyScale.y / 2;

            bool checkForSufficientMana = resourceBar.getCurrentResource - (int)Mathf.Log(tile.number, 2) >= 0 && resourceBar.getCurrentResource >= (int)Mathf.Log(tile.number, 2);

            if (topX - w < transform.localPosition.x && transform.localPosition.x < topX + w && topY + h > transform.localPosition.y && transform.localPosition.y > topY - h && checkForSufficientMana)
            {
                if (tile != null)
                {
                    tile.DeleteTile();
                    GameObject heroInstance = Instantiate(HeroPrefab[(int)Mathf.Log(tile.number, 2) - 1]);
                    heroInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    heroInstance.GetComponent<RectTransform>().transform.localPosition = new Vector3(heroInstance.GetComponent<RectTransform>().localPosition.x, heroInstance.GetComponent<RectTransform>().localPosition.y, 1f);
                    heroInstance.transform.SetParent(transform.parent);
                    audioManeger.PlaySFX(audioManeger.spawn);

                    resourceBar.ChangeResourceByAmount((int)Mathf.Log(tile.number, 2) * -1);
                }
            }
            else
            {
                tile.GetComponent<RectTransform>().transform.position = startPosition;
            }
        }
    }
}
