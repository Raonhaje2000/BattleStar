using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemMessageUI : MonoBehaviour
{
    CanvasGroup canvasGroup; // ĵ���� �׷� ������Ʈ

    float idleTime;          // ��� �ð�
    float faidOutTime;       // ���̵� �ƿ� �ð�

    float time;              // ���� �ð�

    bool isIdle;             // ��� �÷���
    bool isFaidOut;          // ���̵� �ƿ� �÷���
 
    [SerializeField] TextMeshProUGUI systemMessageText; // �ý��� �޼��� �ؽ�Ʈ

    private void Awake()
    {
        // ĵ���� �׷� ������Ʈ �ҷ�����
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // �ʱ�ȭ
        idleTime = 5.0f;
        faidOutTime = 1.0f;

        time = 0.0f;

        canvasGroup.alpha = 1.0f;

        // ��� ����
        StartCoroutine(WaitIdle());
    }

    // �ý��� �޼��� �ؽ�Ʈ ����
    public void SetSystemMessage(string message)
    {
        systemMessageText.text = "[System] " + message;
    }

    // ��� ó��
    IEnumerator WaitIdle()
    {
        if (!isIdle)
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
        if (!isFaidOut)
        {
            isFaidOut = true;

            // �ð� �� ���� �� �ʱ�ȭ
            time = 0.0f;
            canvasGroup.alpha = 1.0f;

            // ���� �ð��� ���̵� �ƿ� �ð� ���� ���� ���
            while (time < faidOutTime)
            {
                // �ð� ������ ���� ���� ����
                if (faidOutTime != 0) canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, time / faidOutTime);

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
