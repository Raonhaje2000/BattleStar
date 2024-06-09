using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemElement : MonoBehaviour
{
    CanvasGroup canvasGroup;                        // ĵ���� �׷� ������Ʈ

    [SerializeField] Image itemIconImage;           // ������ ������ �̹���
    [SerializeField] TextMeshProUGUI itemNameText;  // ������ �̸� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI itemCountText; // ������ ���� �ؽ�Ʈ

    float faidInTime;                               // ���̵� �� �ð�
    float faidOutTime;                              // ���̵� �ƿ� �ð�

    float idleTime;                                 // ��� �ð�

    float time;                                     // ���� �ð�

    bool isFaidIn;                                  // ���̵� �� �÷���
    bool isFaidOut;                                 // ���̵� �ƿ� �÷���
    bool isIdle;                                    // ��� �÷���

    private void Awake()
    {
        // ĵ���� �׷� ������Ʈ �ҷ�����
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // �ʱ�ȭ
        time = 0.0f;

        canvasGroup.alpha = 0.0f;

        // ���̵� �� ����
        StartCoroutine(FaidIn());
    }

    // ������ ������Ʈ ����
    public void SetItemListElement(Sprite itemIcon, string itemName, int count)
    {
        itemIconImage.sprite = itemIcon;
        itemNameText.text = itemName;
        itemCountText.text = count.ToString();
    }

    // �ð� �� ����
    public void SetTimeValues(float faidInTime, float faidOutTime, float idleTime)
    {
        this.faidInTime = faidInTime;
        this.faidOutTime = faidOutTime;
        this.idleTime = idleTime;
    }

    // ���̵� �� ó��
    IEnumerator FaidIn()
    {
        if(!isFaidIn)
        {
            isFaidIn = true;

            // �ð� �� ���� �� �ʱ�ȭ
            time = 0.0f;
            canvasGroup.alpha = 0.0f;

            // ���� �ð��� ���̵� �� �ð� ���� ���� ���
            while (time < faidInTime)
            {
                // �ð� ������ ���� ���� ����
                if(faidInTime != 0) canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, time / faidInTime);

                yield return null;

                time += Time.deltaTime;
            }

            // ���� �� ����
            canvasGroup.alpha = 1.0f;

            // ��� ����
            StartCoroutine(WaitIdle());
            StopCoroutine("FaidIn");
        }
    }

    // ��� ó��
    IEnumerator WaitIdle()
    {
        if(!isIdle)
        {
            isIdle = true;

            // ��� �ð� ���� ���
            yield return new WaitForSeconds(idleTime);

            // ���̵� �ƿ� ����
            StartCoroutine(FaidOut());
            StopCoroutine("WaitIdle");

            isIdle = false;
        }
    }

    // ���̵� �ƿ� ó��
    IEnumerator FaidOut()
    {
        if(!isFaidOut)
        {
            isFaidOut = true;

            // �ð� �� ���� �� �ʱ�ȭ
            time = 0.0f;
            canvasGroup.alpha = 1.0f;

            // ���� �ð��� ���̵� �ƿ� �ð� ���� ���� ���
            while(time < faidOutTime)
            {
                // �ð� ������ ���� ���� ����
                if(faidOutTime != 0) canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, time / faidOutTime);

                yield return null;

                time += Time.deltaTime;
            }

            // ���� �� ����
            canvasGroup.alpha = 0.0f;

            // �ش� ������Ʈ ��Ȱ��ȭ
            StopCoroutine("FaidOut");
            gameObject.SetActive(false);

            isFaidOut = false;
        }
    }
}
