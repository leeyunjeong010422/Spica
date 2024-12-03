using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSizeItem : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    public float sizeMultiplier = 2f; // 캐릭터 크기를 몇 배할지
    public float effectDuration = 5f; // 효과 지속 시간

    public void Activate()
    {
        Debug.Log("스몰사이즈 아이템 활성화");

        if (playerMove != null)
        {
            // 캐릭터 크기 증가
            playerMove.transform.localScale /= sizeMultiplier;

            // 효과 지속
            playerMove.StartCoroutine(SizeEffectDuration());
        }
    }

    public void Deactivate()
    {
        Debug.Log("스몰사이즈 아이템 비활성화");

        if (playerMove != null)
        {
            // 캐릭터 크기 원래대로 복귀
            playerMove.transform.localScale *= sizeMultiplier;
        }
    }

    private IEnumerator SizeEffectDuration()
    {
        yield return new WaitForSeconds(effectDuration); // 5초 동안 지속
        Deactivate(); // 효과 비활성화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMove = collision.GetComponent<PlayerMove>();
            Debug.Log("스몰사이즈 아이템 획득");
            Activate();
            Destroy(gameObject); // 스몰사이즈 아이템 제거
        }
    }
}
