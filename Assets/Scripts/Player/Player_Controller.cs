using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Player_Controls playerControls;
    private Vector2 movements;
    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        playerControls = new Player_Controls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate() 
    {
        Move();
        AdjustPlayerDirection();
    }
    private void OnEnable() 
    {
        playerControls.Enable();
    }
    private void PlayerInput()
    {
        movements=playerControls.Movements.Move.ReadValue<Vector2>();
        
        animator.SetFloat("moveX",movements.x);
        animator.SetFloat("moveY",movements.y);
    }
    private void Move()
    {
        rb.MovePosition(rb.position + movements*(moveSpeed*Time.fixedDeltaTime));
    }
    private void AdjustPlayerDirection ()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    
}
