using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody2D spellRigidBody;
    private GameObject spellAim;
    private GameObject spellObj;
    private Vector3 startPosition;
    private Vector3 lastAimPosition;

    [SerializeField]
    private float speed;

    public int instaniateDistance = 0;
    public int damage = 10;
    public GameObject spellPrefab;
    public GameObject spellAimPrefab;

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
            spellObj = Instantiate(spellPrefab, new Vector3(lastAimPosition.x, lastAimPosition.y + instaniateDistance, 1), spellPrefab.transform.rotation);
            spellObj.GetComponent<Collider2D>().enabled = false;
            spellObj.transform.SetParent(spellAim.transform.root);
            spellObj.transform.SetAsLastSibling();
        } 
        else if(spellObj != null && spellObj.transform.position.y < lastAimPosition.y + 50)
        {
            spellObj.GetComponent<Collider2D>().enabled = true;
        }
        if (spellObj != null && spellObj.transform.position.y < lastAimPosition.y)
        {
            Destroy(spellAim);
            Destroy(spellObj);
        }
    }


}
