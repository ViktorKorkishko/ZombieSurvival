
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private Vector2 input;

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }
}
