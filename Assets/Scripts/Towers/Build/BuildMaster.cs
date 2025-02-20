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
    [SerializeField] private GamePhase gamePhase;

    public float selectedTowerPrice;
    private GameObject _towerSelected;
    private Sprite _spriteSelected;
    private bool _isBuilding = false;
    private Vector3 _mousePosition;
    
   
    private void Update()
    {
        if(IsBuilding())
        {
            ShowSelectedTower(_spriteSelected);

            if (Input.GetMouseButtonDown(0) && IsOverBuildableArea())
            {
                BuildTower(_towerSelected);
            }
            if (Input.GetMouseButtonDown(1))
            {
                _isBuilding = false;
                ActivateDeactivateTowerImage(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.B) && gamePhase.CanBuild())
        { 
            towerShop.SetActive(true);
        }
    }
    public void SelectTower(GameObject selectedTower)
    {
        _towerSelected = selectedTower;
        _isBuilding = true;
    }
    public void SelectSprite(Sprite selectedSprite)
    {
        _spriteSelected = selectedSprite;
        _isBuilding = true;
    }
    private void ShowSelectedTower(Sprite selectedSprite)
    {
        ActivateDeactivateTowerImage(true);
        towerSelected.sprite = selectedSprite;
        var _mousePos = BuildLocation();
        towerSelected.transform.position = new Vector3 (_mousePos.x - 1, _mousePos.y, 0f);
    }
    public void BuildTower(GameObject selectedTower)
    {
        Instantiate(selectedTower, BuildLocation(), Quaternion.identity, towerHolder.transform);
        _isBuilding = false;
        ActivateDeactivateTowerImage(false);
        currencyManager.ReduceGold(selectedTowerPrice);
    }
    private void ActivateDeactivateTowerImage(bool yesNo)
    {
        towerSelected.gameObject.SetActive(yesNo);
    }
    private Vector3 BuildLocation()
    {
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition = new Vector3(mouseWorldPos.x,mouseWorldPos.y, 0f);
        return _mousePosition;
    }
    private bool IsOverBuildableArea()
    {
        Vector3 mouseWorldPos = _mousePosition;
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject.name == "Buildable Area")
        {
            return true;
        }
        return false;
    }
    private bool IsBuilding()
    {
        return _isBuilding;
    }
}
