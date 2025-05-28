// InventoryWindow.cs��������
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.I; // �л�������Ĭ�� I ����
    private bool isOpen = true; // ����ʵ��״̬������ΪĬ�ϣ�

    void Start()
    {
        // ��ʼ��ʱ���� isOpen ״̬���� UI ��ʾ��ȷ����ű��߼�һ�£�
        gameObject.SetActive(isOpen);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen; // �л�״̬
        gameObject.SetActive(isOpen); // ͬ������ UI ��ʾ
    }
}