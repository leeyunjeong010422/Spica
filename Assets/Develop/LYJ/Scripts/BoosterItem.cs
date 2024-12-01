using System.Collections;
using UnityEngine;

public class BoosterItem : MonoBehaviour, IItem
{
    [SerializeField] PlayerMove playerMove; // PlayerMove ����
    public float speedMultiplier = 2f; // �ν��� �ӵ��� �� ������ (���)
    public float boostDuration = 5f; // �ν��� ���� �ð�

    public void Activate()
    {
        Debug.Log("�ν��� Ȱ��ȭ");

        if (playerMove != null)
        {
            // �ӵ� ����
            playerMove.SetSpeedMultiplier(speedMultiplier);

            // �÷��̾ ���� ���·� ����
            playerMove.gameObject.layer = LayerMask.NameToLayer("Invincible");

            // �ν��� ����
            playerMove.StartCoroutine(BoosterDuration());
        }
    }

    public void Deactivate()
    {
        Debug.Log("�ν��� ��Ȱ��ȭ");

        if (playerMove != null)
        {
            // �ӵ� ������� ����
            playerMove.SetSpeedMultiplier(1f);

            // �÷��̾� ���̾� ������� ����
            playerMove.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private IEnumerator BoosterDuration()
    {
        yield return new WaitForSeconds(boostDuration); // 5�� ���� ���� ����
        Deactivate(); // �ν��� ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMove = collision.GetComponent<PlayerMove>();
            Debug.Log("�ν��� ȹ��");

            Activate();
            Destroy(gameObject); // �ν��� ������ ����
        }

        if (collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
