using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI positionText; // ��ġ �ؽ�Ʈ

    string planet;   // ���� �༺
    string position; // ���� ��ġ

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

    // ���� ���ҽ� �ҷ�����
    public void LoadResources()
    {
        positionText = transform.Find("PositionBackground/PositionText").GetComponent<TextMeshProUGUI>();
    }

    // �ʱ�ȭ
    public void Initialize()
    {
        planet = "�̸� ���� �༺";
        position = "";

        // �༺ ��ġ ����
        SetPlanet();
    }

    // �༺ ��ġ ����
    public void SetPlanet()
    {
        if (GameManager.instance.currentPlanet != null && GameManager.instance.currentPlanet.name != "")
        {
            // ���� ��ġ�� �༺ �����Ͱ� ���� �ϴ� ���
            // �ش� �༺ �̸����� ����
            this.planet = GameManager.instance.currentPlanet.planetName;
            this.position = "";
        }
        else
        {
            // ���� ��ġ�� �༺ �����Ͱ� ���� ���� �ʴ� ���
            // ����Ʈ ������ ����
            this.planet = "�̸� ���� �༺";
            this.position = "";
        }

        // �ؽ�Ʈ ����
        SetText();
    }

    // ���� ��ġ ����
    public void SetPosition(string position)
    {
        // ���� ��ġ ���� �޾� �� ����
        this.position = position;

        // �ؽ�Ʈ ����
        SetText();
    }

    // �ؽ�Ʈ ����
    void SetText()
    {
        // ���� ��ġ�� ���� ��� '�༺ �̸�', ���� ��ġ�� �ִ� ��� '�༺ �̸� (���� ��ġ)' �������� �ؽ�Ʈ ����
        if (position == "") positionText.text = planet;
        else positionText.text = string.Format("{0} ({1})", planet, position);
    }
}
