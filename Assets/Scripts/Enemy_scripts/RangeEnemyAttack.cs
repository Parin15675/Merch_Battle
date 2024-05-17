using System.Collections;
using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private EnemyMovement enemyMovement;
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
        enemyMovement = GetComponent<EnemyMovement>();
        attackDamage = baseCharacter.attack;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetType() == typeof(BoxCollider2D))
        {
            if (target.gameObject.CompareTag("Hero"))
            {
                if (!isAttacking)
                {
                    enemyMovement.StopMovement();
                    StartCoroutine(AttackRoutine(target));
                }
            }
        }
    }

    private IEnumerator AttackRoutine(Collider2D target)
    {
        isAttacking = true;
        while (target != null && target.gameObject.CompareTag("Hero"))
        {
            animator.SetBool("Attacking", true);
            arrow = Instantiate(arrowPrefab, launchOffset.position, Quaternion.identity);
            arrow.GetComponent<RectTransform>().transform.localPosition = new Vector3(arrow.GetComponent<RectTransform>().localPosition.x, arrow.GetComponent<RectTransform>().localPosition.y, 1f);
            arrow.transform.SetParent(transform);
            enemyMovement.StopMovement();

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
        enemyMovement.WalkForward();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " Another hero left, resuming.");
        enemyMovement.WalkForward();
    }
}
