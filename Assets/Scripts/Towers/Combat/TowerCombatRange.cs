using System.Collections.Generic;
using UnityEngine;

public class TowerCombatRange : MonoBehaviour
{
    private TowerCombat _towerCombat;
    private List<GameObject> monstersInRange = new List<GameObject>();

    private void Start()
    {
        _towerCombat = GetComponent<TowerCombat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (!monstersInRange.Contains(collision.gameObject))
            {
                monstersInRange.Add(collision.gameObject);
                _towerCombat.SetTarget(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            monstersInRange.Remove(collision.gameObject);

            if (monstersInRange.Count > 0)
            {
                _towerCombat.SetTarget(monstersInRange[0]);
            }
            else
            {
                _towerCombat.SetTarget(null);
            }
        }
    }
    public bool IsEnemyStillInside(GameObject monster)
    {
        return monstersInRange.Contains(monster);
    }
}
