// PlayerController.cs
using UnityEngine;

/// <summary>
/// ��������ƶ��Ͷ������������������߼�
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public CharacterData characterData; // ��ɫ������������
    private CharacterStats stats;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
        if (stats != null)
        {
            stats.baseData = characterData;
        }
    }

    void Update()
    {
        // ���ö�������
        float moveX = InputManager.Horizontal;
        float moveY = InputManager.Vertical;
        Vector2 move = new Vector2(moveX, moveY);
        if (animator != null)
        {
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Speed", move.magnitude);
        }

        // �������ģ��ƶ�ʱ�۳�����
        if (stats != null && move.magnitude > 0.1f && stats.currentStamina > 0)
        {
            stats.currentStamina -= (int)(characterData.staminaCostPerSecond * Time.deltaTime);
            if (stats.currentStamina < 0) stats.currentStamina = 0;
        }
    }

    void FixedUpdate()
    {
        // ���������ƶ���ɫ����������������޷��ƶ�
        Vector2 move = new Vector2(InputManager.Horizontal, InputManager.Vertical);
        if (stats != null && stats.currentStamina <= 0)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = move.normalized * characterData.moveSpeed;
        }
    }
}
