using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PaidShopPackItemListElement : MonoBehaviour
{
    [SerializeField] Image itemIconImage;
    [SerializeField] TextMeshProUGUI itemCountText;

    [SerializeField] Button itemButton;

    ItemInfo itemData;

    private void Awake()
    {
        itemButton.onClick.AddListener(ClickItemButton);
    }

    void Start()
    {
        
    }
    
    public void SetPaidShopPackItemListElement(ItemInfo item, int count)
    {
        itemData = item;

        itemIconImage.sprite = itemData.itemImage;
        itemCountText.text = count.ToString();
    }

    public void ClickItemButton()
    {
        Debug.Log("아이템 버튼 클릭");

        PaidShopUI.instance.SetSelectionItemDetailUI(itemData);
    }
}
