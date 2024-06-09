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
        Purchase = 0, // ���� ��
        Sale          // �Ǹ� ��
    }

    [Header("�� ��ư")]
    [SerializeField] Button purchaseTabButton;                  // ���� ��ư
    [SerializeField] Button saleTabButton;                      // �Ǹ� ��ư

    ShopTab currentTab;                                         // ���� ���õ� ��
    List<GameObject> tabItemList;                               // �� ������ ���

    [Header("���� ����")]
    [SerializeField] TextMeshProUGUI goldText;                  // ������ ��� �ؽ�Ʈ

    [SerializeField] GameObject itemElementParenet;             // ������ ��� ���� ��ġ ������Ʈ (������ ��� ������Ʈ�� �θ� ������Ʈ)
    RectTransform parenetRectTransform;                         // �θ� ������Ʈ�� RectTransform ������Ʈ
    GridLayoutGroup parenetGridLayoutGroup;                     // �θ� ������Ʈ�� GridLayoutGroup ������Ʈ

    GameObject itemElementPrefab;                               // ������ ��� ������

    [SerializeField] GameObject selectionItemDetailUIObject;    // ���� ������ ���� ���� UI ������Ʈ
    SelectionItemDetail selectionItemDetailUIComponent;         // ���� ������ ���� ���� UI ������Ʈ

    [Header("�ݱ� ��ư")]
    [SerializeField] Button exitButton;                         // �ݱ� ��ư

    [Header("�޼��� â")]
    [SerializeField] GameObject messageBoxUIObject;
    ShopUIMessageBox messageBoxUIComponent;

    bool wait;

    private void Awake()
    {
        if (instance == null) instance = this;

        // ���� ���ҽ� �� ������Ʈ �ҷ�����
        LoadResourcesAndComponents();

        // ��ư ������ ����
        SetButtonListener();

        // �׸��� ���̾ƿ� �׷� ����
        SetGridLayoutGroup();
    }

    void Start()
    {
        // ���� �ʱ�ȭ
        //ResetShopUI();
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.B))
        //{
        //    // ������ ��� ����
        //    CreateItemElement(null);
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickExitButton();
        }
    }

    // ���� ���ҽ� �� ������Ʈ �ҷ�����
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

    // ��ư ������ ����
    void SetButtonListener()
    {
        if (purchaseTabButton != null) purchaseTabButton.onClick.AddListener(ClickPurchaseTabButton);
        if (saleTabButton != null) saleTabButton.onClick.AddListener(ClickSaleTabButton);

        if (exitButton != null) exitButton.onClick.AddListener(ClickExitButton);
    }

    // �׸��� ���̾ƿ� �׷� ���� (ũ�� ����)
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

    // �� ������ ��� ����
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

    // ���� �ʱ�ȭ
    void ResetShopUI()
    {
        goldText.text = GameManager.instance.currentPlayerData.money.ToString("#,##0");

        // �� ������ ��� ����
        for(int i = 0; i < tabItemList.Count; i++)
        {
            Destroy(tabItemList[i].gameObject);
        }

        tabItemList.Clear();

        // ������ ���� ���� ��Ȱ��ȭ
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

    // �ٸ� �༺���� �Ǹ��ϴ� Ư�깰���� üũ
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

    // ���� ��ư Ŭ��
    public void ClickPurchaseTabButton()
    {
        Debug.Log("���� �� ��ư Ŭ��");

        currentTab = ShopTab.Purchase;

        // ������ ���� ������ ������ �� Ȱ��ȭ
        if (purchaseTabButton != null) purchaseTabButton.interactable = false;
        if (saleTabButton != null) saleTabButton.interactable = true;

        // ���� �ʱ�ȭ
        ResetShopUI();

        // ���� ������ ��� ����
        for (int i = 0; i < GameManager.instance.currentPlanet.planetShopItems.Count; i++)
        {
            CreateItemElement(GameManager.instance.currentPlanet.planetShopItems[i]);
        }
    }

    // �Ǹ� ��ư Ŭ��
    void ClickSaleTabButton()
    {
        Debug.Log("�Ǹ� �� ��ư Ŭ��");

        wait = false;

        currentTab = ShopTab.Sale;

        // ������ ���� ������ ������ �� Ȱ��ȭ
        if (purchaseTabButton != null) purchaseTabButton.interactable = true;
        if (saleTabButton != null) saleTabButton.interactable = false;

        //// ���� �ʱ�ȭ
        //ResetShopUI();

        //if (GameManager.instance.currentPlayerData.ownedItem != null)
        //{
        //    // �κ��丮 ������ ��� ����
        //    for (int i = 0; i < GameManager.instance.currentPlayerData.ownedItem.Count; i++)
        //    {
        //        CreateItemElement(GameManager.instance.currentPlayerData.ownedItem[i].ChangeItemInfo());
        //    }
        //}

        StartCoroutine(WaitGameManager());
    }

    // �ݱ� ��ư Ŭ��
    void ClickExitButton()
    {
        if (!messageBoxUIObject.activeSelf)
        {
            // ���� �ʱ�ȭ
            ResetShopUI();
            GameManager.instance.currentRoom = ROOMSTATE.EXIT;
            gameObject.SetActive(false);
        }
    }

    // ���� �Ŵ��� ������ ���� ���
    IEnumerator WaitGameManager()
    {
        if (!wait)
        {
            wait = true;

            yield return new WaitForSeconds(0.1f);

            // ���� �ʱ�ȭ
            ResetShopUI();

            if (GameManager.instance.currentPlayerData.ownedItem != null)
            {
                // �κ��丮 ������ ��� ����
                for (int i = 0; i < GameManager.instance.currentPlayerData.ownedItem.Count; i++)
                {
                    CreateItemElement(GameManager.instance.currentPlayerData.ownedItem[i].ChangeItemInfo());
                }
            }

            wait = false;
        }
    }
}
