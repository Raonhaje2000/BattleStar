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

    [Header("�� ��ư")]
    [SerializeField] Button gemTabButton;
    [SerializeField] Button goldTabButton;
    [SerializeField] Button itemTabButton;

    PaidShopTab currentTab;
    List<GameObject> tabElementList;

    [Header("���� ����")]
    [SerializeField] TextMeshProUGUI gemText;
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] GameObject elementParenet;

    GameObject gemElementPrefab;
    GameObject goldElementPrefab;
    GameObject packageItemElementPrefab;

    [SerializeField] GameObject selectionItemDetailUIObject;    // ���� ������ ���� ���� UI ������Ʈ
    SelectionItemDetail selectionItemDetailUIComponent;

    [SerializeField] GameObject selectionPackageItemDetailUIObject;
    SelectionPackageItemDetail selectionPackageItemDetailUIComponent;

    [SerializeField] Scrollbar paidShopContentScrollbar;

    [Header("�ݱ� ��ư")]
    [SerializeField] Button exitButton;                         // �ݱ� ��ư

    [Header("�޼��� â")]
    [SerializeField] GameObject messageBoxUIObject;
    PaidShopUIMessageBox messageBoxUIComponent;

    [Header("�Ǹ� ��Ű�� ���")]
    [SerializeField] List<PackageItemData> packageItemList;

    private void Awake()
    {
        if (instance == null) instance = this;

        // ���� ���ҽ� �� ������Ʈ �ҷ�����
        LoadResourcesAndComponents();

        // ��ư ������ ����
        SetButtonListener();
    }

    void Start()
    {
        // ���� �ʱ�ȭ
        //ResetPaidShopUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickExitButton();
        }
    }

    // ���� ���ҽ� �� ������Ʈ �ҷ�����
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

    // ��ư ������ ����
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

    // ���� ���� �ʱ�ȭ
    void ResetPaidShopUI()
    {
        gemText.text = GameManager.instance.currentPlayerData.gem.ToString("#,##0");
        goldText.text = GameManager.instance.currentPlayerData.money.ToString("#,##0");

        paidShopContentScrollbar.value = 1;

        // �� ������ ��� ����
        for (int i = 0; i < tabElementList.Count; i++)
        {
            Destroy(tabElementList[i].gameObject);
        }

        tabElementList.Clear();

        // ������ ���� ���� ��Ȱ��ȭ
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
        Debug.Log("���� �� ��ư Ŭ��");

        currentTab = PaidShopTab.Gem;

        // ������ ���� ������ ������ �� Ȱ��ȭ
        if (gemTabButton != null) gemTabButton.interactable = false;
        if (goldTabButton != null) goldTabButton.interactable = true;
        if (itemTabButton != null) itemTabButton.interactable = true;

        // ���� ���� �ʱ�ȭ
        ResetPaidShopUI();

        // ���� ��� ����
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
        Debug.Log("��� �� ��ư Ŭ��");

        currentTab = PaidShopTab.Gold;

        // ������ ���� ������ ������ �� Ȱ��ȭ
        if (gemTabButton != null) gemTabButton.interactable = true;
        if (goldTabButton != null) goldTabButton.interactable = false;
        if (itemTabButton != null) itemTabButton.interactable = true;

        // ���� ���� �ʱ�ȭ
        ResetPaidShopUI();

        // ���� ��� ����
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
        Debug.Log("������ �� ��ư Ŭ��");

        currentTab = PaidShopTab.Item;

        // ������ ���� ������ ������ �� Ȱ��ȭ
        if (gemTabButton != null) gemTabButton.interactable = true;
        if (goldTabButton != null) goldTabButton.interactable = true;
        if (itemTabButton != null) itemTabButton.interactable = false;

        // ���� ���� �ʱ�ȭ
        ResetPaidShopUI();

        // ������ ��� ����
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
