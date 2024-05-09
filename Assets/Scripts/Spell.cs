using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody2D spellRigidBody;
    private Transform target;
    private GameObject spellAim;
    private GameObject spellObj;
    private Vector3 startPosition;
    private Vector3 lastAimPosition;

    [SerializeField]
    private float speed;

    public GameObject spellPrefab;
    public GameObject spellAimPrefab;

    public void Start()
    {
        
    }

    public void cast()
    {
        if (spellAim == null) 
        {
            spellAim = Instantiate(spellAimPrefab, transform);
            startPosition = spellAim.transform.position;
        }
    }

    public void Update()
    {
        
        if (!Input.GetMouseButtonDown(0) && spellAim != null && spellObj == null)
        {
            spellAim.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPosition.z);
            lastAimPosition = spellAim.transform.position;
        } 
        else if (Input.GetMouseButtonDown(0) && spellAim != null) 
        {
            spellObj = Instantiate(spellPrefab, new Vector3(lastAimPosition.x, lastAimPosition.y + 1000, 1), spellPrefab.transform.rotation);
            spellObj.transform.SetParent(spellAim.transform.root);
            spellObj.transform.SetAsLastSibling();
        } 
        else if(spellObj != null && spellObj.transform.position.y < lastAimPosition.y)
        {
            Destroy(spellAim);
            Destroy(spellObj);
        }
    }
}
