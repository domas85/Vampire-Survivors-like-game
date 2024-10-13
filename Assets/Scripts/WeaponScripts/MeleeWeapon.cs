using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
 
    public Camera cam;
    public bool inverted;
    public enum Axis { x, y }
    public Axis axis = Axis.y;
    private Vector3 mousePosition;
    private Vector3 lookAtPosition;
    private Vector2 direction;
   

    [SerializeField] Transform weaponPivotPoint;
    [SerializeField] private bool rotateToMouse = false;

    private void Awake()
    {
        if (Camera.main != null)
        {
            cam = Camera.main;
            //This enables Main Camera
            cam.enabled = true;
        }
    }


    public override void UpdateWeapon()
    {
       
        if (cam == null && rotateToMouse)
        {
            Debug.LogError(gameObject.name + " target missing!");
            return;
        }
     


        if (rotateToMouse)
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = -cam.transform.position.z + weaponPivotPoint.transform.position.z;

            lookAtPosition = cam.ScreenToWorldPoint(mousePosition);

            direction = (lookAtPosition - weaponPivotPoint.transform.position).normalized;

            //Debug.DrawRay(weaponPivotPoint.transform.position, direction * 20f, Color.blue);
        }


        switch (axis)
        {
            case Axis.x:
                if (!inverted)
                    weaponPivotPoint.transform.right = direction; // Point x axis towards direction
                else
                    weaponPivotPoint.transform.right = -direction; // Point x axis towards inverted direction
                break;

            case Axis.y:
                if (!inverted)
                    weaponPivotPoint.transform.up = direction; // Point y axis towards direction
                else
                    weaponPivotPoint.transform.up = -direction; // Point y axis towards inverted direction
                break;

            default:
                break;
        }

    }

}
