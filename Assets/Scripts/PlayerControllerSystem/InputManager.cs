// InputManager.cs
using UnityEngine;

// ����ģʽ�¶�ȡ���������
public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }

    void Update()
    {
        // ��ȡˮƽ�ʹ�ֱ�����루����Unity Input�����ж����ᣩ
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
    }
}
