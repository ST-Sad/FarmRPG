// SeasonManager.cs
using UnityEngine;

/// <summary>
/// ���ڹ�����������ȫ�ּ�¼��ǰ���ڣ����ڱ༭�����л���
/// </summary>
public class SeasonManager : MonoBehaviour
{
    public enum Season { Spring, Summer, Autumn, Winter }
    [Tooltip("��Ϸ��ʼʱ�ļ���")]
    public Season startingSeason = Season.Spring;
    public static Season currentSeason;

    void Awake()
    {
        // ����̬��������Ϊ��ʼ����
        currentSeason = startingSeason;
    }
}
