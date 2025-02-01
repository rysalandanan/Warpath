using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2D;

    [Header("Enemy Attributes")]
    [SerializeField] private float movementSpeed = 2f;

    private Transform _targetPath;
    private int _pathIndex = 0;

    private void Start()
    {
        
        SetPath();
    }
    private void Update()
    {
        if (Vector2.Distance(_targetPath.position, transform.position) <= 0.1f)
        {
            _pathIndex++;
            if(IsAtEndOfPath())
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                SetPath();
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = (_targetPath.position - transform.position).normalized;
        rb2D.velocity = direction * movementSpeed;
    }

    private void SetPath()
    {
        _targetPath = LevelManager.main.enemyPath[_pathIndex];
    }
    private bool IsAtEndOfPath()
    {
        return _pathIndex >= LevelManager.main.enemyPath.Length;
    }
}
