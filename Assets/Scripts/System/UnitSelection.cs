using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public GameObject selectionBox;
    public GameObject selectionIndicatorPrefab; // Reference to the selection indicator prefab
    public MoveScreen moveScreen;

    public List<GameObject> selectedObjects = new List<GameObject>();

    private Vector2 mouseStartPos;

    private Dictionary<GameObject, GameObject> selectionIndicators = new Dictionary<GameObject, GameObject>(); // To keep track of selection indicators

    void Start()
    {
        selectionBox.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Single selection
        if (!Input.GetKey(KeyCode.LeftShift)) // Clear previous selection if Shift is not held
        {
            ClearSelection();
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);

        if (hitCollider != null)
        {
            GameObject selectedObject = hitCollider.gameObject;
            if (hitCollider.CompareTag("Hero") && hitCollider.GetType() == typeof(BoxCollider2D) && selectedObject.name != "player wall")
            {
                if (!selectedObjects.Contains(selectedObject))
                {
                    selectedObjects.Add(selectedObject);
                    CreateSelectionIndicator(selectedObject);
                    Debug.Log("Single object selected: " + selectedObject.name);
                }
            }
            else if (hitCollider.CompareTag("Enemy") && hitCollider.GetType() == typeof(BoxCollider2D))
            {
                AssignTargetEnemyToHeroes(selectedObject.transform);
                Debug.Log("Enemy selected as target: " + selectedObject.name);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mouseStartPos = Input.mousePosition;
        selectionBox.SetActive(true);
        selectionBox.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateSelectionBox(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selectionBox.SetActive(false);
        SelectUnits();
    }

    void UpdateSelectionBox(Vector2 currentMousePos)
    {
        float width = currentMousePos.x - mouseStartPos.x;
        float height = currentMousePos.y - mouseStartPos.y;

        selectionBox.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.GetComponent<SpriteRenderer>().size = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.GetComponent<RectTransform>().anchoredPosition = mouseStartPos + new Vector2(width / 2, height / 2);

        if (moveScreen.isOpen)
        {
            selectionBox.GetComponent<RectTransform>().anchoredPosition = mouseStartPos + new Vector2(width / 2 - 731.34f, height / 2);
        }
    }

    void SelectUnits()
    {
        if (!Input.GetKey(KeyCode.LeftShift)) // Clear previous selection if Shift is not held
        {
            ClearSelection();
        }

        Vector2 min = selectionBox.GetComponent<RectTransform>().anchoredPosition - (selectionBox.GetComponent<RectTransform>().sizeDelta / 2);
        Vector2 max = selectionBox.GetComponent<RectTransform>().anchoredPosition + (selectionBox.GetComponent<RectTransform>().sizeDelta / 2);

        if (moveScreen.isOpen)
        {
            min.x += 731.34f;
            max.x += 731.34f;
        }

        GameObject[] selectableObjects = GameObject.FindGameObjectsWithTag("Hero");

        foreach (GameObject obj in selectableObjects)
        {
            Vector2 objPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            if (objPos.x >= min.x && objPos.x <= max.x && objPos.y >= min.y && objPos.y <= max.y && obj.name != "player wall")
            {
                selectedObjects.Add(obj);
                CreateSelectionIndicator(obj);
                Debug.Log("Object selected: " + obj.name);
            }
        }
    }

    void ClearSelection()
    {
        foreach (var indicator in selectionIndicators.Values)
        {
            Destroy(indicator);
        }
        selectionIndicators.Clear();
        selectedObjects.Clear();
    }

    void CreateSelectionIndicator(GameObject selectedObject)
    {
        GameObject indicator = Instantiate(selectionIndicatorPrefab, selectedObject.transform);
        indicator.transform.localPosition = new Vector3(-0.1f, -1.1f, 0);
        selectionIndicators[selectedObject] = indicator;
    }

    void AssignTargetEnemyToHeroes(Transform enemy)
    {
        foreach (GameObject hero in selectedObjects)
        {
            HeroMovement heroMovement = hero.GetComponent<HeroMovement>();
            if (heroMovement != null)
            {
                heroMovement.AssignTarget(enemy);
            }
        }
    }
}
