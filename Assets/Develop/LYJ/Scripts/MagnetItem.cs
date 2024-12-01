using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour, IItem
{
    [SerializeField] private float magnetRadius = 5f; // �ڼ� ����
    [SerializeField] private float pullSpeed = 10f;   // ������ �������� �ӵ�
    [SerializeField] private float activeDuration = 5f; // �ڼ� ȿ�� ���� �ð�
    private Player player; // �÷��̾� ����
    private bool isActive = false; // �ڼ��� Ȱ��ȭ�Ǿ����� ����

    private List<GameObject> coinsInRange = new List<GameObject>(); // ���� ���� ���� ����

    private void Update()
    {
        if (isActive && player != null)
        {
            // ���� ���� ������ �������
            for (int i = 0; i < coinsInRange.Count; i++)
            {
                if (coinsInRange[i] != null)
                {
                    // ������ �÷��̾� ������ �̵�
                    coinsInRange[i].transform.position = Vector3.MoveTowards(
                        coinsInRange[i].transform.position,
                        player.transform.position,
                        pullSpeed * Time.deltaTime
                    );

                    // �÷��̾�� ������ ������ ����
                    if (Vector3.Distance(coinsInRange[i].transform.position, player.transform.position) < 0.1f)
                    {
                        CollectCoin(coinsInRange[i]);
                        coinsInRange.RemoveAt(i);
                        i--; // ����Ʈ���� ���� �� �ε��� ����
                    }
                }
            }
        }
    }

    // IItem �������̽��� Activate() ����: �ڼ� ȿ�� ����
    public void Activate()
    {
        if (isActive || player == null) return;

        isActive = true;
        Debug.Log("�ڼ� �������� Ȱ��ȭ�Ǿ����ϴ�!");

        // ���� �ֱ⸶�� ���� �� ���� �˻�
        StartCoroutine(CheckCoinsInRange());

        // �ڼ� ���� �ð��� ������ ��Ȱ��ȭ
        StartCoroutine(DeactivateAfterDuration());
    }

    // IItem �������̽��� Deactivate() ����: �ڼ� ȿ�� ����
    public void Deactivate()
    {
        if (!isActive) return;

        isActive = false;
        Debug.Log("�ڼ� �������� ��Ȱ��ȭ�Ǿ����ϴ�.");
        StopAllCoroutines(); // �ڷ�ƾ ����
        coinsInRange.Clear(); // ����Ʈ �ʱ�ȭ
        Destroy(gameObject);
    }

    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(activeDuration); // �ڼ� ȿ�� ���� �ð�
        Deactivate(); // �ڼ� ȿ�� ��Ȱ��ȭ
    }

    private IEnumerator CheckCoinsInRange()
    {
        while (isActive && player != null)
        {
            // �÷��̾� �ֺ��� ���� �� ���� ã��
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.transform.position, magnetRadius);

            coinsInRange.Clear();
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Coin"))
                {
                    coinsInRange.Add(hitCollider.gameObject);
                }
            }

            yield return new WaitForSeconds(0.2f); // 0.2�ʸ��� �˻�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("�÷��̾ �ڼ� �������� ȹ���߽��ϴ�!");
                player.UseItem(this); // �÷��̾ �ڼ� �������� �����ϵ��� ����

                // �������� ȭ�鿡�� ������ �ʰ� ����
                HideItemVisuals();

                // �ڼ� ȿ�� �ߵ�
                Activate();
            }
        }
    }

    private void CollectCoin(GameObject coin)
    {
        // ���� ���� ó��
        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript != null)
        {
            coinScript.Collect(player); // ���� ������ Coin Ŭ�������� ó��
        }
    }

    private void HideItemVisuals()
    {
        // �ڼ� �������� Renderer�� Collider�� ��Ȱ��ȭ�Ͽ� ȭ�鿡�� ������� ��
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();

        if (spriteRenderer != null) spriteRenderer.enabled = false; // �ð������� ��Ȱ��ȭ
        if (collider != null) collider.enabled = false; // �浹 ��Ȱ��ȭ
    }

    private void OnDrawGizmosSelected()
    {
        // �ڼ� ������ �ð������� Ȯ���ϱ� ���� �����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
