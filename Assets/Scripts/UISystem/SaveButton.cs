using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Text saveFeedbackText;

    private void Start()
    {
        saveButton.onClick.AddListener(OnSaveClicked);
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnSaveClicked()
    {
        SaveGame();
        ShowFeedback("游戏已保存！", 2f);
    }

    private void SaveGame()
    {
        // 创建保存数据对象
        GameSaveData saveData = new GameSaveData
        {
            playerPosition = GameObject.Find("Player").transform.position, // 假设玩家对象名为 "Player"
        };

        // 二进制保存
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamesave.save";
        
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, saveData);
        }
        
        Debug.Log($"游戏保存到: {path}");
    }

    private void ShowFeedback(string message, float duration)
    {
        saveFeedbackText.text = message;
        saveFeedbackText.gameObject.SetActive(true);
        Invoke("HideFeedback", duration);
    }

    private void HideFeedback()
    {
        saveFeedbackText.gameObject.SetActive(false);
    }
}

// 可序列化的保存数据类
[System.Serializable]
public class GameSaveData
{
    public Vector3 playerPosition;
    public float playerHealth;
    public float gameTime;
    // 其他游戏数据...
}