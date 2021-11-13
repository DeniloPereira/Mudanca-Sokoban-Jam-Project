using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
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
        Vector2 newPos = new Vector2 (position.x, position.y) + direction;

        //Check if collide with some wall
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return false;
            }
        }

        //Check if collide with another box
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                return false;
            }
        }
        return true;
    }
}
