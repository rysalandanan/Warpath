using UnityEngine;

public class BuildLocation : MonoBehaviour
{
    [SerializeField] private GameObject towerShopPanel;
    private BuildMaster _buildMaster;
    private Vector3 _selectedLocation;
    public bool hasBuild = false;
    private void Start()
    {
        _buildMaster = FindAnyObjectByType<BuildMaster>();
    }
    private void OnMouseDown()
    {
        SendBuildLocation();
    }
    private void SendBuildLocation()
    {
        if (!hasBuild && !towerShopPanel.activeInHierarchy)
        {
            _selectedLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _buildMaster.GetBuildLocation(_selectedLocation, this);
            OpenTowerShop();
        }
    }
    private void OpenTowerShop()
    {
        towerShopPanel.SetActive(true);
    }
    public void CloseTowerShop()
    {
        towerShopPanel.SetActive(false);
    }
}
