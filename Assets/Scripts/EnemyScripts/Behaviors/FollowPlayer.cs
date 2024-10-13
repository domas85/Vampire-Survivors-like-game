using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowPlayer : EnemyMovement
{
    private Vector3 moveDirection;

    public override Vector3 Move(Vector3 aPlayerPos, Rigidbody2D rb2D)
    {
        Vector3 Direction = (aPlayerPos - transform.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        rb2D.rotation = angle;
        moveDirection = Direction;

        return moveDirection * myEnemyData.enemyMoveSpeed;

        //this is without rb
        //return (aPlayerPos - transform.position).normalized * MovementSpeed;
    }
}
