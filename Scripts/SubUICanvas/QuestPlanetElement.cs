using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPlanetElement : MonoBehaviour
{
    [SerializeField] Image planetIconImage;
    [SerializeField] TextMeshProUGUI planetNameText;

    public void SetQuestPlanetElement(QuestData quest)
    {
        if(quest != null)
        {
            if (planetIconImage != null ) planetIconImage.sprite = quest.QuestPlanetIcon;
            if (planetNameText != null) planetNameText.text = quest.QuestPlanetName;
        }
    }
}
