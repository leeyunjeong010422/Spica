using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IItem currentItem; // 현재 소지 중인 아이템 (쉴드 포함)
    public int ShieldCount { get; private set; } // 쉴드 아이템 소지 개수
    public int CoinCount { get; private set; }

    public void UseItem(IItem item)
    {
        currentItem = item;
        Debug.Log("아이템을 얻어버렸다!!!!!!");
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
        Debug.Log("쉴드 사용ㅠㅠ 현재 쉴드 개수: " + ShieldCount);
    }

    public void IncreaseShieldCount()
    {
        ShieldCount += 1; // 쉴드를 먹으면 카운트 증가
        Debug.Log("쉴드 획득ㅎㅎ 현재 쉴드 개수: " + ShieldCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 충돌 시 아이템 발동
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (ShieldCount > 0)
            {
                Debug.Log("쉴드 발동 얍얍~");
                ActivateItem();
            }
            else
            {
                Debug.Log("플레이어 사망ㅜㅜ");
                gameObject.SetActive(false);
            }
        }
    }

    public void AddCoin()
    {
        CoinCount++;
        Debug.Log($"코인 개수: {CoinCount}");
    }
}
