using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundScroll : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] SpriteRenderer backgroundSprite;


    [SerializeField] float backgroundLimitX = 2.5f;
    [SerializeField] float backgroundLimitY = 2.5f;
    Vector3 Center;
    float offsetX, offsetY;
    private void Start()
    {
        Center = transform.position;
        offsetX = backgroundSprite.size.y / 2 + backgroundLimitX;
        offsetY = backgroundSprite.size.y / 2 + backgroundLimitY;
    }

    public void UpdateBackground()
    {
        CheckIfPlayerNearBorder();
    }

    public void CheckIfPlayerNearBorder()
    {

        if (player.position.y >= Center.y + offsetY)
        {
            transform.position = new Vector3(transform.position.x, player.position.y);
            Center.y += offsetY;
        }
        if (player.position.y <= Center.y - offsetY)
        {
            transform.position = new Vector3(transform.position.x, player.position.y);
            Center.y -= offsetY;
        }
        if (player.position.x >= Center.x + offsetX)
        {
            transform.position = new Vector3(player.position.x, transform.position.y);
            Center.x += offsetX;
        }
        if (player.position.x <= Center.x - offsetX)
        {
            transform.position = new Vector3(player.position.x, transform.position.y);
            Center.x -= offsetX;
        }

    }

}
