using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationUI : MonoBehaviour
{
    [SerializeField] Slider hpBar;              // ü�¹�
    [SerializeField] Slider spBar;              // ���׹̳ʹ�

    [SerializeField] Image expImage;            // ����ġ �̹���
    [SerializeField] TextMeshProUGUI levelText; // ���� �ؽ�Ʈ

    // ������ ��ȭ
    [SerializeField] TextMeshProUGUI gemText;   // ������ ���� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI goldText;  // ������ ��� �ؽ�Ʈ

    void Start()
    {
        SetHpBar();       // ü�¹� ����
        SetSpBar();       // ���׹̳ʹ� ����
        SetLevelAndExp(); // ���� �� ����ġ ����
        SetMoney();       // ��ȭ ����
    }

    void Update()
    {
        SetHpBar();       // ü�¹� ����
        SetSpBar();       // ���׹̳ʹ� ����
        SetLevelAndExp(); // ���� �� ����ġ ����
        SetMoney();       // ��ȭ ����
    }

    // ü�¹� ����
    public void SetHpBar()
    {
        // GameManager�� �÷��̾� �ִ� ü�°� ���� ü�� �����͸� �޾ƿͼ� ü�¹� ����
        hpBar.minValue = 0;
        hpBar.maxValue = GameManager.instance.currentPlayerData.maxHp;
        hpBar.value = GameManager.instance.currentPlayerData.hp;
    }

    public void SetSpBar()
    {
        // GameManager�� �÷��̾� �ִ� ���׹̳ʿ� ���� �����̸� �����͸� �޾ƿͼ� ���׹̳ʹ� ����
        spBar.minValue = 0;
        spBar.maxValue = GameManager.instance.currentPlayerData.maxSp;
        spBar.value = GameManager.instance.currentPlayerData.sp;
    }

    // ���� �� ����ġ ����
    public void SetLevelAndExp()
    {
        // GameManager�� �÷��̾� ���� �����͸� �޾ƿͼ� ���� �ؽ�Ʈ ����
        levelText.text = GameManager.instance.currentPlayerData.level.ToString();

        // GameManager�� �÷��̾� ���� ����ġ�� �ִ� ����ġ �����͸� �޾ƿͼ� �ش� ������ŭ ����ġ �̹����� ä���� ���� ����
        expImage.fillAmount = GameManager.instance.currentPlayerData.exp / GameManager.instance.currentPlayerData.maxExp;
    }

    // ��ȭ ����
    public void SetMoney()
    {
        // GameManager�� �÷��̾� ���� ������ ��� �����͸� �޾ƿͼ� ���� �ؽ�Ʈ�� ��� �ؽ�Ʈ ���� ����
        gemText.text = GameManager.instance.currentPlayerData.gem.ToString();
        goldText.text = GameManager.instance.currentPlayerData.money.ToString();
    }
}
