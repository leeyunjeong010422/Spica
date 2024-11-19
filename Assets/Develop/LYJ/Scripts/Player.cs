using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IItem currentItem; // 현재 소지 중인 아이템 (쉴드 포함)
    public int ShieldCount { get; private set; } // 쉴드 아이템 소지 개수

    public void UseItem(IItem item)
    {
        if (currentItem != null)
        {
            Debug.Log("이미 아이템을 소지 중입니다!");
            return; // 이미 아이템을 소지 중이라면 무시
        }

        currentItem = item;
        Debug.Log("아이템을 소지했습니다!");
    }

    public void ActivateItem()
    {
        if (currentItem != null && ShieldCount > 0)
        {
            currentItem.Activate(); // 아이템 효과 발동
        }
    }

    public void ClearItem()
    {
        if (currentItem != null)
        {
            currentItem.Deactivate(); // 아이템 효과 종료
            currentItem = null;       // 현재 아이템 제거
        }
    }

    public void DecreaseShieldCount()
    {
        ShieldCount = Mathf.Max(0, ShieldCount - 1); // 쉴드 사용 후 0보다 작지 않게 감소
        Debug.Log("현재 쉴드 개수: " + ShieldCount);
    }

    public void IncreaseShieldCount()
    {
        ShieldCount += 1; // 쉴드를 먹으면 카운트 증가
        Debug.Log("쉴드를 추가로 획득했습니다. 현재 쉴드 개수: " + ShieldCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 충돌 시 아이템 발동
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("플레이어가 장애물과 충돌했습니다! 쉴드가 발동됩니다.");
            ActivateItem(); // 소지 중인 아이템 발동
        }
    }
}
