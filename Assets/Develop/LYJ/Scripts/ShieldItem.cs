using System.Collections;
using UnityEngine;

public class ShieldItem : MonoBehaviour, IItem
{
    [SerializeField] Player player; // 플레이어 참조
    private bool isCollected = false; // 쉴드가 소지 중인지 여부
    private bool isActive = false;    // 쉴드가 활성화(무적 상태) 중인지 여부

    public void Activate()
    {
        // 장애물과 충돌 시 쉴드 효과 발동
        if (!isCollected || isActive || player == null || player.ShieldCount <= 0) return;

        isActive = true;
        Debug.Log("쉴드가 발동되었습니다! 플레이어는 5초 동안 무적입니다.");

        // 플레이어를 무적 상태로 변경
        player.gameObject.layer = LayerMask.NameToLayer("Invincible");

        // 5초 후 쉴드 비활성화
        player.StartCoroutine(ShieldDuration());

        player.DecreaseShieldCount(); 
    }

    public void Deactivate()
    {
        if (!isActive || player == null) return;

        isActive = false;
        Debug.Log("쉴드가 비활성화되었습니다! 플레이어는 더 이상 무적이 아닙니다.");

        // 플레이어 레이어 원래대로 복구
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(5f); // 5초 동안 무적 유지
        Deactivate(); // 쉴드 비활성화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌하면 쉴드 아이템을 소지 상태로 전환
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            if (player != null && player.ShieldCount == 0)
            {
                isCollected = true; // 쉴드 소지 상태로 변경
                Debug.Log("쉴드를 획득했습니다! 장애물에 부딪히면 자동으로 발동됩니다.");
                player.UseItem(this);
                Destroy(gameObject); // 아이템 오브젝트 제거

                player.IncreaseShieldCount();
            }
        }
    }
}
