using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetListUI : MonoBehaviour
{
    const int ITEM_LIST_MAX_COUNT = 3;            // 아이템 리스트의 최대 개수

    // 경험치 관련
    [SerializeField] GameObject expElementObject; // 경험치 오브젝트
    AmountElement expElement;                     // 경험치 컴포넌트


    // 골드 관련
    [SerializeField] GameObject goldElementObject; // 골드 오브젝트
    AmountElement goldElement;                     // 골드 컴포넌트

    // 아이템 관련
    [SerializeField] Transform itemTransfrom;      // 아이템 오브젝트 생성 위치
    GameObject itemPrefab;                         // 아이템 오브젝트 프리팹

    List<GameObject> itemList;                     // 아이템 오브젝트 리스트

    // 페이드 인 / 페이드 아웃 관련
    float faidInTime;                              // 페이드 인 시간
    float faidOutTime;                             // 페이드 아웃 시간
    float idleTime;                                // 대기 시간

    string[] testName = { "아이템1", "아이템2", "아이템3", "아이템4", "아이템5" }; // 테스트용 아이템 이름

    private void Awake()
    {
        // 관련 리소스 불러오기
        LoadResources();
    }

    void Start()
    {
        // 초기화
        Initialize();
    }

    void Update()
    {
        // 아이템 오브젝트가 리스트에 남아있으나, 첫번째 아이템 오브젝트가 비활성화 상태인 경우
        if (itemList.Count > 0 && !itemList[0].activeSelf)
        {
            // 해당 오브젝트 제거 및 목록에서 제거
            Destroy(itemList[0].gameObject);
            itemList.RemoveAt(0);
        }
    }

    // 관련 리소스 불러오기
    void LoadResources()
    {
        itemPrefab = Resources.Load<GameObject>("Prefabs/UI/GetListUI_ItemElement");
    }

    void Initialize()
    {
        // 페이드 관련 초기화
        faidInTime = 0.3f;
        faidOutTime = 0.3f;

        idleTime = 2.0f;

        // 경험치 오브젝트 관련 초기화
        expElement = expElementObject.GetComponent<AmountElement>();
        expElement.SetTimeValues(faidInTime, faidOutTime, idleTime);

        expElementObject.SetActive(false);

        // 골드 오브젝트 관련 초기화
        goldElement = goldElementObject.GetComponent<AmountElement>();
        goldElement.SetTimeValues(faidInTime, faidOutTime, idleTime);

        goldElementObject.SetActive(false);

        // 아이템 오브젝트 관련 초기화
        itemList = new List<GameObject>();
    }

    // 테스트용 (나중에 인벤토리에서 받아옴)
    void OnlyTest()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            // 테스트용으로 랜덤으로 값 집어넣음
            int count = Random.Range(1, 21);

            AddItemListElement(null, count);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            // 테스트용으로 랜덤으로 값 집어넣음
            int amount = Random.Range(0, 1001);
            Debug.Log("Gold: " + amount);

            AddGoldAmount(amount);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            // 테스트용으로 랜덤으로 값 집어넣음
            int amount = Random.Range(0, 1001);
            Debug.Log("Exp: " + amount);

            AddExpAmount(amount);
        }
    }

    // 아이템 획득 처리 (리스트에 추가)
    public void AddItemListElement(ItemInfo item, int itemCount)
    {
        // 아이템 오브젝트의 개수가 아이템 리스트의 최대 개수 이상인 경우
        if (itemList.Count >= ITEM_LIST_MAX_COUNT)
        {
            // 가장 처음 생성되었던 오브젝트 제거 후 목록에서 제거
            Destroy(itemList[0].gameObject);
            itemList.RemoveAt(0);
        }

        // 아이템 오브젝트 생성
        GameObject newItemElement = Instantiate(itemPrefab, itemTransfrom);

        // 아이템 오브젝트 컴포넌트 세팅
        ItemElement itemElement = newItemElement.GetComponent<ItemElement>();
        itemElement.SetTimeValues(faidInTime, faidOutTime, idleTime);

        if (item != null)
        {
            // 아이템이 존재 하는 겨우 
            itemElement.SetItemListElement(item.itemImage, item.itemName, itemCount);
        }
        else
        {
            // 아이템이 존재 하지 않는 경우 (테스트 용)
            int index = Random.Range(0, testName.Length);
            itemElement.SetItemListElement(null, testName[index], itemCount);
        }

        // 생성된 오브젝트 리스트에 추가
        itemList.Add(newItemElement);
    }

    // 골드 획득 처리 (획득 누적량에 추가)
    public void AddGoldAmount(int goldAmount)
    {
        // 골드 오브젝트가 비활성화 상태인 경우 활성화
        if (!goldElementObject.activeSelf) goldElement.SetActiveAmountElement();

        // 양 세팅
        goldElement.SetAmount(goldAmount);
    }

    // 경험치 획득 처리 (획득 누적량에 추가)
    public void AddExpAmount(int expAmount)
    {
        // 경험치 오브젝트가 비활성화 상태인 경우 활성화
        if (!expElementObject.activeSelf) expElement.SetActiveAmountElement();

        // 양 세팅
        expElement.SetAmount(expAmount);
    }
}
