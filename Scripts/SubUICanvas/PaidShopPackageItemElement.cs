using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PaidShopPackageItemElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI packageItemNameText;
    [SerializeField] TextMeshProUGUI packageItemPriceText;

    GameObject packItemListElementPrefab;

    [SerializeField] GameObject packItemListParent;
    List<GameObject> packItemList;
    

    PackageItemData packageItemData;

    private void Awake()
    {
        packItemListElementPrefab = Resources.Load<GameObject>("Prefabs/UI/PaidShopUI_PaidShopPackItemListElement");

        packItemList = new List<GameObject>();
    }

    void Start()
    {
        
    }

    public void SetPaidShopPackageItemElement(PackageItemData packageItem)
    {
        packageItemData = packageItem;

        if (packageItemData != null)
        {
            packageItemNameText.text = packageItemData.PackageItemName;
            packageItemPriceText.text = packageItemData.PackageItemPrice.ToString("#,##0");

            for(int i = 0; i < packageItem.Items.Count; i++)
            {
                int count = (packageItem.ItemsCount[i] > 0) ? packageItem.ItemsCount[i] : 1;

                CreatePackItemListElement(packageItemData.Items[i], count);
            }
        }
    }

    void CreatePackItemListElement(ItemInfo item, int count)
    {
        GameObject newElement = Instantiate(packItemListElementPrefab, packItemListParent.transform);
        packItemList.Add(newElement);

        newElement.GetComponent<PaidShopPackItemListElement>().SetPaidShopPackItemListElement(item, count);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("패키지 아이템 왼쪽 버튼 클릭");

            PaidShopUI.instance.SetSelectionPackageItemDetailUI(packageItemData);
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("패키지 아이템 오른쪽 버튼 클릭");

            // 구매 팝업 뜨기
            PaidShopUI.instance.SetMessageBoxUI(packageItemData);
        }
    }
}
