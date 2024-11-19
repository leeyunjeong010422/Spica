using System.Collections;
using UnityEngine;

public class ShieldItem : MonoBehaviour, IItem
{
    [SerializeField] Player player; // �÷��̾� ����
    private bool isCollected = false; // ���尡 ���� ������ ����
    private bool isActive = false;    // ���尡 Ȱ��ȭ(���� ����) ������ ����

    public void Activate()
    {
        // ��ֹ��� �浹 �� ���� ȿ�� �ߵ�
        if (!isCollected || isActive || player == null || player.ShieldCount <= 0) return;

        isActive = true;
        Debug.Log("���尡 �ߵ��Ǿ����ϴ�! �÷��̾�� 5�� ���� �����Դϴ�.");

        // �÷��̾ ���� ���·� ����
        player.gameObject.layer = LayerMask.NameToLayer("Invincible");

        // 5�� �� ���� ��Ȱ��ȭ
        player.StartCoroutine(ShieldDuration());

        player.DecreaseShieldCount(); 
    }

    public void Deactivate()
    {
        if (!isActive || player == null) return;

        isActive = false;
        Debug.Log("���尡 ��Ȱ��ȭ�Ǿ����ϴ�! �÷��̾�� �� �̻� ������ �ƴմϴ�.");

        // �÷��̾� ���̾� ������� ����
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(5f); // 5�� ���� ���� ����
        Deactivate(); // ���� ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹�ϸ� ���� �������� ���� ���·� ��ȯ
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            if (player != null && player.ShieldCount == 0)
            {
                isCollected = true; // ���� ���� ���·� ����
                Debug.Log("���带 ȹ���߽��ϴ�! ��ֹ��� �ε����� �ڵ����� �ߵ��˴ϴ�.");
                player.UseItem(this);
                Destroy(gameObject); // ������ ������Ʈ ����

                player.IncreaseShieldCount();
            }
        }
    }
}
