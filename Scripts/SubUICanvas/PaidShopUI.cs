using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaidShopUI : MonoBehaviour
{
    public static PaidShopUI instance;

    const int MONEY_SLOT_MAX_COUNT = 10;

    public enum PaidShopTab
    {
        Gem = 0,
        Gold,
        Item
    }

    [Header("탭 버튼")]
    [SerializeField] Button gemTabButton;
    [SerializeField] Button goldTabButton;
    [SerializeField] Button itemTabButton;

    PaidShopTab currentTab;
    List<GameObject> tabElementList;

    [Header("상점 내용")]
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] GameObject elementParenet;

    GameObject gemElementPrefab;
    GameObject goldElementPrefab;
    GameObject packageItemElementPrefab;

    [SerializeField] GameObject selectionItemDetailUIObject;    // 선택 아이템 세부 내용 UI 오브젝트
    SelectionItemDetail selectionItemDetailUIComponent;

    [SerializeField] GameObject selectionPackageItemDetailUIObject;
    SelectionPackageItemDetail selectionPackageItemDetailUIComponent;

    [SerializeField] Scrollbar paidShopContentScrollbar;

    [Header("닫기 버튼")]
    [SerializeField] Button exitButton;                         // 닫기 버튼

    [Header("메세지 창")]
    [SerializeField] GameObject messageBoxUIObject;
    PaidShopUIMessageBox messageBoxUIComponent;

    [Header("판매 패키지 목록")]
    [SerializeField] List<PackageItemData> packageItemList;

    private void Awake()
    {
        if (instance == null) instance = this;

        // 관련 리소스 및 컴포넌트 불러오기
        LoadResourcesAndComponents();

        // 버튼 리스너 세팅
        SetButtonListener();
    }

    void Start()
    {
        // 상점 초기화
        //ResetPaidShopUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickExitButton();
        }
    }

    // 관련 리소스 및 컴포넌트 불러오기
    void LoadResourcesAndComponents()
    {
        gemElementPrefab = Resources.Load<GameObject>("Prefabs/UI/PaidShopUI_PaidShopGemElement");
        goldElementPrefab = Resources.Load<GameObject>("Prefabs/UI/PaidShopUI_PaidShopGoldElement");
        packageItemElementPrefab = Resources.Load<GameObject>("Prefabs/UI/PaidShopUI_PaidShopPackageItemElement");

        packageItemList = new List<PackageItemData>();
        packageItemList.AddRange(Resources.LoadAll<PackageItemData>("ItemData/PackgaeItemData"));

        tabElementList = new List<GameObject>();

        if (selectionItemDetailUIObject != null)
            selectionItemDetailUIComponent = selectionItemDetailUIObject.GetComponent<SelectionItemDetail>();

        if (selectionPackageItemDetailUIObject != null)
            selectionPackageItemDetailUIComponent = selectionPackageItemDetailUIObject.GetComponent<SelectionPackageItemDetail>();

        if (messageBoxUIObject != null)
            messageBoxUIComponent = messageBoxUIObject.GetComponent<PaidShopUIMessageBox>();
    }

    // 버튼 리스너 세팅
    void SetButtonListener()
    {
        if (gemTabButton != null) gemTabButton.onClick.AddListener(ClickGemTabButton);
        if (goldTabButton != null) goldTabButton.onClick.AddListener(ClickGoldTabButton);
        if (itemTabButton != null) itemTabButton.onClick.AddListener(ClickItemTabButton);

        if (exitButton != null) exitButton.onClick.AddListener(ClickExitButton);
    }

    void CreateMoneyElement(int moneyAmount, int moneyPrice, int moneyDiscountPercentage)
    {
        GameObject newElement = null;

        if(currentTab == PaidShopTab.Gem && gemElementPrefab != null)
            newElement = Instantiate(gemElementPrefab, elementParenet.transform);
        else if(currentTab == PaidShopTab.Gold && goldElementPrefab != null)
            newElement = Instantiate(goldElementPrefab, elementParenet.transform);

        if (gemElementPrefab != null || goldElementPrefab != null)
        {
            tabElementList.Add(newElement);
            newElement.GetComponent<PaidShopMoneyElement>().SetPaidShopMoneyElement(currentTab, moneyAmount, moneyPrice, moneyDiscountPercentage);
        }
    }

    void CreatePackageItemElement(PackageItemData packageItem)
    {
        GameObject newElement = Instantiate(packageItemElementPrefab, elementParenet.transform);
        tabElementList.Add(newElement);

        newElement.GetComponent<PaidShopPackageItemElement>().SetPaidShopPackageItemElement(packageItem);
    }

    // 유료 상점 초기화
    void ResetPaidShopUI()
    {
        gemText.text = GameManager.instance.currentPlayerData.gem.ToString("#,##0");
        goldText.text = GameManager.instance.currentPlayerData.money.ToString("#,##0");

        paidShopContentScrollbar.value = 1;

        // 탭 아이템 목록 제거
        for (int i = 0; i < tabElementList.Count; i++)
        {
            Destroy(tabElementList[i].gameObject);
        }

        tabElementList.Clear();

        // 아이템 세부 설명 비활성화
        if (selectionItemDetailUIComponent != null) selectionItemDetailUIComponent.ActiveDetailContentsObject(false);
        if (selectionPackageItemDetailUIComponent != null) selectionPackageItemDetailUIComponent.ActiveDetailContentsObject(false);
    }

    public void SetSelectionItemDetailUI(ItemInfo item)
    {
        if (selectionItemDetailUIComponent != null && item != null)
        {
            selectionItemDetailUIComponent.ActiveDetailContentsObject(true);
            selectionItemDetailUIComponent.SetSelectionItemDetail(item);

            if (selectionPackageItemDetailUIComponent != null) selectionPackageItemDetailUIComponent.ActiveDetailContentsObject(false);
        }
    }

    public void SetSelectionPackageItemDetailUI(PaidShopMoneyElement.MoneyType money, Sprite icon, int amount, int price, int discountPercentage)
    {
        if (selectionPackageItemDetailUIComponent != null)
        {
            selectionPackageItemDetailUIComponent.ActiveDetailContentsObject(true);
            selectionPackageItemDetailUIComponent.SetSelectionItemDetailMoney(money, icon, amount, price, discountPercentage);

            if (selectionItemDetailUIComponent != null) selectionItemDetailUIComponent.ActiveDetailContentsObject(false);
        }
    }

    public void SetSelectionPackageItemDetailUI(PackageItemData packageItem)
    {
        if (selectionPackageItemDetailUIComponent != null && packageItem != null)
        {
            selectionPackageItemDetailUIComponent.ActiveDetailContentsObject(true);
            selectionPackageItemDetailUIComponent.SetSelectionItemDetailItem(packageItem);

            if (selectionItemDetailUIComponent != null) selectionItemDetailUIComponent.ActiveDetailContentsObject(false);
        }
    }

    public void SetMessageBoxUI(Sprite packageIcon, string packageName, int moneyAmount, int price)
    {
        messageBoxUIComponent.SetMessageBox(currentTab, packageIcon, packageName, moneyAmount, price);
    }

    public void SetMessageBoxUI(PackageItemData packageItem)
    {
        messageBoxUIComponent.SetMessageBox(currentTab, packageItem);
    }

    public void SetPaidShop()
    {
        if (currentTab == PaidShopTab.Gem) ClickGemTabButton();
        else if (currentTab == PaidShopTab.Gold) ClickGoldTabButton();
        else ClickItemTabButton();
    }

    public void ClickGemTabButton()
    {
        Debug.Log("보석 탭 버튼 클릭");

        currentTab = PaidShopTab.Gem;

        // 선택한 탭을 제외한 나머지 탭 활성화
        if (gemTabButton != null) gemTabButton.interactable = false;
        if (goldTabButton != null) goldTabButton.interactable = true;
        if (itemTabButton != null) itemTabButton.interactable = true;

        // 유료 상점 초기화
        ResetPaidShopUI();

        // 보석 목록 세팅
        for(int i = 0; i < MONEY_SLOT_MAX_COUNT; i++)
        {
            int moneyAmount = 500 * (i + 1); // 500, 1000, ... , 50,000
            int moneyPrice = moneyAmount * 10;

            int discountPercentage = 2 * i;
            moneyPrice = Mathf.RoundToInt(moneyPrice - moneyPrice * discountPercentage / 100);

            CreateMoneyElement(moneyAmount, moneyPrice, discountPercentage);
        }
    }

    public void ClickGoldTabButton()
    {
        Debug.Log("골드 탭 버튼 클릭");

        currentTab = PaidShopTab.Gold;

        // 선택한 탭을 제외한 나머지 탭 활성화
        if (gemTabButton != null) gemTabButton.interactable = true;
        if (goldTabButton != null) goldTabButton.interactable = false;
        if (itemTabButton != null) itemTabButton.interactable = true;

        // 유료 상점 초기화
        ResetPaidShopUI();

        // 보석 목록 세팅
        for (int i = 0; i < MONEY_SLOT_MAX_COUNT; i++)
        {
            int moneyAmount = 500 * (i + 1); // 500, 1000, ... , 50,000
            int moneyPrice = moneyAmount * 5;

            int discountPercentage = 2 * i;
            moneyPrice = Mathf.RoundToInt(moneyPrice - moneyPrice * discountPercentage / 100);

            CreateMoneyElement(moneyAmount, moneyPrice, discountPercentage);
        }
    }

    public void ClickItemTabButton()
    {
        Debug.Log("아이템 탭 버튼 클릭");

        currentTab = PaidShopTab.Item;

        // 선택한 탭을 제외한 나머지 탭 활성화
        if (gemTabButton != null) gemTabButton.interactable = true;
        if (goldTabButton != null) goldTabButton.interactable = true;
        if (itemTabButton != null) itemTabButton.interactable = false;

        // 유료 상점 초기화
        ResetPaidShopUI();

        // 아이템 목록 세팅
        for (int i = 0; i < packageItemList.Count; i++)
        {
            CreatePackageItemElement(packageItemList[i]);
        }
    }

    void ClickExitButton()
    {
        GameManager.instance.currentRoom = ROOMSTATE.EXIT;
        gameObject.SetActive(false);
    }
}
