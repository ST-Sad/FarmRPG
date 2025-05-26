// CameraController.cs
using UnityEngine;

/// <summary>
/// 简单的摄像机跟随脚本，让相机平滑跟随玩家
/// </summary>
public class CameraController : MonoBehaviour
{
    public Transform target;     // 玩家Transform
    public float smoothing = 5f; // 跟随平滑系数

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 targetCamPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
