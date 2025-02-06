using UnityEngine;
using System.Collections;

public class MonsterHealthPoints : MonoBehaviour
{
    [SerializeField] private float monsterHealth;

    private SpriteRenderer _spriteRenderer;
    private MonsterMovement _monsterMovement;
    private CurrencyManager _currencyManager;
    private AudioSource _hitAudio;
    private float _arrowDamage = 5;
    private float _crossbowBoltDamage = 10;
    private float _cannonBallDamage = 15;
    private float _iceShardDamage = 7;
    private float _lightningBoldDamage = 9;
    private float _poisonDamage = 3;

    private bool _isCurrentlyDebuff = false;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _monsterMovement = GetComponent<MonsterMovement>();
        _currencyManager = FindAnyObjectByType<CurrencyManager>();
        _hitAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            FlatDamage(_arrowDamage);
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Crossbow Bolt"))
        {
            FlatDamage(_crossbowBoltDamage);
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Cannon Ball"))
        {
            FlatDamage(_cannonBallDamage);
        }
        if(collision.gameObject.CompareTag("Ice Shard"))
        {
            FlatDamage(_iceShardDamage);
            if(!_isCurrentlyDebuff)
            {
                StartCoroutine(SlowDebuff());
            }  
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Lightning Bolt"))
        {
            FlatDamage(_lightningBoldDamage);
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Poison"))
        {
            StartCoroutine(DamageOvertime(_poisonDamage));
            DestroyProjectile(collision.gameObject);
        }
    }
    private void FlatDamage(float damageValue)
    {
        ReduceHealthPoints(damageValue);
    }
    private IEnumerator DamageOvertime(float poisonDamage)
    {
        for(int i = 0; i < 3; i++)
        {
            FlatDamage(poisonDamage);
            yield return new WaitForSeconds(0.3f);
        }
    }
    private IEnumerator SlowDebuff()
    {
        _isCurrentlyDebuff = true;
        float originalSpeed = _monsterMovement.movementSpeed;
        _monsterMovement.movementSpeed = _monsterMovement.movementSpeed / 2;
        yield return new WaitForSeconds(3.5f);
        _monsterMovement.movementSpeed = originalSpeed;
        _isCurrentlyDebuff = false;
    }
    private void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }
    private void ReduceHealthPoints(float damageValue)
    {
        monsterHealth -= damageValue;
        StartCoroutine(HitFlash());
        CheckHealthPoints();
    }
    private void CheckHealthPoints()
    {
        PlayHitSFX();
        if (monsterHealth <=0)
        {
            _currencyManager.GainGold(2);
            Destroy(gameObject);
        }
    }
    private IEnumerator HitFlash()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }
    private void PlayHitSFX()
    {
        _hitAudio.Play();
    }
}

