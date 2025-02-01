using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [SerializeField] private Transform startingPosition;
    [Header("Enemie's path point/s")]
    public Transform[] enemyPath;
    private void Awake()
    {
        main = this;
    }
}
