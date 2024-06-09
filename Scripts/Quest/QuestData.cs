using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "����Ʈ")]
public class QuestData : ScriptableObject
{
    public const int REWARD_ITEM_MAX_COUNT = 3;

    public enum Type
    { 
        Planet = 0, // �༺ �湮
        Item,       // ������ ȹ��
        Enemy       // �� óġ
    }

    [Header("����Ʈ ����")]
    [SerializeField] int questId;                // ����Ʈ ID
    [SerializeField] string questTitle;          // ����Ʈ ����

    [Header("����Ʈ ����")]
    [SerializeField] Sprite questPlanetIcon;     // ����Ʈ �༺ ������
    [SerializeField] string questPlanetName;     // ����Ʈ �༺ �̸�

    [SerializeField] Sprite questNpcImage;       // ����Ʈ NPC �̹���
    [SerializeField] string questNpcName;        // ����Ʈ NPC �̸�

    [SerializeField] Type questType;              // ����Ʈ ����
    [SerializeField] PlanetInfo questVisitPlanet; // ����Ʈ �湮 �༺
    [SerializeField] ItemInfo questGetItem;       // ����Ʈ ȹ�� ������

    [SerializeField] string questGoal;           // ����Ʈ ��ǥ
    [SerializeField] int maxCount;               // ����Ʈ �ϷḦ ���� ����
    [SerializeField] int currentCount = 0;       // ���� �޼��� ����

    [TextArea]
    [SerializeField] string questContent;        // ����Ʈ ����

    [Header("����Ʈ ����")]
    [SerializeField] int rewardExp;              // ���� ����ġ
    [SerializeField] int rewardGold;             // ���� ���
    [SerializeField] List<ItemInfo> rewardItems; // ���� �����۵�
    [SerializeField] List<int> rewardItemsCount; // ���� ������ ����

    bool progress = false;                       // ����Ʈ ���� Ȯ�� �÷���
    bool completePossible = false;               // ����Ʈ �Ϸ� ���� Ȯ�� �÷���

    [SerializeField]
    bool viewCheck = false;                      // ����Ʈ üũ ǥ�� Ȯ�� �÷���

    public int QuestId
    {
        get { return questId; }
    }

    public string QuestTitle
    {
        get { return questTitle; }
    }

    public Sprite QuestPlanetIcon
    {
        get { return questPlanetIcon; }
    }

    public string QuestPlanetName
    {
        get { return questPlanetName; }
    }

    public Sprite QuestNpcImage
    {
        get { return questNpcImage; }
    }

    public string QuestNpcName
    {
        get { return questNpcName; }
    }

    public Type QuestType
    { 
        get { return questType; }
    }

    public PlanetInfo QuestVisitPlanet
    { 
        get { return questVisitPlanet; }
    }

    public ItemInfo QuestGetItem
    {
        get { return questGetItem; }
    }

    public string QuestGoal
    {
        get { return questGoal; }
    }

    public int MaxCount
    {
        get
        {
            if (questType == Type.Planet) maxCount = 1;
            return maxCount; 
        }
    }

    public int CurrentCount
    {
        get { return currentCount; }
        set { currentCount = value; }
    }

    public string QuestContent
    {
        get { return questContent; }
    }

    public int RewardExp
    {
        get { return rewardExp; }
    }

    public int RewardGold
    {
        get { return rewardGold; }
    }

    public List<ItemInfo> RewardItems
    {
        get { return rewardItems; }
    }

    public List<int> RewardItemsCount
    {
        get { return rewardItemsCount; }
    }

    public bool Progress
    {
        get { return progress; }
        set { progress = value; }
    }

    public bool CompletePossible
    { 
        get
        {
            completePossible = (currentCount >= maxCount) ? true : false;

            return completePossible; 
        }
        set { completePossible = value; }
    }

    public bool ViewCheck
    {
        get { return viewCheck; }
        set { viewCheck = value; }
    }
}
