using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void Collect(Player player)
    {
        // 코인 점수 증가 처리
        Debug.Log("코인 냠냠~~!!~!~!~!~!~!~!");

        // TODO: 코인 점수 증가 로직 추가
        player.AddCoin();

        // 코인 제거
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            // 코인 점수 증가 처리
            Debug.Log("코인 냠냠~~!!~!~!~!~!~!~!");

            // TODO: 코인 점수 증가 로직 추가
            player.AddCoin();

            // 코인 제거
            Destroy(gameObject);
        }
    }
}
