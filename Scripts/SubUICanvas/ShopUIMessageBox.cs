using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIMessageBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;

    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI weightText;
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] TMP_InputField inputField;

    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    ShopUI.ShopTab currentTab;
    ItemInfo selectedItem;
    int itemPrice;

    float remainingWeight;
    float weight;
    int gold;

    Color nomal;
    Color error;

    CanvasGroup yesButtonCanvasGroup;

    int selectedCount;

    private void Awake()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(ChangeValue);
            inputField.onEndEdit.AddListener(EndEdit);
        }

        if (yesButton != null) yesButton.onClick.AddListener(ClickYesButton);
        if (noButton != null) noButton.onClick.AddListener(ClickNoButton);

        if (yesButton != null) yesButtonCanvasGroup = yesButton.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        nomal = Color.white;
        error = new Color(200 / 255.0f, 0, 0);

        selectedCount = 1;

        gameObject.SetActive(false);
    }

    public void SetMessageBox(ShopUI.ShopTab tab, ItemInfo item, int price)
    {
        currentTab = tab;
        selectedItem = item;
        itemPrice = price;

        remainingWeight = GameManager.instance.currentPlayerData.currentShip.carryWeight - GameManager.instance.CalcTotalWeight();
        weight = selectedItem.weight;

        if (selectedItem != null)
        {
            if (messageText != null) messageText.text = ((currentTab == ShopUI.ShopTab.Purchase) ? "구매" : "판매") + "하시겠습니까?";

            if (itemIcon != null) itemIcon.sprite = selectedItem.itemImage;
            if (weightText != null) weightText.text = selectedItem.weight.ToString();
            if (goldText != null) goldText.text = itemPrice.ToString();

            if (inputField != null)
            {
                inputField.text = "1";
                SetMessageBoxByInputField(1);
            }

            gameObject.SetActive(true);
        }
    }

    void ChangeValue(string text)
    {
        int count = 1;

        if (int.TryParse(text, out count))
        {
            if (currentTab == ShopUI.ShopTab.Purchase)
            {
                count = Mathf.Clamp(count, 1, 999);
            }
            else
            {
                count = Mathf.Clamp(count, 1, GameManager.instance.CountItem(selectedItem));
            }

            inputField.text = count.ToString();
        }
        else
        {
            count = 0;
            inputField.text = "";
        }

        SetMessageBoxByInputField(count);
    }

    void SetMessageBoxByInputField(int count)
    {
        weight = selectedItem.weight * count;
        gold = itemPrice * count;

        Debug.Log("인벤토리 무게 여유분: " + remainingWeight);

        if (weightText != null)
        {
            weightText.text = weight.ToString();
            weightText.color = (currentTab == ShopUI.ShopTab.Purchase && weight > remainingWeight) ? error : nomal;
        }

        if (goldText != null)
        {
            goldText.text = gold.ToString();
            goldText.color = (currentTab == ShopUI.ShopTab.Purchase && gold > GameManager.instance.currentPlayerData.money) ? error : nomal;
        }

        if(currentTab == ShopUI.ShopTab.Purchase)
        {
            if (gold > GameManager.instance.currentPlayerData.money || weight > remainingWeight) SetInteractableYesButton(false);
            else SetInteractableYesButton(true);
        }
        else
        {
            SetInteractableYesButton(true);
        }
    }

    void EndEdit(string text)
    {
        selectedCount = int.Parse(text);

        gold = itemPrice * selectedCount;
    }

    void SetInteractableYesButton(bool interactable)
    {
        yesButton.interactable = interactable;
        yesButtonCanvasGroup.alpha = (interactable) ? 1.0f : 0.5f;
    }

    void ClickYesButton()
    {
        Debug.Log("Yes 버튼 클릭");

        if(currentTab == ShopUI.ShopTab.Purchase)
        {
            Debug.Log("아이템 구매: " + selectedCount + " 개");

            GameManager.instance.AddItem(selectedItem, selectedCount);
            GameManager.instance.AddGold(-gold);
        }
        else
        {
            Debug.Log("아이템 판매: " + selectedCount + " 개");

            // 아이템 제거
            GameManager.instance.DeleteItem(selectedItem, selectedCount);
            GameManager.instance.AddGold(gold);
        }

        selectedItem = null;
        ShopUI.instance.SetShop();

        gameObject.SetActive(false);
    }

    void ClickNoButton()
    {
        Debug.Log("No 버튼 클릭");
        selectedItem = null;

        gameObject.SetActive(false);
    }
}
