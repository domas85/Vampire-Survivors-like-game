using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave : EnemyMovement
{

    [SerializeField] float Frequency;
    [SerializeField] float Amplitude;
    //[SerializeField] private EnemyData enemyData;
    public override Vector3 Move(Vector3 aPlayerPostion, Rigidbody2D rb2D)
    {
        Vector3 returnVelocity = aPlayerPostion - transform.position;

        Vector3 Parallel = Vector3.Cross(returnVelocity, transform.forward);

        returnVelocity += Parallel * Mathf.Sin(Time.time * Frequency) * Amplitude;

        return returnVelocity.normalized * MovementSpeed;
    }
}
