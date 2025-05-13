// PlayerController.cs
using UnityEngine;

// ��������ƶ��Ͷ���
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
            // ����ɫ����Ӧ�õ� CharacterStats
            stats.baseData = characterData;
        }
    }

    void Update()
    {
        // ��ȡ����
        float moveX = InputManager.Horizontal;
        float moveY = InputManager.Vertical;
        Vector2 move = new Vector2(moveX, moveY);

        // ���¶�������
        if (animator != null)
        {
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Speed", move.magnitude);
        }
    }

    void FixedUpdate()
    {
        // �����ƶ�
        Vector2 move = new Vector2(InputManager.Horizontal, InputManager.Vertical);
        rb.velocity = move.normalized * characterData.moveSpeed;
    }
}
