// CameraController.cs
using UnityEngine;

/// <summary>
/// �򵥵����������ű��������ƽ���������
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform target;     // ���Transform
    public float smoothing = 5f; // ����ƽ��ϵ��
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void LateUpdate()
    {
        target = GameObject.Find("Player").transform;
        Vector3 targetCamPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
