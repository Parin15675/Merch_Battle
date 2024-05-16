using System.Collections;
using UnityEngine;

public class RangeHeroAttack : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private HeroMovement heroMovement;
    private GameObject arrow;

    public GameObject arrowPrefab;
    public Transform launchOffset;
    public int attackDamage;
    public bool isAttacking = false;
    public int point = 1;
    public Animator animator;
    public float attackInterval = 0.01f; // Time between attacks

    private void Awake()
    {
        baseCharacter = GetComponent<BaseCharacter>();
        heroMovement = GetComponent<HeroMovement>();
        attackDamage = baseCharacter.attack;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetType() == typeof(BoxCollider2D))
        {
            if (target.gameObject.CompareTag("Enemy"))
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackRoutine(target));
                }
            }
        }
    }

    private IEnumerator AttackRoutine(Collider2D target)
    {
        isAttacking = true;
        while (target != null && target.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("Attacking", true);
            arrow = Instantiate(arrowPrefab, launchOffset.position, Quaternion.identity);
            arrow.GetComponent<RectTransform>().transform.localPosition = new Vector3(arrow.GetComponent<RectTransform>().localPosition.x, arrow.GetComponent<RectTransform>().localPosition.y, 1f);
            arrow.transform.SetParent(transform);
            heroMovement.StopMovement();

            // Wait for the attack interval before the next attack
            yield return new WaitForSeconds(attackInterval);

            // Reset attack state
            animator.SetBool("Attacking", false);
        }
        if (arrow != null)
        {
            animator.SetBool("Attacking", false);
        }

        isAttacking = false;
        heroMovement.WalkForward();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " Another hero left, resuming.");
        heroMovement.WalkForward();
    }
}
