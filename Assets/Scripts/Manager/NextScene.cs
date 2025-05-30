using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("MyHomeScene");

    }
}