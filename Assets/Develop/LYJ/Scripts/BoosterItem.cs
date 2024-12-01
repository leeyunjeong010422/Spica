using System.Collections;
using UnityEngine;

public class BoosterItem : MonoBehaviour, IItem
{
    [SerializeField] PlayerMove playerMove; // PlayerMove 참조
    public float speedMultiplier = 2f; // 부스터 속도를 몇 배할지 (배수)
    public float boostDuration = 5f; // 부스터 지속 시간

    public void Activate()
    {
        Debug.Log("부스터 활성화");

        if (playerMove != null)
        {
            // 속도 증가
            playerMove.SetSpeedMultiplier(speedMultiplier);

            // 플레이어를 무적 상태로 변경
            playerMove.gameObject.layer = LayerMask.NameToLayer("Invincible");

            // 부스터 실행
            playerMove.StartCoroutine(BoosterDuration());
        }
    }

    public void Deactivate()
    {
        Debug.Log("부스터 비활성화");

        if (playerMove != null)
        {
            // 속도 원래대로 복귀
            playerMove.SetSpeedMultiplier(1f);

            // 플레이어 레이어 원래대로 복구
            playerMove.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private IEnumerator BoosterDuration()
    {
        yield return new WaitForSeconds(boostDuration); // 5초 동안 무적 유지
        Deactivate(); // 부스터 비활성화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMove = collision.GetComponent<PlayerMove>();
            Debug.Log("부스터 획득");

            Activate();
            Destroy(gameObject); // 부스터 아이템 제거
        }

        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
