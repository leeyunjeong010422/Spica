using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IItem currentItem; // ���� ���� ���� ������ (���� ����)
    public int ShieldCount { get; private set; } // ���� ������ ���� ����

    public void UseItem(IItem item)
    {
        if (currentItem != null)
        {
            Debug.Log("�̹� �������� ���� ���Դϴ�!");
            return; // �̹� �������� ���� ���̶�� ����
        }

        currentItem = item;
        Debug.Log("�������� �����߽��ϴ�!");
    }

    public void ActivateItem()
    {
        if (currentItem != null && ShieldCount > 0)
        {
            currentItem.Activate(); // ������ ȿ�� �ߵ�
        }
    }

    public void ClearItem()
    {
        if (currentItem != null)
        {
            currentItem.Deactivate(); // ������ ȿ�� ����
            currentItem = null;       // ���� ������ ����
        }
    }

    public void DecreaseShieldCount()
    {
        ShieldCount = Mathf.Max(0, ShieldCount - 1); // ���� ��� �� 0���� ���� �ʰ� ����
        Debug.Log("���� ���� ����: " + ShieldCount);
    }

    public void IncreaseShieldCount()
    {
        ShieldCount += 1; // ���带 ������ ī��Ʈ ����
        Debug.Log("���带 �߰��� ȹ���߽��ϴ�. ���� ���� ����: " + ShieldCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ��� �浹 �� ������ �ߵ�
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("�÷��̾ ��ֹ��� �浹�߽��ϴ�! ���尡 �ߵ��˴ϴ�.");
            ActivateItem(); // ���� ���� ������ �ߵ�
        }
    }
}
