using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IItem currentItem; // ���� ���� ���� ������ (���� ����)
    public int ShieldCount { get; private set; } // ���� ������ ���� ����
    public int CoinCount { get; private set; }

    public void UseItem(IItem item)
    {
        currentItem = item;
        Debug.Log("�������� �����ȴ�!!!!!!");
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
        Debug.Log("���� ���Ф� ���� ���� ����: " + ShieldCount);
    }

    public void IncreaseShieldCount()
    {
        ShieldCount += 1; // ���带 ������ ī��Ʈ ����
        Debug.Log("���� ȹ�椾�� ���� ���� ����: " + ShieldCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ��� �浹 �� ������ �ߵ�
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (ShieldCount > 0)
            {
                Debug.Log("���� �ߵ� ���~");
                ActivateItem();
            }
            else
            {
                Debug.Log("�÷��̾� ����̤�");
                gameObject.SetActive(false);
            }
        }
    }

    public void AddCoin()
    {
        CoinCount++;
        Debug.Log($"���� ����: {CoinCount}");
    }
}
