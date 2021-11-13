using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public LayerMask wallLayer, boxLayer, furnitureLayer, targetLayer;
    public bool Move(Vector2 direction)
    {
        //Check if the box can be pushed
        if (CanBePushed(transform.position, direction))
        {
            transform.Translate(direction);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanBePushed (Vector3 position, Vector2 direction)
    {
        //Set the place where the box is been pushed to
        Vector2 newPos = new Vector2 (transform.position.x, transform.position.y) + direction/1.9f;

        //Check if collide with a wall or another box
        if (Physics2D.OverlapCircle(newPos, .1f, wallLayer))
        {
            Debug.Log("Blocked by a wall");
            return false;
        }

        if (Physics2D.OverlapCircle(newPos, .01f, boxLayer))
        {
            Debug.Log("Blocked by a box");
            return false;
        }
        return true;
    }
}
