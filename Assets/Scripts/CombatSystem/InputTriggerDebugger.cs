using UnityEngine;

public class InputTriggerDebugger : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            float speed = animator.GetFloat("Speed");
            Debug.Log("Current Speed: " + speed);
        }
        // 检测 Space 键按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed at time: " + Time.time);
            // 检查 Attack 触发器是否被设置
            if (animator != null)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attack trigger set for Animator on " + gameObject.name);
            }
        }

        // 检查当前动画状态
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // Base Layer
            if (stateInfo.IsName("Attack") || stateInfo.IsTag("Attack"))
            {
                Debug.Log("Playing Attack animation at time: " + Time.time);
            }
        }
    }
}