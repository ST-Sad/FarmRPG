// InputManager.cs
using UnityEngine;

/// <summary>
/// �������������ֵ��ʹ�þ�̬���Է���ȫ�ַ���:contentReference[oaicite:16]{index=16}
/// </summary>
public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }

    void Update()
    {
        // ��ȡˮƽ�ʹ�ֱ�����루Ĭ��ӳ�䵽 WASD/��ͷ�ȣ�
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
    }
}
