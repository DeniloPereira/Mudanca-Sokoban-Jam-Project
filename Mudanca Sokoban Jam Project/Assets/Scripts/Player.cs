using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject[] packages;
    public int packCount;
    public void Start()
    {
        packages = GameObject.FindGameObjectsWithTag("Package");
    }
    public bool Move(Vector2 direction)
    {
        //Always set one of the coordinates to 0, avoiding diagonal movement
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }

        //Check if can move
        if (CanMove(transform.position, direction))
        {
            transform.Translate(direction);
            packageCollect();
            Debug.Log(packCount);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanMove (Vector3 position, Vector2 direction)
    {
        //Set the place where the player want to go
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

        //Check if collide with some box
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            //Check if can push this box
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void packageCollect()
    {
        foreach(var pack in packages)
        {
            if (pack.transform.position.x == this.transform.position.x &&
                pack.transform.position.y == this.transform.position.y)
            {
                Debug.Log("Pacotão pegadão");
                
                Object.Destroy(pack);
            }
        }
    }
}
