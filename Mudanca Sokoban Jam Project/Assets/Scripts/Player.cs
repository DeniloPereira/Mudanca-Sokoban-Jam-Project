using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int packageNumber; //Number of packages
    public int movesNumber; //Number of movements
    public LayerMask wallLayer, boxLayer, packageLayer, furnitureLayer;
    public bool Move(Vector2 direction)
    {
        //Set the place where the player want to go
        Vector2 newPos = new Vector2 (transform.position.x, transform.position.y) + direction/2;

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
            packageCollect(newPos);
            movesNumber--;
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
        Vector2 newPos = new Vector2 (position.x, position.y) + direction/2;

        //Check if collide with the wall
        if (Physics2D.OverlapCircle(newPos, .1f, wallLayer))
        {
            return false;
        }

        //Check if collide with some furniture
        if (Physics2D.OverlapCircle(newPos, .1f, furnitureLayer))
        {
            return false;
        }

        //Check if collide with some box
        Collider2D collideChecker;
        if (collideChecker = Physics2D.OverlapCircle(newPos, .1f, boxLayer))
        {
            Box box;
            box = collideChecker.gameObject.GetComponent<Box>();
            if (box.Move(direction))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void packageCollect(Vector2 newPos)
    {
        Collider2D collideChecker;
        if (collideChecker = Physics2D.OverlapCircle(newPos, .1f, packageLayer))
        {
            GameObject pkge;
            pkge = collideChecker.gameObject;
            Debug.Log("+1 package");
            packageNumber++;
            Object.Destroy(pkge);
        }
    }
}
