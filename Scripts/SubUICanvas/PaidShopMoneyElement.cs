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
            moneyNameText.text = "���� ";

            moneyIcon = gemPackItemImage;
        }
        else if (tab == PaidShopUI.PaidShopTab.Gold)
        {
            currentMoneyType = MoneyType.Gold;
            moneyNameText.text = "��� ";

            moneyIcon = goldPackItemImage;
        }

        moneyAmount = amount;
        moneyPrice = price;
        moneyDiscountPercentage = discountPercentage;

        moneyNameText.text += moneyAmount.ToString("#,##0") + " ��";
        moneyPriceText.text = moneyPrice.ToString("#,##0");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("��Ű�� ������ ���� ��ư Ŭ��");

            PaidShopUI.instance.SetSelectionPackageItemDetailUI(currentMoneyType, moneyIcon, moneyAmount, moneyPrice, moneyDiscountPercentage);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("��Ű�� ������ ������ ��ư Ŭ��");

            // ���� �˾� �߱�
            PaidShopUI.instance.SetMessageBoxUI(moneyIcon, moneyNameText.text, moneyAmount, moneyPrice);
        }
    }
}