using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetListUI : MonoBehaviour
{
    const int ITEM_LIST_MAX_COUNT = 3;            // ������ ����Ʈ�� �ִ� ����

    // ����ġ ����
    [SerializeField] GameObject expElementObject; // ����ġ ������Ʈ
    AmountElement expElement;                     // ����ġ ������Ʈ


    // ��� ����
    [SerializeField] GameObject goldElementObject; // ��� ������Ʈ
    AmountElement goldElement;                     // ��� ������Ʈ

    // ������ ����
    [SerializeField] Transform itemTransfrom;      // ������ ������Ʈ ���� ��ġ
    GameObject itemPrefab;                         // ������ ������Ʈ ������

    List<GameObject> itemList;                     // ������ ������Ʈ ����Ʈ

    // ���̵� �� / ���̵� �ƿ� ����
    float faidInTime;                              // ���̵� �� �ð�
    float faidOutTime;                             // ���̵� �ƿ� �ð�
    float idleTime;                                // ��� �ð�

    string[] testName = { "������1", "������2", "������3", "������4", "������5" }; // �׽�Ʈ�� ������ �̸�

    private void Awake()
    {
        // ���� ���ҽ� �ҷ�����
        LoadResources();
    }

    void Start()
    {
        // �ʱ�ȭ
        Initialize();
    }

    void Update()
    {
        // ������ ������Ʈ�� ����Ʈ�� ����������, ù��° ������ ������Ʈ�� ��Ȱ��ȭ ������ ���
        if (itemList.Count > 0 && !itemList[0].activeSelf)
        {
            // �ش� ������Ʈ ���� �� ��Ͽ��� ����
            Destroy(itemList[0].gameObject);
            itemList.RemoveAt(0);
        }
    }

    // ���� ���ҽ� �ҷ�����
    void LoadResources()
    {
        itemPrefab = Resources.Load<GameObject>("Prefabs/UI/GetListUI_ItemElement");
    }

    void Initialize()
    {
        // ���̵� ���� �ʱ�ȭ
        faidInTime = 0.3f;
        faidOutTime = 0.3f;

        idleTime = 2.0f;

        // ����ġ ������Ʈ ���� �ʱ�ȭ
        expElement = expElementObject.GetComponent<AmountElement>();
        expElement.SetTimeValues(faidInTime, faidOutTime, idleTime);

        expElementObject.SetActive(false);

        // ��� ������Ʈ ���� �ʱ�ȭ
        goldElement = goldElementObject.GetComponent<AmountElement>();
        goldElement.SetTimeValues(faidInTime, faidOutTime, idleTime);

        goldElementObject.SetActive(false);

        // ������ ������Ʈ ���� �ʱ�ȭ
        itemList = new List<GameObject>();
    }

    // �׽�Ʈ�� (���߿� �κ��丮���� �޾ƿ�)
    void OnlyTest()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            // �׽�Ʈ������ �������� �� �������
            int count = Random.Range(1, 21);

            AddItemListElement(null, count);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            // �׽�Ʈ������ �������� �� �������
            int amount = Random.Range(0, 1001);
            Debug.Log("Gold: " + amount);

            AddGoldAmount(amount);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            // �׽�Ʈ������ �������� �� �������
            int amount = Random.Range(0, 1001);
            Debug.Log("Exp: " + amount);

            AddExpAmount(amount);
        }
    }

    // ������ ȹ�� ó�� (����Ʈ�� �߰�)
    public void AddItemListElement(ItemInfo item, int itemCount)
    {
        // ������ ������Ʈ�� ������ ������ ����Ʈ�� �ִ� ���� �̻��� ���
        if (itemList.Count >= ITEM_LIST_MAX_COUNT)
        {
            // ���� ó�� �����Ǿ��� ������Ʈ ���� �� ��Ͽ��� ����
            Destroy(itemList[0].gameObject);
            itemList.RemoveAt(0);
        }

        // ������ ������Ʈ ����
        GameObject newItemElement = Instantiate(itemPrefab, itemTransfrom);

        // ������ ������Ʈ ������Ʈ ����
        ItemElement itemElement = newItemElement.GetComponent<ItemElement>();
        itemElement.SetTimeValues(faidInTime, faidOutTime, idleTime);

        if (item != null)
        {
            // �������� ���� �ϴ� �ܿ� 
            itemElement.SetItemListElement(item.itemImage, item.itemName, itemCount);
        }
        else
        {
            // �������� ���� ���� �ʴ� ��� (�׽�Ʈ ��)
            int index = Random.Range(0, testName.Length);
            itemElement.SetItemListElement(null, testName[index], itemCount);
        }

        // ������ ������Ʈ ����Ʈ�� �߰�
        itemList.Add(newItemElement);
    }

    // ��� ȹ�� ó�� (ȹ�� �������� �߰�)
    public void AddGoldAmount(int goldAmount)
    {
        // ��� ������Ʈ�� ��Ȱ��ȭ ������ ��� Ȱ��ȭ
        if (!goldElementObject.activeSelf) goldElement.SetActiveAmountElement();

        // �� ����
        goldElement.SetAmount(goldAmount);
    }

    // ����ġ ȹ�� ó�� (ȹ�� �������� �߰�)
    public void AddExpAmount(int expAmount)
    {
        // ����ġ ������Ʈ�� ��Ȱ��ȭ ������ ��� Ȱ��ȭ
        if (!expElementObject.activeSelf) expElement.SetActiveAmountElement();

        // �� ����
        expElement.SetAmount(expAmount);
    }
}
