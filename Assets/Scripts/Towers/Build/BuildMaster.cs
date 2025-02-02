using UnityEngine;

public class BuildMaster : MonoBehaviour
{
    [SerializeField] private GameObject towerParent;
    private Vector3 _selectedLocation;
    private MonoBehaviour fromScript;

    public void GetBuildLocation(Vector3 selectedLocation, MonoBehaviour caller)
    {
        _selectedLocation = selectedLocation;
        fromScript = caller;
    }
    public void BuildTower(GameObject selectedTower)
    {
        Instantiate(selectedTower, _selectedLocation, Quaternion.identity, towerParent.transform);
        if(fromScript is BuildLocation buildLocation)
        {
            buildLocation.hasBuild = true;
            buildLocation.CloseTowerShop();
        }
    }
}
