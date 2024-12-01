using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour, IItem
{
    [SerializeField] private float magnetRadius = 5f; // 자석 범위
    [SerializeField] private float pullSpeed = 10f;   // 코인이 끌려오는 속도
    [SerializeField] private float activeDuration = 5f; // 자석 효과 지속 시간
    private Player player; // 플레이어 참조
    private bool isActive = false; // 자석이 활성화되었는지 여부

    private List<GameObject> coinsInRange = new List<GameObject>(); // 범위 안의 코인 저장

    private void Update()
    {
        if (isActive && player != null)
        {
            // 범위 내의 코인을 끌어오기
            for (int i = 0; i < coinsInRange.Count; i++)
            {
                if (coinsInRange[i] != null)
                {
                    // 코인을 플레이어 쪽으로 이동
                    coinsInRange[i].transform.position = Vector3.MoveTowards(
                        coinsInRange[i].transform.position,
                        player.transform.position,
                        pullSpeed * Time.deltaTime
                    );

                    // 플레이어와 닿으면 코인을 제거
                    if (Vector3.Distance(coinsInRange[i].transform.position, player.transform.position) < 0.1f)
                    {
                        CollectCoin(coinsInRange[i]);
                        coinsInRange.RemoveAt(i);
                        i--; // 리스트에서 제거 후 인덱스 조정
                    }
                }
            }
        }
    }

    // IItem 인터페이스의 Activate() 구현: 자석 효과 시작
    public void Activate()
    {
        if (isActive || player == null) return;

        isActive = true;
        Debug.Log("자석 아이템이 활성화되었습니다!");

        // 일정 주기마다 범위 내 코인 검색
        StartCoroutine(CheckCoinsInRange());

        // 자석 지속 시간이 지나면 비활성화
        StartCoroutine(DeactivateAfterDuration());
    }

    // IItem 인터페이스의 Deactivate() 구현: 자석 효과 종료
    public void Deactivate()
    {
        if (!isActive) return;

        isActive = false;
        Debug.Log("자석 아이템이 비활성화되었습니다.");
        StopAllCoroutines(); // 코루틴 정지
        coinsInRange.Clear(); // 리스트 초기화
        Destroy(gameObject);
    }

    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(activeDuration); // 자석 효과 지속 시간
        Deactivate(); // 자석 효과 비활성화
    }

    private IEnumerator CheckCoinsInRange()
    {
        while (isActive && player != null)
        {
            // 플레이어 주변의 범위 내 코인 찾기
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.transform.position, magnetRadius);

            coinsInRange.Clear();
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Coin"))
                {
                    coinsInRange.Add(hitCollider.gameObject);
                }
            }

            yield return new WaitForSeconds(0.2f); // 0.2초마다 검사
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("플레이어가 자석 아이템을 획득했습니다!");
                player.UseItem(this); // 플레이어가 자석 아이템을 소지하도록 설정

                // 아이템을 화면에서 보이지 않게 설정
                HideItemVisuals();

                // 자석 효과 발동
                Activate();
            }
        }
    }

    private void CollectCoin(GameObject coin)
    {
        // 코인 수집 처리
        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript != null)
        {
            coinScript.Collect(player); // 코인 수집을 Coin 클래스에서 처리
        }
    }

    private void HideItemVisuals()
    {
        // 자석 아이템의 Renderer와 Collider를 비활성화하여 화면에서 사라지게 함
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();

        if (spriteRenderer != null) spriteRenderer.enabled = false; // 시각적으로 비활성화
        if (collider != null) collider.enabled = false; // 충돌 비활성화
    }

    private void OnDrawGizmosSelected()
    {
        // 자석 범위를 시각적으로 확인하기 위한 기즈모
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
