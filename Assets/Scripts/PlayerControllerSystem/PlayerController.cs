// PlayerController.cs
using UnityEngine;

/// <summary>
/// 控制玩家移动和动画，包括体力消耗逻辑
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public CharacterData characterData; // 角色基础属性数据
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
        // 设置动画参数
        float moveX = InputManager.Horizontal;
        float moveY = InputManager.Vertical;
        Vector2 move = new Vector2(moveX, moveY);
        if (animator != null)
        {
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Speed", move.magnitude);
        }

        // 体力消耗：移动时扣除体力
        if (stats != null && move.magnitude > 0.1f && stats.currentStamina > 0)
        {
            stats.currentStamina -= (int)(characterData.staminaCostPerSecond * Time.deltaTime);
            if (stats.currentStamina < 0) stats.currentStamina = 0;
        }
    }

    void FixedUpdate()
    {
        // 根据输入移动角色，如果体力不足则无法移动
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
