using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;

    public enum ShopTab
    {
        Purchase = 0, // 구매 탭
        Sale          // 판매 탭
    }

    [Header("탭 버튼")]
    [SerializeField] Button purchaseTabButton;                  // 구매 버튼
    [SerializeField] Button saleTabButton;                      // 판매 버튼

    ShopTab currentTab;                                         // 현재 선택된 탭
    List<GameObject> tabItemList;                               // 탭 아이템 목록

    [Header("상점 내용")]
    [SerializeField] TextMeshProUGUI goldText;                  // 소지한 골드 텍스트

    [SerializeField] GameObject itemElementParenet;             // 아이템 요소 생성 위치 오브젝트 (아이템 요소 오브젝트의 부모 오브젝트)
    RectTransform parenetRectTransform;                         // 부모 오브젝트의 RectTransform 컴포넌트
    GridLayoutGroup parenetGridLayoutGroup;                     // 부모 오브젝트의 GridLayoutGroup 컴포넌트

    GameObject itemElementPrefab;                               // 아이템 요소 프리팹

    [SerializeField] GameObject selectionItemDetailUIObject;    // 선택 아이템 세부 내용 UI 오브젝트
    SelectionItemDetail selectionItemDetailUIComponent;         // 선택 아이템 세부 내용 UI 컴포넌트

    [Header("닫기 버튼")]
    [SerializeField] Button exitButton;                         // 닫기 버튼

    [Header("메세지 창")]
    [SerializeField] GameObject messageBoxUIObject;
    ShopUIMessageBox messageBoxUIComponent;

    bool wait;

    private void Awake()
    {
        if (instance == null) instance = this;

        // 관련 리소스 및 컴포넌트 불러오기
        LoadResourcesAndComponents();

        // 버튼 리스너 세팅
        SetButtonListener();

        // 그리드 레이아웃 그룹 세팅
        SetGridLayoutGroup();
    }

    void Start()
    {
        // 상점 초기화
        //ResetShopUI();
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.B))
        //{
        //    // 아이템 요소 생성
        //    CreateItemElement(null);
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickExitButton();
        }
    }

    // 관련 리소스 및 컴포넌트 불러오기
    void LoadResourcesAndComponents()
    {
        itemElementPrefab = Resources.Load<GameObject>("Prefabs/UI/ShopUI_ShopItemElement");

        tabItemList = new List<GameObject>();

        if (itemElementParenet != null)
        {
            parenetRectTransform = itemElementParenet.GetComponent<RectTransform>();
            parenetGridLayoutGroup = itemElementParenet.GetComponent<GridLayoutGroup>();
        }

        if (selectionItemDetailUIObject != null)
            selectionItemDetailUIComponent = selectionItemDetailUIObject.GetComponent<SelectionItemDetail>();

        if (messageBoxUIObject != null)
            messageBoxUIComponent = messageBoxUIObject.GetComponent<ShopUIMessageBox>();
    }

    // 버튼 리스너 세팅
    void SetButtonListener()
    {
        if (purchaseTabButton != null) purchaseTabButton.onClick.AddListener(ClickPurchaseTabButton);
        if (saleTabButton != null) saleTabButton.onClick.AddListener(ClickSaleTabButton);

        if (exitButton != null) exitButton.onClick.AddListener(ClickExitButton);
    }

    // 그리드 레이아웃 그룹 세팅 (크기 맞춤)
    void SetGridLayoutGroup()
    {
        if (parenetRectTransform != null && parenetGridLayoutGroup != null)
        {
            float parenetWidth = parenetRectTransform.rect.width;
            float marge = parenetGridLayoutGroup.padding.left + parenetGridLayoutGroup.padding.right + parenetGridLayoutGroup.spacing.x;

            float elementWidth = (parenetWidth - marge) / 2.0f;

            parenetGridLayoutGroup.cellSize = new Vector2(elementWidth, 120.0f);
        }
    }

    // 탭 아이템 목록 세팅
    GameObject CreateItemElement(ItemInfo item)
    {
        if (itemElementPrefab != null)
        {
            GameObject newElement = Instantiate(itemElementPrefab, itemElementParenet.transform);
            tabItemList.Add(newElement);

            if (item != null) newElement.GetComponent<ShopItemElement>().SetShopItemElement(item, currentTab);

            return newElement;
        }

        return null;
    }

    // 상점 초기화
    void ResetShopUI()
    {
        goldText.text = GameManager.instance.currentPlayerData.money.ToString("#,##0");

        // 탭 아이템 목록 제거
        for(int i = 0; i < tabItemList.Count; i++)
        {
            Destroy(tabItemList[i].gameObject);
        }

        tabItemList.Clear();

        // 아이템 세부 설명 비활성화
        if (selectionItemDetailUIComponent != null) selectionItemDetailUIComponent.ActiveDetailContentsObject(false);
    }

    public void SetSelectionItemDetailUI(ItemInfo item)
    {
        if (selectionItemDetailUIComponent != null && item != null)
        {
            selectionItemDetailUIComponent.ActiveDetailContentsObject(true);
            selectionItemDetailUIComponent.SetSelectionItemDetail(item);
        }
    }

    public void SetMessageBoxUI(ItemInfo item, int price)
    {
        if(messageBoxUIComponent != null && item != null)
        {
            messageBoxUIComponent.SetMessageBox(currentTab, item, price);
        }
    }

    public void SetShop()
    {
        if (currentTab == ShopTab.Purchase) ClickPurchaseTabButton();
        else ClickSaleTabButton();
    }

    // 다른 행성에서 판매하는 특산물인지 체크
    public bool CheckOtherPlanetSpeciality(ItemInfo item)
    {
        for(int i = 0; i < GameManager.instance.currentPlanet.planetShopItems.Count; i++)
        {
            if(GameManager.instance.currentPlanet.planetShopItems[i].itemID == item.itemID)
            {
                return false;                
            }
        }

        return true;
    }

    // 구매 버튼 클릭
    public void ClickPurchaseTabButton()
    {
        Debug.Log("구매 탭 버튼 클릭");

        currentTab = ShopTab.Purchase;

        // 선택한 탭을 제외한 나머지 탭 활성화
        if (purchaseTabButton != null) purchaseTabButton.interactable = false;
        if (saleTabButton != null) saleTabButton.interactable = true;

        // 상점 초기화
        ResetShopUI();

        // 상점 아이템 목록 세팅
        for (int i = 0; i < GameManager.instance.currentPlanet.planetShopItems.Count; i++)
        {
            CreateItemElement(GameManager.instance.currentPlanet.planetShopItems[i]);
        }
    }

    // 판매 버튼 클릭
    void ClickSaleTabButton()
    {
        Debug.Log("판매 탭 버튼 클릭");

        wait = false;

        currentTab = ShopTab.Sale;

        // 선택한 탭을 제외한 나머지 탭 활성화
        if (purchaseTabButton != null) purchaseTabButton.interactable = true;
        if (saleTabButton != null) saleTabButton.interactable = false;

        //// 상점 초기화
        //ResetShopUI();

        //if (GameManager.instance.currentPlayerData.ownedItem != null)
        //{
        //    // 인벤토리 아이템 목록 세팅
        //    for (int i = 0; i < GameManager.instance.currentPlayerData.ownedItem.Count; i++)
        //    {
        //        CreateItemElement(GameManager.instance.currentPlayerData.ownedItem[i].ChangeItemInfo());
        //    }
        //}

        StartCoroutine(WaitGameManager());
    }

    // 닫기 버튼 클릭
    void ClickExitButton()
    {
        if (!messageBoxUIObject.activeSelf)
        {
            // 상점 초기화
            ResetShopUI();
            GameManager.instance.currentRoom = ROOMSTATE.EXIT;
            gameObject.SetActive(false);
        }
    }

    // 게임 매니저 아이템 삭제 대기
    IEnumerator WaitGameManager()
    {
        if (!wait)
        {
            wait = true;

            yield return new WaitForSeconds(0.1f);

            // 상점 초기화
            ResetShopUI();

            if (GameManager.instance.currentPlayerData.ownedItem != null)
            {
                // 인벤토리 아이템 목록 세팅
                for (int i = 0; i < GameManager.instance.currentPlayerData.ownedItem.Count; i++)
                {
                    CreateItemElement(GameManager.instance.currentPlayerData.ownedItem[i].ChangeItemInfo());
                }
            }

            wait = false;
        }
    }
}
