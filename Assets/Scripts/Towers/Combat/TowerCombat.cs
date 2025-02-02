using UnityEngine;
using System.Collections;

public class TowerCombat : MonoBehaviour
{
    [Header("Tower's Combat Attributes")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectSpeed;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float angleOffset = 0f;
    private TowerCombatRange _towerCombatRange;
    private bool isInRange = false;
    private bool canFire = true;
    private GameObject monsterObject;
    private void Start()
    {
        _towerCombatRange =GetComponent<TowerCombatRange>();
    }
    public void SetTarget(GameObject targetMonster)
    {
        if(_towerCombatRange.IsEnemyStillInside(targetMonster))
        {
            isInRange = true;
            monsterObject = targetMonster;
        }
        else
        {
            isInRange = false;
            monsterObject = targetMonster;
        }
    }
    private void Update()
    {
        if(isInRange && canFire)
        {
            StartCoroutine(AttackMonster(monsterObject));
        }
    }
    private IEnumerator AttackMonster(GameObject targetMonster)
    {
        if (targetMonster == null) yield break; // Stop if the target is gone

        canFire = false; // Prevent immediate next attack
        GameObject spawnedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        Vector2 direction = (targetMonster.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the angleOffset to adjust the projectile's facing direction
        spawnedProjectile.transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);

        Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectSpeed;
        }

        yield return new WaitForSeconds(rateOfFire); // Fire rate delay
        canFire = true; // Allow firing again
    }
}
