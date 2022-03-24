using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;

    // Update is called once per frame
    void Update()
    {
        this.MovePlayer();
        this.CheckBoundaries();
    }
    void MovePlayer()
    {
        float hMovement = Input.GetAxis("Horizontal");

        if (hMovement != 0)
            GetComponent<Rigidbody2D>().velocity = new Vector2(hMovement * speed, 0);
        else if (Input.GetMouseButton(0))
        {
            Vector2 relativePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

            if (relativePos.x < 0.5f)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            else
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }

    void CheckBoundaries()
    {
        float spriteSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float widthLimit = Camera.main.orthographicSize * Camera.main.aspect - spriteSize / 2;
        float posX = transform.position.x;
        float posY = transform.position.y;
        if (posX < -widthLimit)
            transform.position = new Vector3(-widthLimit, posY);
        else if (posX > widthLimit)
            transform.position = new Vector3(widthLimit, posY);
    }
}
