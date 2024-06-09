using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaidShopPackItemsDescriptionElement : MonoBehaviour
{
    [SerializeField] Image packItemIconImage;

    [SerializeField] TextMeshProUGUI packItemNameText;
    [SerializeField] TextMeshProUGUI packItemCountText;

    void Start()
    {
        
    }

    public void SetPaidShopPackItemsDescriptionElement(Sprite icon, string name, int count)
    {
        packItemIconImage.sprite = icon;
        packItemNameText.text = name;
        packItemCountText.text = count.ToString("#,##0");
    }
}
