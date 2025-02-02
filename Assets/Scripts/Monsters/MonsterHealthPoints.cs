using UnityEngine;

public class MonsterHealthPoints : MonoBehaviour
{
    [SerializeField] private float monsterHealth;

    private float _arrowDamage = 5;
    private float _crossbowBoltDamage = 10;
    private float _cannonBallDamage = 15;
    private float _iceShard = 7;
    private float _lightningBoldDamage = 9;
    private float _poisonDamage = 3;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            ReduceHealthPoints(_arrowDamage);
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Crossbow Bolt"))
        {
            ReduceHealthPoints(_crossbowBoltDamage);
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Cannon Ball"))
        {
            ReduceHealthPoints(_cannonBallDamage);
        }
        if(collision.gameObject.CompareTag("Ice Shard"))
        {
            ReduceHealthPoints(_iceShard);
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Lightning Bolt"))
        {
            ReduceHealthPoints(_lightningBoldDamage);
            DestroyProjectile(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Poison"))
        {
            ReduceHealthPoints(_poisonDamage);
            DestroyProjectile(collision.gameObject);
        }
    }
    private void ReduceHealthPoints(float damageValue)
    {
        monsterHealth -= damageValue;
        CheckHealthPoints();
    }
    private void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }
    private void CheckHealthPoints()
    {
        if(monsterHealth <=0)
        {
            Destroy(gameObject);
        }
    }

}

