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

    private float speedMultiplier = 1f; // �ӵ� ����? ���? (�⺻ �� 1)

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
        //�浹�� ���� �ٴ��� ��� (normal.y > 0.7f�� �ٴ� Ȯ��)
        //CollisionŸ���� �浹 �������� ������ ��� ContactPoint Ÿ���� �����͸� contacs��� �迭�� ���·� ����
        //���� �浹�����߿��� ù��° �浹������ ������ ������
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0; //���� Ƚ�� �ʱ�ȭ
        }
    }

    /// <summary>
    /// �̵� �ӵ� ���� ���� �Լ�
    /// </summary>
    /// <param name="multiplier"></param>
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
