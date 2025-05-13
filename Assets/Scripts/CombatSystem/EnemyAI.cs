// EnemyAI.cs
using UnityEngine;

// �򵥵ĵ���AI��׷����Ҳ��ڿ���ʱ����˺�
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;       // �ƶ��ٶ�
    public float chaseRange = 5f;      // �����ҵķ�Χ
    public float attackRange = 0.5f;   // ��������
    public int attackDamage = 10;      // �����˺�
    public float attackCooldown = 1.5f; // ������ȴ
    private float lastAttackTime = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= chaseRange)
        {
            // �������
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            // ����ڹ�����Χ�ڲ�����ȴ��ɣ��򹥻����
            if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                // ���������˺������������ CharacterStats��
                CharacterStats playerStats = player.GetComponent<CharacterStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(attackDamage);
                }
                lastAttackTime = Time.time;
            }
        }
    }
}
