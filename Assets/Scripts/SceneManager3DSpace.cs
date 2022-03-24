using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager3DSpace : MonoBehaviour
{
    public Text infoText;
    public Player3DController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector2> touchPos = new List<Vector2>();
        
        infoText.text = "No input detected.";
        
        this.ManageRealTouches(touchPos);
        this.ManageMockTouches(touchPos);

        this.UpdateText(touchPos);
        this.ManagePowerUps(touchPos);
    }

    private void ManageRealTouches(List<Vector2> positions)
    {
        foreach (Touch touch in Input.touches)
            positions.Add(touch.position);
    }

    private void ManageMockTouches(List<Vector2> position)
    {
        if (Input.GetMouseButton(0))
            position.Add(Input.mousePosition);

        if (Input.GetKey(KeyCode.Space))
            position.Add(new Vector2(32, 32));

        if (Input.GetKey(KeyCode.V))
            position.Add(new Vector2(64, 64));
    }

    private void UpdateText(List<Vector2> touchPos)
    {
        if (touchPos.Count > 0)
        {
            infoText.text = "";
            for (int i = 0; i < touchPos.Count; i++)
            {
                infoText.text += string.Format("Input {0}: {1}, {2}\n", i + 1, touchPos[i].x, touchPos[i].y);
            }
        }
    }

    private void ManagePowerUps(List<Vector2> touchPos)
    {
        if (touchPos.Count == 2)
            player.ActivateSpeedUp();

        if (touchPos.Count == 3)
            player.ActivateInvincible();
    }

}
