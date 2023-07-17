using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool up, down, left, right, isMoving;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    void Update() {

        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        if(up || down || left || right) {
            isMoving = true;
        } else {
            isMoving = false;
        }
        if (moveHorizontal < 0) {
            spriteRenderer.flipX = true;
        }else if(moveHorizontal > 0) {
            spriteRenderer.flipX = false;
        }
        animator.SetBool("isMoving", isMoving);

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        //movement.Normalize();

        movement *= movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }


}
