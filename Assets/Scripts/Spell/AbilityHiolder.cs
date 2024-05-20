using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHiolder : MonoBehaviour
{
    public float cooldownTime;
    public float activeTime;
    public GameObject abilityPrefab;
    public GameObject spellCastCirclePrefab;
    public GameObject cooldownCounterPrefab;

    private ResourceBarTracker resourceBar;
    private GameObject spellCastCircle;
    private GameObject cooldownCounter;
    private GameObject abilityObj;
    private Ability ability;

    enum AbilityState
    {
        ready,
        cast,
        active, 
        cooldown
    }

    AbilityState state = AbilityState.ready;

    public KeyCode key;

    private void Start()
    {
        resourceBar = GameObject.Find("Track Bar").GetComponent<ResourceBarTracker>();
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                break;
            case AbilityState.cast:

                spellCastCircle.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
                spellCastCircle.transform.SetParent(GameObject.Find("Panel").transform);
                spellCastCircle.transform.SetAsLastSibling();
                resourceBar.renderManaNeed(10);

                if (Input.GetKeyDown(key))
                {
                    abilityObj = Instantiate(abilityPrefab);
                    ability = abilityObj.GetComponent<Ability>();
                    ability.Activate(spellCastCircle);
                    activeTime = ability.activeTime;
                    state = AbilityState.active;
                    spellCastCircle.GetComponent<Image>().enabled = false;

                    resourceBar.ChangeResourceByAmount(-10);
                    resourceBar.renderManaNeed(0);

                    Debug.Log("Abilty active");
                }
                break;
            case AbilityState.active:

                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                } 
                else {
                    state = AbilityState.cooldown;
                    cooldownCounter = Instantiate(cooldownCounterPrefab, transform);
                    cooldownTime = ability.cooldownTime;
                    Destroy(abilityObj);
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                    cooldownCounter.GetComponent<Image>().fillAmount = (float) cooldownTime / ability.cooldownTime;   
                }
                else
                {
                    state = AbilityState.ready;
                    Destroy(cooldownCounter);
                    Destroy(spellCastCircle);
                }
                break;
        }
    }

    public void startCast()
    {
        if( state == AbilityState.ready)
        {
            spellCastCircle = Instantiate(spellCastCirclePrefab, transform);
            state = AbilityState.cast;

            Debug.Log("Start Cast");
        }
    }
}
