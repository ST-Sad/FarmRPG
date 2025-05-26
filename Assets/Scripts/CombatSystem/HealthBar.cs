// HealthBar.cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 生命值显示条，使用 UI Slider 显示实体血量比例
/// </summary>
public class HealthBar : MonoBehaviour
{
    public EntityStats entityStats; // 绑定的实体 Stats
    public Slider healthSlider;     // UI Slider 控件

    void Update()
    {
        if (entityStats != null && healthSlider != null)
        {
            healthSlider.value = (float)entityStats.currentHealth / entityStats.maxHealth;
        }
    }
}
