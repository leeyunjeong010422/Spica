using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void Collect(Player player)
    {
        // ���� ���� ���� ó��
        Debug.Log("���� �ȳ�~~!!~!~!~!~!~!~!");

        // TODO: ���� ���� ���� ���� �߰�
        player.AddCoin();

        // ���� ����
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            // ���� ���� ���� ó��
            Debug.Log("���� �ȳ�~~!!~!~!~!~!~!~!");

            // TODO: ���� ���� ���� ���� �߰�
            player.AddCoin();

            // ���� ����
            Destroy(gameObject);
        }
    }
}
