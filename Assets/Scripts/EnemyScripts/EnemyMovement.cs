using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]protected float MovementSpeed;
    [SerializeField] public EnemyData myEnemyData;

    public virtual Vector3 Move(Vector3 aPlayerPos, Rigidbody2D rb2D)
    {
        return Vector3.zero;
    }
}

