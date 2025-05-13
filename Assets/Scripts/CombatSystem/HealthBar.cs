// HealthBar.cs
using UnityEngine;
using UnityEngine.UI;

// ����ֵ��ʾ����Slider �ؼ���
public class HealthBar : MonoBehaviour
{
    public EntityStats entityStats; // ��Ҫ��ʾ����ֵ��ʵ��
    public Slider healthSlider;     // UI Slider�ؼ�

    void Update()
    {
        if (entityStats != null && healthSlider != null)
        {
            // ����SliderֵΪ��ǰ��������
            healthSlider.value = (float)entityStats.currentHealth / entityStats.maxHealth;
        }
    }
}
