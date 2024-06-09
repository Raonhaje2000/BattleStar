using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionItemDetail : MonoBehaviour
{
    [SerializeField] GameObject detailContentsObject;           // 세부 내용 오브젝트

    [SerializeField] Image itemIcon;                            // 아이템 아이콘 이미지

    [SerializeField] TextMeshProUGUI itemNameText;              // 아이템 이름 텍스트

    [SerializeField] TextMeshProUGUI itemTypeText;              // 아이템 분류 텍스트
    [SerializeField] TextMeshProUGUI itemWeightText;            // 아이템 개별 무게 텍스트

    [SerializeField] TextMeshProUGUI itemDescriptionText;       // 아이템 설명 텍스트
    [SerializeField] TextMeshProUGUI itemUseDescriptionText;    // 아이템 사용 설명 텍스트

    [SerializeField] TextMeshProUGUI itemPossessionCurrentText; // 아이템 현재 보유량 텍스트

    [SerializeField] TextMeshProUGUI itemTotalWeightText;       // 해당 아이템의 전체 무게 텍스트
    [SerializeField] TextMeshProUGUI totalWeightText;           // 전체 무게 텍스트

    [SerializeField] TextMeshProUGUI weightPercentText;         // 전체 대비 아이템이 차지하는 무게 퍼센트 텍스트

    [SerializeField] Slider itemTotalWeightBar;                 // 해당 아이템의 전체 무게를 보여주는 바
    [SerializeField] Slider totalWeightBar;                     // 전체 무게를 보여주는 바

    [SerializeField] Scrollbar descriptionScrollbar;            // 아이템 설명 스크롤뷰의 스크롤바

    void Start()
    {
        ActiveDetailContentsObject(false);
    }

    public void ActiveDetailContentsObject(bool active)
    {
        if (detailContentsObject != null) detailContentsObject.SetActive(active);
    }

    public void SetSelectionItemDetail(ItemInfo item)
    {
        if (item != null)
        {
            // 스크롤바 맨 위로 초기화
            descriptionScrollbar.value = 1;

            if (itemIcon != null) itemIcon.sprite = item.itemImage;

            if (itemNameText != null) itemNameText.text = item.itemName;

            if (itemTypeText != null) itemTypeText.text = item.itemMainTypeString;
            if (itemWeightText != null) itemWeightText.text = item.weight.ToString();

            if (itemDescriptionText != null) itemDescriptionText.text = item.itemDef;
            if (itemUseDescriptionText != null) itemUseDescriptionText.text = item.itemUesDef;

            // 현재 보유량 값 받아와서 세팅하기
            float itemAmount = GameManager.instance.CountItem(item);
            if (itemPossessionCurrentText != null) itemPossessionCurrentText.text = itemAmount.ToString();

            // 아이템 현재 보유량 값 받아와서 개당 무게로 값 계산 후 세팅
            float itemTotalWeight = itemAmount * item.weight;
            if (itemTotalWeightText != null) itemTotalWeightText.text = itemTotalWeight.ToString();

            SetItemTotalWeightBar(itemTotalWeight);

            // 전체 무게 값 받아와서 세팅
            float totalWeight = GameManager.instance.CalcTotalWeight();
            if (totalWeightText != null) totalWeightText.text = totalWeight.ToString();

            SetTotalWeightBar(totalWeight);

            float percent = itemTotalWeight / totalWeight * 100.0f;
            if (weightPercentText != null) weightPercentText.text = (Mathf.Round(percent * 100) / 100).ToString();
        }
    }

    void SetItemTotalWeightBar(float itemTotalWeight)
    {
        if(itemTotalWeightBar != null)
        {
            // 인벤토리 전체 무게로 세팅
            itemTotalWeightBar.maxValue = GameManager.instance.currentPlayerData.currentShip.carryWeight;
            itemTotalWeightBar.minValue = 0.0f;

            itemTotalWeightBar.value = itemTotalWeight;
        }
    }

    void SetTotalWeightBar(float totalWeight)
    {
        if(totalWeightBar != null)
        {
            // 인벤토리 전체 무게로 세팅
            totalWeightBar.maxValue = GameManager.instance.currentPlayerData.currentShip.carryWeight;
            totalWeightBar.minValue = 0.0f;

            totalWeightBar.value = totalWeight;
        }
    }
}
