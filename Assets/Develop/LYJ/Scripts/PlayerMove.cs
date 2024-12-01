using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;

    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;
    private bool isGrounded = false;
    private int jumpCount = 0;
    private float defaultColliderHeight;
    private bool isSliding = false;

    private float speedMultiplier = 1f; // 속도 배율? 배수? (기본 값 1)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        defaultColliderHeight = playerCollider.size.y;
    }

    private void Update()
    {
        Move();
        HandleJump();
        HandleSlide();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * speedMultiplier, rb.velocity.y);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < 2))
        {
            rb.velocity = new Vector2(rb.velocity.x * speedMultiplier, jumpPower);
            jumpCount++;
            isGrounded = false;
        }
    }

    private void HandleSlide()
    {
        if (Input.GetKey(KeyCode.Z) && isGrounded)
        {
            StartSlide();
        }
        else
        {
            StopSlide();
        }
    }

    private void StartSlide()
    {
        if (isSliding) return;

        isSliding = true;
        playerCollider.size = new Vector2(playerCollider.size.x, defaultColliderHeight / 2f);
    }

    private void StopSlide()
    {
        if (!isSliding) return;

        isSliding = false;
        playerCollider.size = new Vector2(playerCollider.size.x, defaultColliderHeight);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌한 면이 바닥인 경우 (normal.y > 0.7f로 바닥 확인)
        //Collision타입은 충돌 지점들의 정보를 담는 ContactPoint 타입의 데이터를 contacs라는 배열의 형태로 제공
        //여러 충돌지점중에서 첫번째 충돌지점의 정보를 가져옴
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0; //점프 횟수 초기화
        }
    }

    /// <summary>
    /// 이동 속도 배율 설정 함수
    /// </summary>
    /// <param name="multiplier"></param>
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
