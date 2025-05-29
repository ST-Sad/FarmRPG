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
        // ��� Space ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed at time: " + Time.time);
            // ��� Attack �������Ƿ�����
            if (animator != null)
            {
                animator.SetTrigger("Attack");
                Debug.Log("Attack trigger set for Animator on " + gameObject.name);
            }
        }

        // ��鵱ǰ����״̬
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