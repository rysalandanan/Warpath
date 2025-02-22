using TMPro;
using UnityEngine;

public class GamePhase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStatus;
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private TextMeshProUGUI dayCount;
    private bool _isBuildPhase;
    private int _count;

    private void Start()
    {
        IsBuildPhase(true);
        SetGameStatus();
    }
    public void IsBuildPhase(bool yesNo)
    {
        _isBuildPhase = yesNo;
        SetGameStatus();
    }
    public bool CanBuild()
    {
        return _isBuildPhase;
    }
    
    private void SetGameStatus()
    {
        if(_isBuildPhase)
        {
           gameStatus.text = "Build Phase".ToString();
        }
        else
        {
            gameStatus.text ="Combat Phase".ToString();
        }
    }
    private void Update()
    {
        if(_isBuildPhase && Input.GetKeyDown(KeyCode.Space))
        {
            _isBuildPhase = false;
            SetGameStatus();
            monsterSpawner.StartWave();
            SetDayCount();
        }
    }
    private void SetDayCount()
    {
        _count++;
        dayCount.text = "Day: " + _count.ToString();
    }
}
