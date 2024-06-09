using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PaidShopMoneyElement : MonoBehaviour, IPointerClickHandler
{
    public enum MoneyType
    {
        Gem = 0,
        Gold
    }

    Sprite gemPackItemImage;
    Sprite goldPackItemImage;

    [SerializeField] TextMeshProUGUI moneyNameText;
    [SerializeField] TextMeshProUGUI moneyPriceText;

    MoneyType currentMoneyType;

    Sprite moneyIcon;
    int moneyAmount;
    int moneyPrice;
    int moneyDiscountPercentage;

    private void Awake()
    {
        gemPackItemImage = Resources.Load<Sprite>("Sprites/Icons/Gem");
        goldPackItemImage = Resources.Load<Sprite>("Sprites/Icons/Gold");
    }

    void Start()
    {
        
    }

    public void SetPaidShopMoneyElement(PaidShopUI.PaidShopTab tab, int amount, int price, int discountPercentage)
    {
        if (tab == PaidShopUI.PaidShopTab.Gem)
        {
            currentMoneyType = MoneyType.Gem;
            moneyNameText.text = "보석 ";

            moneyIcon = gemPackItemImage;
        }
        else if (tab == PaidShopUI.PaidShopTab.Gold)
        {
            currentMoneyType = MoneyType.Gold;
            moneyNameText.text = "골드 ";

            moneyIcon = goldPackItemImage;
        }

        moneyAmount = amount;
        moneyPrice = price;
        moneyDiscountPercentage = discountPercentage;

        moneyNameText.text += moneyAmount.ToString("#,##0") + " 개";
        moneyPriceText.text = moneyPrice.ToString("#,##0");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("패키지 아이템 왼쪽 버튼 클릭");

            PaidShopUI.instance.SetSelectionPackageItemDetailUI(currentMoneyType, moneyIcon, moneyAmount, moneyPrice, moneyDiscountPercentage);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("패키지 아이템 오른쪽 버튼 클릭");

            // 구매 팝업 뜨기
            PaidShopUI.instance.SetMessageBoxUI(moneyIcon, moneyNameText.text, moneyAmount, moneyPrice);
        }
    }
}