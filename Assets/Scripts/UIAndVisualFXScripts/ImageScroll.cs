using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScroll : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float _x, _y;
    

    private void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, img.uvRect.size);

    }


}
