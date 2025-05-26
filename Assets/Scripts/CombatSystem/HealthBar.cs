// HealthBar.cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ֵ��ʾ����ʹ�� UI Slider ��ʾʵ��Ѫ������
/// </summary>
public class HealthBar : MonoBehaviour
{
    public EntityStats entityStats; // �󶨵�ʵ�� Stats
    public Slider healthSlider;     // UI Slider �ؼ�

    void Update()
    {
        if (entityStats != null && healthSlider != null)
        {
            healthSlider.value = (float)entityStats.currentHealth / entityStats.maxHealth;
        }
    }
}
