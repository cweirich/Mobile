using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DController : MonoBehaviour
{
    public float speed = 2;
    public float invincibleDuration;
    public float speedUpDuration;

    private bool usedInvincible;
    private bool usedSpeedUp;

    private float invincibleTimer;
    private float speedUpTimer;

    // Update is called once per frame
    void Update()
    {
        this.MovePlayer();
        this.CheckBoundaries();

        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            transform.localScale = Vector3.one;
            usedInvincible = false;
        }
    }
    private void MovePlayer()
    {
        float hMovement = Input.GetAxis("Horizontal");
        float moveSpeed = 0;

        if (speedUpTimer > 0)
        {
            speedUpTimer -= Time.deltaTime;
            moveSpeed = speed * 2;
        }
        else
        {
            moveSpeed = speed;
            usedSpeedUp = false;
        }

        if (hMovement != 0)
            GetComponent<Rigidbody>().velocity = new Vector2(hMovement * moveSpeed, 0);
        else if (Input.GetMouseButton(0))
        {
            Vector2 relativePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

            if (relativePos.x < 0.5f)
                GetComponent<Rigidbody>().velocity = new Vector2(-moveSpeed, 0);
            else
                GetComponent<Rigidbody>().velocity = new Vector2(moveSpeed, 0);
        }
        else
            GetComponent<Rigidbody>().velocity = new Vector2(0, 0);

    }

    private void CheckBoundaries()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        float cosX = Mathf.Cos(Camera.main.transform.localEulerAngles.x * Mathf.Deg2Rad);

        float zDistance = Camera.main.transform.position.z - zPos;

        float leftLimit = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -zDistance / cosX)).x;
        float rightLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, -zDistance / cosX)).x;

        if (xPos < leftLimit)
            transform.position = new Vector3(leftLimit, yPos, zPos);
        else if (xPos > rightLimit)
            transform.position = new Vector3(rightLimit, yPos, zPos);
    }

    public void ActivateInvincible()
    {
        if (usedInvincible)
            return;

        usedInvincible = true;

        invincibleTimer = invincibleDuration;

    }

    public void ActivateSpeedUp()
    {
        if (usedSpeedUp)
            return;

        usedSpeedUp = true;

        speedUpTimer = speedUpDuration;
    }
}
