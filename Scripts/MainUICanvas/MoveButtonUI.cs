using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MoveButtonUI : MonoBehaviour
{
    [SerializeField] Button moveButton; // �̵� ��ư

    void Start()
    {
        // ��ư �̺�Ʈ ���
        moveButton.onClick.AddListener(ClickMoveButton);
    }

    // �̵� ��ư Ŭ�� ó��
    void ClickMoveButton()
    {
        //Debug.Log("�̵� ��ư Ŭ��");

        // �� ��ø���� �༺ ���� ��ư ��(�ε��� 3��) �ҷ�����
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    // ��ư ��ȣ�ۿ� ����
    public void SetButtonInteractable(bool interactable)
    {
        moveButton.interactable = interactable;
    }
}
