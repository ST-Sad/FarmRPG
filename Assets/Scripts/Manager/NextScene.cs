using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LastLoadedScript : MonoBehaviour
{
    IEnumerator Start()
    {
        // 等待一帧，确保其他Start方法都执行完毕
        yield return null;
        SceneManager.LoadScene("MyHomeScene");
    }
}