using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAndObjectsToClick : MonoBehaviour
{
    protected Player player;
    protected Transform playerTransform;
    protected Transform thisTransform;

    virtual protected void Start()
    {
        player = ReferencesToObjects.Player;
        playerTransform = player.transform;
        thisTransform = gameObject.transform;
    }

    protected virtual void OnMouseDown()
    {
        if (CheckPosition())
        {
            DestroyAnd();
        }
    }
    
    protected virtual void DestroyAnd()
    {
        Destroy(gameObject);
    }
    protected bool CheckPosition()
    {
        if ((Mathf.Abs(playerTransform.position.x - thisTransform.position.x) == 1)
            && (playerTransform.position.y - thisTransform.position.y == 0) ||
                (playerTransform.position.x - thisTransform.position.x == 0)
            && (Mathf.Abs(playerTransform.position.y - thisTransform.position.y) == 1))
            return true;
        else
            return false;
    }
}
