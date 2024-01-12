using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        [HideInInspector]
        public bool movementEnabled = true;
    
        private Animator animator;
        public float moveSpeed = 1f;
        public float collisionOffset = 0.05f;
        public ContactFilter2D movementFilter;
    
        private Rigidbody2D rb;
        private Vector2 movementInput;
        private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            if (movementInput != Vector2.zero && movementEnabled) {

                bool moveSuccess = TryMove(movementInput);
                if (!moveSuccess) {
                    moveSuccess = TryMove(new Vector2(movementInput.x, 0));
                }
            
                if (!moveSuccess) {
                    moveSuccess = TryMove(new Vector2(0, movementInput.y));
                }
            
                animator.SetFloat(MoveX, movementInput.x);
                animator.SetFloat(MoveY, movementInput.y);
                animator.SetBool(IsMoving, moveSuccess);
            }
            else {
                animator.SetBool(IsMoving, false);
                AudioManager.Instance.StopSound(0);
            }
        }

        private bool TryMove(Vector2 direction) {
            if (direction == Vector2.zero) {
                return false;
            }
        
            bool moveResult = false;
            // Check for collisions
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0) {
                rb.MovePosition(rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
                AudioManager.Instance.PlaySound(Sound.WALK, 0);
                moveResult = true;
            }

            return moveResult;
        }

        void OnMove(InputValue movementValue) {
            movementInput = movementValue.Get<Vector2>();
        }
    }
}