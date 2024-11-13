using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveInput, 0f);

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
