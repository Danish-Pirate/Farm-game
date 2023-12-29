using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

enum MovementDirection {
    NONE, UP, DOWN, LEFT, RIGHT
}
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    private MovementDirection movementDirection;
    private bool isMoving;
    private Rigidbody2D rb;
    
    public Transform movePoint;
    public Tilemap solidObjects;

    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        movePoint.SetParent(null);
    }

    private void Update() {
        HandleInput();
    }

    private void FixedUpdate() {
        if (movementDirection != MovementDirection.NONE) {
            GetTargetPos();
            if (IsWalkable()) {
                Move();
            }
        }
        StartCoroutine(AnimateMovement());
    }

    private bool IsWalkable() {
        Vector3Int solidMapTile = solidObjects.WorldToCell(movePoint.position);
        if (solidObjects.GetTile(solidMapTile) == null) {
            return true;
        }

        return false;
    }

    private void GetTargetPos() {
        // A new target pos should not be calculated if the player is moving.
        if (isMoving) return;

        Vector2 movement = Vector2.zero;

        switch (movementDirection)
        {
            case MovementDirection.UP:
                movement = Vector2.up;
                break;
            case MovementDirection.DOWN:
                movement = Vector2.down;
                break;
            case MovementDirection.LEFT:
                movement = Vector2.left;
                break;
            case MovementDirection.RIGHT:
                movement = Vector2.right;
                break;
        }
        movePoint.position = rb.position + movement;
        AnimateFaceDirection(movement);
    }
    
    private void HandleInput() {
        // Disable input when moving
        if (isMoving) return;
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        // If no input detected, exit
        if (x == 0 && y == 0) return;

        if (y != 0) {
            if (y > 0) movementDirection = MovementDirection.UP;
            else movementDirection = MovementDirection.DOWN;
        }
        else {
            if (x > 0) movementDirection = MovementDirection.RIGHT;
            else movementDirection = MovementDirection.LEFT;
        }
    }

    private void Move()
    {
        Vector2 targetPos = movePoint.position;

        // Check if the target position is reached
        if (Vector2.Distance(rb.position, targetPos) > Mathf.Epsilon)
        {
            // Move towards the target position
            rb.position = Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.deltaTime);
            isMoving = true;
        }
        else
        {
            // Stop moving
            isMoving = false;
            movementDirection = MovementDirection.NONE;
        }
    }

    private void AnimateFaceDirection(Vector2 movement) {
        animator.SetFloat(MoveX, movement.x);
        animator.SetFloat(MoveY, movement.y);
    }

    private IEnumerator AnimateMovement()
    {
        Vector2 originalPos = rb.position;
        yield return new WaitForSeconds(0.1f);

        // Check if the player has moved during the delay
        animator.SetBool(IsMoving, rb.position != originalPos);
    }
}