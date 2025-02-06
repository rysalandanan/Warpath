using UnityEngine;
using System.Collections;

public class TowerCombat : MonoBehaviour
{
    [Header("Tower's Combat Attributes")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectSpeed;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float angleOffset = 0f;

    [SerializeField] private Transform projectileSpawn;
    private TowerCombatRange _towerCombatRange;
    private bool _isInRange = false;
    private bool _canFire = true;
    private GameObject _monsterObject;
    private AudioSource _attackAudio;
    private void Start()
    {
        _towerCombatRange =GetComponent<TowerCombatRange>();
        _attackAudio = GetComponent<AudioSource>();
    }
    public void SetTarget(GameObject targetMonster)
    {
        if(_towerCombatRange.IsEnemyStillInside(targetMonster))
        {
            _isInRange = true;
            _monsterObject = targetMonster;
        }
        else
        {
            _isInRange = false;
            _monsterObject = targetMonster;
        }
    }
    private void Update()
    {
        if(_isInRange && _canFire)
        {
            StartCoroutine(AttackMonster(_monsterObject));
        }
    }
    private IEnumerator AttackMonster(GameObject targetMonster)
    {
        if (targetMonster == null) yield break;

        _canFire = false;
        GameObject spawnedProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);

        Vector2 direction = (targetMonster.transform.position - projectileSpawn.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        spawnedProjectile.transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);

        Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectSpeed;
        }
        PlayAttackSFX();
        yield return new WaitForSeconds(rateOfFire);
        _canFire = true;
    }
    private void PlayAttackSFX()
    {
        _attackAudio.Play();
        Debug.Log("Play sfx");
    }
}
