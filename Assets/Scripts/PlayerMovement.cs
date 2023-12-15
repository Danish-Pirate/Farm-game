using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    private Vector2 input;
    private bool isMoving;

    public LayerMask solidObjectsLayer;
    private BoxCollider2D playerCollider;

    private void Start() {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isMoving)
        {
            GetInput();
            HandleMovement();
        }

        animator.SetBool("isMoving", isMoving);
    }

    private void HandleMovement()
    {
        if (input != Vector2.zero)
        {
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
            Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0f);
            if (IsWalkable(targetPos)) {
                StartCoroutine(Move(targetPos));
            }
        }
    }

    private bool IsWalkable(Vector3 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, 0f, solidObjectsLayer)) {
            return false;
        }
        return true;
    }

    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.y != 0) input.x = 0;
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
