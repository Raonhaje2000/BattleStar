using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackageItemData", menuName = "패키지 아이템")]
public class PackageItemData : ScriptableObject
{
    [SerializeField] Sprite packageItemIcon;

    [SerializeField] string packageItemName;

    [TextArea]
    [SerializeField] string packageItemDescription;

    [SerializeField] List<ItemInfo> items;
    [SerializeField] List<int> itemsCount;

    [SerializeField] int packageItemPrice;
    [SerializeField] int packageItemDiscountPercentage;

    public Sprite PackageItemIcon
    {
        get { return packageItemIcon; }
    }

    public string PackageItemName
    {
        get { return packageItemName; }
    }

    public string PackageItemDescription
    {
        get { return packageItemDescription; }
    }

    public List<ItemInfo> Items
    {
        get { return items; }
    }

    public List<int> ItemsCount
    {
        get { return itemsCount; }
    }

    public int PackageItemPrice
    {
        get { return packageItemPrice; }
    }

    public int PackageItemDiscountPercentage
    {
        get { return packageItemDiscountPercentage; }
    }
}
