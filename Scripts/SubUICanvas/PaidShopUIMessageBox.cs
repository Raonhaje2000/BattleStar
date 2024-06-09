using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaidShopUIMessageBox : MonoBehaviour
{
    const int STRING_LENGTH = 10;

    [SerializeField] Image packageItemIcon;

    [SerializeField] TextMeshProUGUI packageNameText;
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] GameObject wonIconObject;
    [SerializeField] GameObject gemIconObject;

    [SerializeField] TextMeshProUGUI systemMessageText;

    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    PaidShopUI.PaidShopTab currentTab;
    PackageItemData selectedItem;
    int itemAmount;
    int itemPrice;

    Color nomal;
    Color error;

    CanvasGroup yesButtonCanvasGroup;

    private void Awake()
    {
        if (yesButton != null) yesButton.onClick.AddListener(ClickYesButton);
        if (noButton != null) noButton.onClick.AddListener(ClickNoButton);

        if (yesButton != null) yesButtonCanvasGroup = yesButton.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        nomal = Color.white;
        error = new Color(200 / 255.0f, 0, 0);

        gameObject.SetActive(false);
    }

    public void SetMessageBox(PaidShopUI.PaidShopTab tab, Sprite icon, string name, int amount, int price)
    {
        currentTab = tab;
        itemAmount = amount;
        itemPrice = price;

        packageItemIcon.sprite = icon;
        packageNameText.text = name;
        moneyText.text = itemPrice.ToString("#,##0");

        if(currentTab == PaidShopUI.PaidShopTab.Gem)
        {
            wonIconObject.SetActive(true);
            gemIconObject.SetActive(false);
        }
        else
        {
            wonIconObject.SetActive(false);
            gemIconObject.SetActive(true);
        }

        SetSystemMessage();

        gameObject.SetActive(true);
    }

    public void SetMessageBox(PaidShopUI.PaidShopTab tab, PackageItemData packageItem)
    {
        currentTab = tab;
        selectedItem = packageItem;
        itemPrice = packageItem.PackageItemPrice;

        packageItemIcon.sprite = selectedItem.PackageItemIcon;
        //packageNameText.text = selectedItem.PackageItemName;

        if (selectedItem.PackageItemName.Length <= STRING_LENGTH) packageNameText.text = selectedItem.PackageItemName;
        else packageNameText.text = selectedItem.PackageItemName.Substring(0, STRING_LENGTH) + "...";

        moneyText.text = itemPrice.ToString("#,##0");

        wonIconObject.SetActive(false);
        gemIconObject.SetActive(true);

        SetSystemMessage();

        gameObject.SetActive(true);
    }

    void SetSystemMessage()
    {
        systemMessageText.color = nomal;
        systemMessageText.text = "신중하게 결정하세요.";

        SetInteractableYesButton(true);

        if (currentTab == PaidShopUI.PaidShopTab.Item && !CheckExtraWeight())
        {
            systemMessageText.color = error;
            systemMessageText.text = "인벤토리 무게를 초과합니다.";

            SetInteractableYesButton(false);
        }

        if(currentTab != PaidShopUI.PaidShopTab.Gem)
        {
            if(itemPrice > GameManager.instance.currentPlayerData.gem)
            {
                systemMessageText.color = error;
                systemMessageText.text = "보석이 부족합니다.";

                SetInteractableYesButton(false);
            }
        }
    }

    void SetInteractableYesButton(bool interactable)
    {
        yesButton.interactable = interactable;
        yesButtonCanvasGroup.alpha = (interactable) ? 1.0f : 0.5f;
    }

    bool CheckExtraWeight()
    {
        float extraWeight = GameManager.instance.currentPlayerData.currentShip.carryWeight - GameManager.instance.CalcTotalWeight();
        float itemWeight = 0.0f;

        for(int i = 0; i < selectedItem.Items.Count; i++)
        {
            itemWeight += (selectedItem.Items[i].weight * selectedItem.ItemsCount[i]);
        }

        Debug.Log("전체 무게: " + GameManager.instance.currentPlayerData.currentShip.carryWeight + " / 여유 무게: " + extraWeight + " / 아이템 무게: " + itemWeight);

        return (itemWeight <= extraWeight) ? true : false;
    }

    void ClickYesButton()
    {
        Debug.Log("Yes 버튼 클릭");

        // 아이템 획득 처리
        if(currentTab == PaidShopUI.PaidShopTab.Gem)
        {
            GameManager.instance.AddGem(itemAmount);
        }
        else if(currentTab == PaidShopUI.PaidShopTab.Gold)
        {
            GameManager.instance.AddGem(-itemPrice);
            GameManager.instance.AddGold(itemAmount);
        }
        else
        {
            GameManager.instance.AddGem(-itemPrice);

            for(int i = 0; i < selectedItem.Items.Count; i++)
            {
                GameManager.instance.AddItem(selectedItem.Items[i], selectedItem.ItemsCount[i]);
            }
        }

        PaidShopUI.instance.SetPaidShop();

        selectedItem = null;

        gameObject.SetActive(false);
    }

    void ClickNoButton()
    {
        Debug.Log("No 버튼 클릭");

        selectedItem = null;

        gameObject.SetActive(false);
    }
}
