using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopItemElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image itemElementEdge;

    [SerializeField] Image itemIconImage;

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemPriceText;

    ItemInfo slotItem;
    int itemPrice;

    Color specialColor;
    Color normalColor;

    private void Awake()
    {
        specialColor = new Color(255 / 255.0f, 150 / 255.0f, 60 / 255.0f);
        normalColor = Color.white;
    }

    void Start()
    {
        
    }

    public void SetShopItemElement(ItemInfo item, ShopUI.ShopTab tab)
    {
        itemElementEdge.color = (item.itemMainType == ITEMTYPE_MAIN.Speciality) ? specialColor : normalColor;

        itemIconImage.sprite = item.itemImage;
        itemNameText.text = item.itemName;

        if(tab == ShopUI.ShopTab.Purchase)
        {
            itemPrice = item.purchasePrice;
        }
        else
        {
            if (item.itemMainType == ITEMTYPE_MAIN.Speciality && ShopUI.instance.CheckOtherPlanetSpeciality(item))
            {
                itemPrice = Mathf.RoundToInt(item.salePrice + item.salePrice * GameManager.instance.specialityMarginPercent / 100.0f);
            }
            else
            {
                itemPrice = item.salePrice;
            }
        }

        itemPriceText.text = itemPrice.ToString("#,##0");

        slotItem = item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("������ ���� ���� ��ư Ŭ��");

            // Ŭ���� ������ ���� ���� ����
            if(slotItem != null && ShopUI.instance != null) ShopUI.instance.SetSelectionItemDetailUI(slotItem);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("������ ���� ������ ��ư Ŭ��");

            // Ŭ���� ������ ���� �Ǵ� �Ǹ� �˾� �߱�
            if (slotItem != null && ShopUI.instance != null) ShopUI.instance.SetMessageBoxUI(slotItem, itemPrice);
        }
    }
}
