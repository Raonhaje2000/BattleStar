using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionPackageItemDetail : MonoBehaviour
{
    [SerializeField] GameObject detailContentsObject;

    [SerializeField] Image itemIconImage;
    Sprite gemImage;
    Sprite goldImage;

    [SerializeField] TextMeshProUGUI itemNameText;

    [SerializeField] TextMeshProUGUI itemPriceText;

    [SerializeField] GameObject itemPriceTextTailWon;
    [SerializeField] GameObject itemPriceTextTailGem;

    [SerializeField] TextMeshProUGUI itemDiscountRateText;

    [SerializeField] TextMeshProUGUI itemDescriptionText;

    [SerializeField] GameObject PackItemsDescriptionParent;
    GameObject packItemsDescriptionElementPrefab;

    List<GameObject> packItemsDescriptionList;

    [SerializeField] Scrollbar descriptionScrollbar;

    private void Awake()
    {
        packItemsDescriptionElementPrefab = Resources.Load<GameObject>("Prefabs/UI/PaidShopUI_PackItemsDescriptionElement");

        gemImage = Resources.Load<Sprite>("Sprites/Icons/Gem");
        goldImage = Resources.Load<Sprite>("Sprites/Icons/Gold");

        packItemsDescriptionList = new List<GameObject>();
    }

    void Start()
    {
        ActiveDetailContentsObject(false);
    }

    public void ActiveDetailContentsObject(bool active)
    {
        detailContentsObject.SetActive(active);
    }

    // ���� ���� �ʱ�ȭ
    void ResetPackItemsDescriptionList()
    {
        // ������ ��� ����
        for (int i = 0; i < packItemsDescriptionList.Count; i++)
        {
            Destroy(packItemsDescriptionList[i].gameObject);
        }

        packItemsDescriptionList.Clear();
    }

    void CreatePackItemsDescriptionList(Sprite icon, string name, int count)
    {
        GameObject newElement = Instantiate(packItemsDescriptionElementPrefab, PackItemsDescriptionParent.transform);
        packItemsDescriptionList.Add(newElement);

        newElement.GetComponent<PaidShopPackItemsDescriptionElement>().SetPaidShopPackItemsDescriptionElement(icon, name, count);
    }

    public void SetSelectionItemDetailMoney(PaidShopMoneyElement.MoneyType money, Sprite icon, int amount, int price, int discountPercentage)
    {
        ResetPackItemsDescriptionList();

        // ��ũ�ѹ� �� ���� �ʱ�ȭ
        descriptionScrollbar.value = 1;

        if (money == PaidShopMoneyElement.MoneyType.Gem)
        {
            itemIconImage.sprite = gemImage;

            itemNameText.text = string.Format("���� {0} ��", amount.ToString("#,##0"));

            itemPriceTextTailWon.SetActive(true);
            itemPriceTextTailGem.SetActive(false);

            itemDescriptionText.text = "���� ��ȭ �� �������� �����ϴµ� ���Ǵ� ��ȭ�Դϴ�.";

            CreatePackItemsDescriptionList(icon, "����", amount);
        }
        else
        { 
            itemIconImage.sprite = goldImage;

            itemNameText.text = string.Format("��� {0} ��", amount.ToString("#,##0"));

            itemPriceTextTailWon.SetActive(false);
            itemPriceTextTailGem.SetActive(true);

            itemDescriptionText.text = "���� ������ �������� �����ϴµ� ���Ǵ� ��ȭ�Դϴ�.";

            CreatePackItemsDescriptionList(icon, "���", amount);
        }

        itemPriceText.text = price.ToString("#,##0");
        itemDiscountRateText.text = discountPercentage.ToString();
    }

    public void SetSelectionItemDetailItem(PackageItemData packageItem)
    {
        ResetPackItemsDescriptionList();

        // ��ũ�ѹ� �� ���� �ʱ�ȭ
        descriptionScrollbar.value = 1;

        itemIconImage.sprite = packageItem.PackageItemIcon;

        itemNameText.text = packageItem.PackageItemName;

        itemPriceText.text = packageItem.PackageItemPrice.ToString("#,##0");
        itemDiscountRateText.text = packageItem.PackageItemDiscountPercentage.ToString();

        itemPriceTextTailWon.SetActive(false);
        itemPriceTextTailGem.SetActive(true);

        itemDescriptionText.text = packageItem.PackageItemDescription;

        for(int i = 0; i < packageItem.Items.Count; i++)
        {
            int count = (packageItem.ItemsCount[i] > 0) ? packageItem.ItemsCount[i] : 1;

            CreatePackItemsDescriptionList(packageItem.Items[i].itemImage, packageItem.Items[i].itemName, count);
        }
    }
}
