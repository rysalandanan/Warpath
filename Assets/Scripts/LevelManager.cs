using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [SerializeField] private Transform startingPosition;
    public Transform[] enemyPath;
    private void Awake()
    {
        main = this;
    }
}
