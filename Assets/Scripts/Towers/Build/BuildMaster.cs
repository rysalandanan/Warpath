using UnityEngine;
using UnityEngine.UI;

public class BuildMaster : MonoBehaviour
{
    [Tooltip("The object that will hold the built Towers")]
    [SerializeField] private GameObject towerHolder;

    [Tooltip("Show which Tower is currently Selected")]
    [SerializeField] private Image towerSelected;

    [Tooltip("Drag the Tower Shop Panel here:")]
    [SerializeField] private GameObject towerShop;

    [SerializeField] private CurrencyManager currencyManager;

    public float selectedTowerPrice;
    private Vector3 _selectedLocation;
    private GameObject _towerSelected;
    private Sprite _spriteSelected;
    private bool isBuilding = false;
    private Vector3 _mousePosition;
    
   
    private void Update()
    {
        if(isBuilding)
        {
            ShowSelectedTower(_spriteSelected);
            if (Input.GetMouseButtonDown(0))
            {
                _selectedLocation = GetMousePosition();
                BuildTower(_towerSelected);
            }
            if (Input.GetMouseButtonDown(1))
            {
                isBuilding = false;
                ActivateDeactivateTowerImage(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            towerShop.SetActive(true);
        }
    }
    public void SelectTower(GameObject selectedTower)
    {
        _towerSelected = selectedTower;
        isBuilding = true;
    }
    public void SelectSprite(Sprite selectedSprite)
    {
        _spriteSelected = selectedSprite;
        isBuilding = true;
    }
    private void ShowSelectedTower(Sprite selectedSprite)
    {
        ActivateDeactivateTowerImage(true);
        towerSelected.sprite = selectedSprite;
        var _mousePos = GetMousePosition();
        towerSelected.transform.position = new Vector3 (_mousePos.x - 1, _mousePos.y, 0f);
    }
    public void BuildTower(GameObject selectedTower)
    {
        Instantiate(selectedTower, _selectedLocation, Quaternion.identity, towerHolder.transform);
        isBuilding = false;
        ActivateDeactivateTowerImage(false);
        currencyManager.ReduceGold(selectedTowerPrice);
    }
    private void ActivateDeactivateTowerImage(bool yesNo)
    {
        towerSelected.gameObject.SetActive(yesNo);
        Debug.Log(yesNo);
    }
    private Vector3 GetMousePosition()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition = new Vector3(mouseWorldPos.x,mouseWorldPos.y, 0f);
        return _mousePosition;
    }
}
