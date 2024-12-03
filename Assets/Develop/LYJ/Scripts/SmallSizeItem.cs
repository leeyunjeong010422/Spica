using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSizeItem : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    public float sizeMultiplier = 2f; // ĳ���� ũ�⸦ �� ������
    public float effectDuration = 5f; // ȿ�� ���� �ð�

    public void Activate()
    {
        Debug.Log("���������� ������ Ȱ��ȭ");

        if (playerMove != null)
        {
            // ĳ���� ũ�� ����
            playerMove.transform.localScale /= sizeMultiplier;

            // ȿ�� ����
            playerMove.StartCoroutine(SizeEffectDuration());
        }
    }

    public void Deactivate()
    {
        Debug.Log("���������� ������ ��Ȱ��ȭ");

        if (playerMove != null)
        {
            // ĳ���� ũ�� ������� ����
            playerMove.transform.localScale *= sizeMultiplier;
        }
    }

    private IEnumerator SizeEffectDuration()
    {
        yield return new WaitForSeconds(effectDuration); // 5�� ���� ����
        Deactivate(); // ȿ�� ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMove = collision.GetComponent<PlayerMove>();
            Debug.Log("���������� ������ ȹ��");
            Activate();
            Destroy(gameObject); // ���������� ������ ����
        }
    }
}
