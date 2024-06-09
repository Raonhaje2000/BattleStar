using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionItemDetail : MonoBehaviour
{
    [SerializeField] GameObject detailContentsObject;           // ���� ���� ������Ʈ

    [SerializeField] Image itemIcon;                            // ������ ������ �̹���

    [SerializeField] TextMeshProUGUI itemNameText;              // ������ �̸� �ؽ�Ʈ

    [SerializeField] TextMeshProUGUI itemTypeText;              // ������ �з� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI itemWeightText;            // ������ ���� ���� �ؽ�Ʈ

    [SerializeField] TextMeshProUGUI itemDescriptionText;       // ������ ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI itemUseDescriptionText;    // ������ ��� ���� �ؽ�Ʈ

    [SerializeField] TextMeshProUGUI itemPossessionCurrentText; // ������ ���� ������ �ؽ�Ʈ

    [SerializeField] TextMeshProUGUI itemTotalWeightText;       // �ش� �������� ��ü ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI totalWeightText;           // ��ü ���� �ؽ�Ʈ

    [SerializeField] TextMeshProUGUI weightPercentText;         // ��ü ��� �������� �����ϴ� ���� �ۼ�Ʈ �ؽ�Ʈ

    [SerializeField] Slider itemTotalWeightBar;                 // �ش� �������� ��ü ���Ը� �����ִ� ��
    [SerializeField] Slider totalWeightBar;                     // ��ü ���Ը� �����ִ� ��

    [SerializeField] Scrollbar descriptionScrollbar;            // ������ ���� ��ũ�Ѻ��� ��ũ�ѹ�

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
            // ��ũ�ѹ� �� ���� �ʱ�ȭ
            descriptionScrollbar.value = 1;

            if (itemIcon != null) itemIcon.sprite = item.itemImage;

            if (itemNameText != null) itemNameText.text = item.itemName;

            if (itemTypeText != null) itemTypeText.text = item.itemMainTypeString;
            if (itemWeightText != null) itemWeightText.text = item.weight.ToString();

            if (itemDescriptionText != null) itemDescriptionText.text = item.itemDef;
            if (itemUseDescriptionText != null) itemUseDescriptionText.text = item.itemUesDef;

            // ���� ������ �� �޾ƿͼ� �����ϱ�
            float itemAmount = GameManager.instance.CountItem(item);
            if (itemPossessionCurrentText != null) itemPossessionCurrentText.text = itemAmount.ToString();

            // ������ ���� ������ �� �޾ƿͼ� ���� ���Է� �� ��� �� ����
            float itemTotalWeight = itemAmount * item.weight;
            if (itemTotalWeightText != null) itemTotalWeightText.text = itemTotalWeight.ToString();

            SetItemTotalWeightBar(itemTotalWeight);

            // ��ü ���� �� �޾ƿͼ� ����
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
            // �κ��丮 ��ü ���Է� ����
            itemTotalWeightBar.maxValue = GameManager.instance.currentPlayerData.currentShip.carryWeight;
            itemTotalWeightBar.minValue = 0.0f;

            itemTotalWeightBar.value = itemTotalWeight;
        }
    }

    void SetTotalWeightBar(float totalWeight)
    {
        if(totalWeightBar != null)
        {
            // �κ��丮 ��ü ���Է� ����
            totalWeightBar.maxValue = GameManager.instance.currentPlayerData.currentShip.carryWeight;
            totalWeightBar.minValue = 0.0f;

            totalWeightBar.value = totalWeight;
        }
    }
}
