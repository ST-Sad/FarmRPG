using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    [Header("目标地图设置")]
    [SerializeField] private string targetMapName;
    [SerializeField] private bool requireKeyPress = true;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    private bool playerInRange;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!requireKeyPress)
            {
                SceneChange();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    private void Update()
    {
        if (playerInRange && requireKeyPress && Input.GetKeyDown(interactionKey))
        {
            SceneChange();
        }
    }
     private void SceneChange()
    {
        MapManager.Instance.SwitchToMap(targetMapName);
    }

}
