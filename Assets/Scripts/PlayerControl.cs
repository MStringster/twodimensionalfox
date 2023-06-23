using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float interactDis = 5f;
    [SerializeField] float rotationSpeed = 5f;

    Camera mainCamera;
    [SerializeField] CinemachineVirtualCamera Camera2D;
    [SerializeField] CinemachineVirtualCamera Camera3D;

    [SerializeField] GameObject foreground;

    private bool is2DMode = true;
    public bool Is2DMode { get { return is2DMode; } }


    Vector2 moveInput;
    Rigidbody rb;
    Animator anim;

    bool isFalling = false;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        Camera2D.Priority = 10; 
        Camera3D.Priority = 0;
    }

    void Update()
    {
        MovePlayer();
        if (is2DMode)
        {
            FlipModel();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isFalling)
        {
            if (value.isPressed)
            {
                anim.SetTrigger("Jumping");
                rb.velocity += new Vector3(0f, jumpSpeed, 0f);
                isFalling = true;
                
                

                StartCoroutine(DelayTransitionToFall());
            }
        }
    }

    IEnumerator DelayTransitionToFall()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Grounded", false);
    }

    void OnUse(InputValue value)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDis);

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Interactable"))
            {
                collider.gameObject.GetComponent<InteractableObject>().Interact();
            }
        }
    }

    void MovePlayer()
    {
        Vector3 playerVelocity;

        if (is2DMode)
        {
            playerVelocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, 0f);
            rb.velocity = playerVelocity;
        }
        else
        {
            playerVelocity = new Vector3(moveInput.y, 0f, moveInput.x) * moveSpeed * Time.deltaTime;
            if (playerVelocity.magnitude > 0f)
            {
                playerVelocity = transform.TransformDirection(playerVelocity);
                transform.position += playerVelocity;
            }

            if (moveInput.x != 0f)
            {
                Quaternion toRotation = Quaternion.Euler(0f, moveInput.x * rotationSpeed * Time.deltaTime, 0f) * transform.rotation;
                transform.rotation = toRotation;
                anim.SetBool("Turning", true);
            }
            else
            {
                anim.SetBool("Turning", false);
            }
        }

        bool playerHasSpeed = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;
        anim.SetBool("Running", playerHasSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Floor")
        {
            isFalling = false;
            anim.SetBool("Grounded", true);
        }
    }

    public void SwitchPerspective()
    {
        is2DMode = !is2DMode;

        if (is2DMode)
        {
            Camera2D.Priority = 10;
            Camera3D.Priority = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            mainCamera.orthographic = true;
            foreground.SetActive(false);
        }
        else
        {
            Camera2D.Priority = 0;
            Camera3D.Priority = 10;
            mainCamera.orthographic = false;
            transform.localScale = new Vector3(Mathf.Sign(1f),1f , 1f);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            foreground.SetActive(true);
        }
    }
    void FlipModel()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactDis);
    }
}