using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmountElement : MonoBehaviour
{
    CanvasGroup canvasGroup;                     // ĵ���� �׷� ������Ʈ

    [SerializeField] TextMeshProUGUI amountText; // �� �ؽ�Ʈ

    int amount;                                  // ��

    float faidInTime;                            // ���̵� �� �ð�
    float faidOutTime;                           // ���̵� �ƿ� �ð�

    float idleTime;                              // ��� �ð�

    [SerializeField] float currentFaidTime;      // ���� ���̵� �ð�
    [SerializeField] float currentIdleTime;      // ���� ��� �ð�


    [SerializeField] bool isFaidIn;              // ���̵� �� �÷���
    [SerializeField] bool isFaidOut;             // ���̵� �ƿ� �÷���
    [SerializeField] bool isIdle;                // ��� �÷���

    private void Awake()
    {
        // ĵ���� �׷� ������Ʈ �ҷ�����
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // �ʱ�ȭ
        amount = 0;

        currentFaidTime = 0.0f;
        currentIdleTime = 0.0f;

        canvasGroup.alpha = 0.0f;
    }

    // �� UI ��� ����
    public void SetActiveAmountElement()
    {
        amount = 0;

        currentFaidTime = 0.0f;
        currentIdleTime = 0.0f;

        isFaidIn = false;
        isFaidOut = false;
        isIdle = false;

        // ������Ʈ Ȱ��ȭ
        gameObject.SetActive(true);

        // ���̵� �� ����
        StartCoroutine(FaidIn());
    }

    // �� ����
    public void SetAmount(int amount)
    {
        if(isFaidOut)
        {
            // ���̵� �ƿ� ���� ���
            // ���̵� �ƿ� ���� �� �� UI ��� �缼��

            StopCoroutine("FaidOut");
            isFaidOut = false;

            SetActiveAmountElement();
        }
        else if(isIdle)
        {
            // ��� ���� ���
            // ��� �ð� ����
            ExtendIdleTime();
        }

        // ���� �翡 ���� �� �ؽ�Ʈ ����
        this.amount += amount;
        amountText.text = this.amount.ToString();
    }

    // �ð� �� ����
    public void SetTimeValues(float faidInTime, float faidOutTime, float idleTime)
    {
        this.faidInTime = faidInTime;
        this.faidOutTime = faidOutTime;
        this.idleTime = idleTime;
    }

    // ��� �ð� ����
    void ExtendIdleTime()
    {
        currentIdleTime = 0.0f;
    }

    // ���̵� �� ó��
    IEnumerator FaidIn()
    {
        if (!isFaidIn)
        {
            isFaidIn = true;

            // �ð� �� ���� �� �ʱ�ȭ
            currentFaidTime = 0.0f;
            canvasGroup.alpha = 0.0f;

            // ���� ���̵� �ð��� ���̵� �� �ð� ���� ���� ���
            while (currentFaidTime < faidInTime && isFaidIn)
            {
                // �ð� ������ ���� ���� ����
                if (faidInTime != 0) canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, currentFaidTime / faidInTime);

                yield return null;

                currentFaidTime += Time.deltaTime;
            }

            // ���� �� ����
            canvasGroup.alpha = 1.0f;

            // ��� ����
            StartCoroutine(WaitIdle());
            StopCoroutine("FaidIn");

            isFaidIn = false;
        }
    }

    // ��� ó��
    IEnumerator WaitIdle()
    {
        if (!isIdle)
        {
            isIdle = true;

            // �ð� �� ���� �� �ʱ�ȭ
            currentIdleTime = 0.0f;
            canvasGroup.alpha = 1.0f;

            // ���� ��� �ð��� ��� �ð� ���� ���� ��� (�߰��� �� ���� �� ���� ó���� ����)
            while (currentIdleTime < idleTime)
            {
                yield return null;

                currentIdleTime += Time.deltaTime;
            }

            // ���̵� �ƿ� ����
            StartCoroutine(FaidOut());
            StopCoroutine("WaitIdle");

            isIdle = false;
        }
    }

    // ���̵� �ƿ� ó��
    IEnumerator FaidOut()
    {
        if (!isFaidOut)
        {
            isFaidOut = true;

            // �ð� �� ���� �� �ʱ�ȭ
            currentFaidTime = 0.0f;
            canvasGroup.alpha = 1.0f;

            // ���� ���̵� �ð��� ���̵� �ƿ� �ð� ���� ���� ���
            while (currentFaidTime < faidOutTime && isFaidOut)
            {
                // �ð� ������ ���� ���� ����
                if (faidOutTime != 0) canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, currentFaidTime / faidOutTime);

                yield return null;

                currentFaidTime += Time.deltaTime;
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
