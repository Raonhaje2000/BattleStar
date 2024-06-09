using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDetailContents : MonoBehaviour
{
    [SerializeField] Scrollbar contentScrollbar;

    [Header("퀘스트 내용")]
    [SerializeField] TextMeshProUGUI titleText;

    [SerializeField] Image planetImage;
    [SerializeField] TextMeshProUGUI planetText;

    [SerializeField] Image npcImage;
    [SerializeField] TextMeshProUGUI npcNameText;

    [SerializeField] TextMeshProUGUI goalText;
    [SerializeField] TextMeshProUGUI goalCurrentCountText;
    [SerializeField] TextMeshProUGUI goalMaxCountText;

    [SerializeField] GameObject goalCount;

    [SerializeField] TextMeshProUGUI contentText;

    [Header("퀘스트 보상")]
    [SerializeField] GameObject rewardExp;
    [SerializeField] TextMeshProUGUI rewardExpText;

    [SerializeField] GameObject rewardGold;
    [SerializeField] TextMeshProUGUI rewardGoldText;

    List<GameObject> rewardItems;
    List<Image> rewardItemImages;
    List<TextMeshProUGUI> rewardItemCountTexts;

    [SerializeField] Transform rewardItemParent;
    GameObject rewardItemPrefab;

    [Header("퀘스트 완료 버튼")]
    [SerializeField] Button questCompletButton;
    CanvasGroup questCompletButtonCanvasGroup;

    [SerializeField] GameObject questCompletButtonSystemMessage;

    QuestData questData;

    private void Awake()
    {
        rewardItemPrefab = Resources.Load<GameObject>("Prefabs/UI/QuestUI_QuestRewardItem");

        rewardItems = new List<GameObject>();
        rewardItemImages = new List<Image>();
        rewardItemCountTexts = new List<TextMeshProUGUI>();

        if (rewardItemPrefab != null && rewardItemParent != null)
        {
            for (int i = 0; i < QuestData.REWARD_ITEM_MAX_COUNT; i++)
            {
                GameObject newObject = Instantiate(rewardItemPrefab);
                newObject.transform.parent = rewardItemParent;
                newObject.SetActive(false);

                rewardItems.Add(newObject);

                rewardItemImages.Add(newObject.transform.Find("RewardItemEdge/RewardItemIcon").GetComponent<Image>());
                rewardItemCountTexts.Add(newObject.transform.Find("RewardItemCountText").GetComponent<TextMeshProUGUI>());
            }
        }

        if (questCompletButton != null)
        {
            questCompletButton.onClick.AddListener(ClickCompletButton);
            questCompletButtonCanvasGroup = questCompletButton.gameObject.GetComponent<CanvasGroup>();
        }
       
    }

    void Start()
    {

    }

    public void SetQuestDetailContents(QuestData quest)
    {
        questData = quest;

        if (questData != null)
        {
            // 스크롤바 맨 위로 초기화
            contentScrollbar.value = 1;

            if (titleText != null) titleText.text = questData.QuestTitle;

            if (planetImage != null) planetImage.sprite = questData.QuestPlanetIcon;
            if (planetText != null) planetText.text = questData.QuestPlanetName;

            if (npcImage != null) npcImage.sprite = questData.QuestNpcImage;
            if (npcNameText != null) npcNameText.text = questData.QuestNpcName;

            if (goalText != null) goalText.text = questData.QuestGoal;

            if (goalCount != null)
            {
                if (questData.QuestType != QuestData.Type.Planet)
                {
                    goalCount.SetActive(true);

                    if (goalCurrentCountText != null) goalCurrentCountText.text = questData.CurrentCount.ToString();
                    if (goalMaxCountText != null) goalMaxCountText.text = questData.MaxCount.ToString();
                }
                else
                {
                    goalCount.SetActive(false);
                }
            }

            if (contentText != null) contentText.text = questData.QuestContent;

            if (rewardExp != null)
            {
                if (questData.RewardExp > 0) rewardExp.SetActive(true);
                else rewardExp.SetActive(false);

                if (rewardExpText != null) rewardExpText.text = questData.RewardExp.ToString();
            }

            if(rewardGold != null)
            {
                if (questData.RewardGold > 0) rewardGold.SetActive(true);
                else rewardGold.SetActive(false);

                if (rewardGoldText != null) rewardGoldText.text = questData.RewardGold.ToString();
            }

            if(rewardItems.Count > 0)
            {
                if(questData.RewardItems != null)
                {
                    for(int i = 0; i < rewardItems.Count; i++)
                    {
                        if(i < questData.RewardItems.Count)
                        {
                            rewardItems[i].SetActive(true);

                            if (rewardItemImages != null) rewardItemImages[i].sprite = questData.RewardItems[i].itemImage;
                            if (rewardItemCountTexts != null) rewardItemCountTexts[i].text = questData.RewardItemsCount[i].ToString();
                        }
                        else
                        {
                            rewardItems[i].SetActive(false);
                        }
                    }
                }
            }

            if(questCompletButton != null)
            {
                if (questData.CompletePossible)
                {
                    if (CheckExtraWeight())
                    {
                        SetInteractableCompletButton(true);
                        questCompletButtonSystemMessage.SetActive(false);
                    }
                    else
                    {
                        SetInteractableCompletButton(false);
                        questCompletButtonSystemMessage.SetActive(true);
                    }
                }
                else
                {
                    SetInteractableCompletButton(false);
                    questCompletButtonSystemMessage.SetActive(false);
                }
            }
        }
    }

    bool CheckExtraWeight()
    {
        float extraWeight = GameManager.instance.currentPlayerData.currentShip.carryWeight - GameManager.instance.CalcTotalWeight();
        float itemWeight = 0.0f;

        for (int i = 0; i < questData.RewardItems.Count; i++)
        {
            itemWeight += (questData.RewardItems[i].weight * questData.RewardItemsCount[i]);
        }

        if (questData.QuestType == QuestData.Type.Item) itemWeight -= questData.QuestGetItem.weight * questData.MaxCount;

        return (itemWeight <= extraWeight) ? true : false;
    }

    void SetInteractableCompletButton(bool interactable)
    {
        questCompletButton.interactable = interactable;
        questCompletButtonCanvasGroup.alpha = (interactable) ? 1.0f : 0.5f;
    }

    public void ClickCompletButton()
    {
        //Debug.Log("퀘스트 완료 버튼 클릭");

        QuestUI.instance.RemoveElement(questData);
    }
}
