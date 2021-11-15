using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int packageNumber; //Number of packages
    public int movesNumber; //Number of movements
    private Animator playerAnimator;
    public LayerMask wallLayer, boxLayer, packageLayer, furnitureLayer;
    private int directionPlayerIsLooking = 0; //0 = down; 1 = up; 2 = left; 3 = right.
    public Vector2 lookingFor;

    public AudioSource src;
    public AudioClip packing_box;

    public void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
    }
    public bool Move(Vector2 direction)
    {
        lookingFor = direction;
        if(!PlayerDirection(direction))
        {
            return false;
        }
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
            movesNumber--;
            transform.Translate(direction);
            packageCollect(newPos);
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

    public void BoxTheFurniture()
    {
        Vector2 newPos = new Vector2 (transform.position.x, transform.position.y) + lookingFor/2;
        Collider2D collideChecker;
        if (collideChecker = Physics2D.OverlapCircle(newPos, .1f, boxLayer))
        {
            Box box;
            box = collideChecker.gameObject.GetComponent<Box>();

            box.changeTheSprite = true;
            collideChecker.gameObject.layer = 9;
            src.PlayOneShot(packing_box);
            Debug.Log("Desempacotou");
            packageNumber++;
            return;
        }

        if (collideChecker = Physics2D.OverlapCircle(newPos, .1f, furnitureLayer))
        {
            Box box;
            box = collideChecker.gameObject.GetComponent<Box>();

            if (packageNumber > 0)
            {
                box.changeTheSprite = true;
                collideChecker.gameObject.layer = 7;
                src.PlayOneShot(packing_box);
                Debug.Log("Empacotou");
                packageNumber--;
                return;
            }
            else
            {
                Debug.Log("Error");
                return;
            }
        }
    }

    bool PlayerDirection(Vector2 input)
    {
        if (input.x == 1)
        {
            if (directionPlayerIsLooking == 3)
            {
                return true;
            }
            else
            {
                playerAnimator.SetTrigger("isLookingRight");
                directionPlayerIsLooking = 3;
                return false;
            }
        }
        else if (input.x == -1)
        {
            if (directionPlayerIsLooking == 2)
            {
                return true;
            }
            else
            {
                playerAnimator.SetTrigger("isLookingLeft");
                directionPlayerIsLooking = 2;
                return false;
            }
        }

        if (input.y == 1)
        {
            if (directionPlayerIsLooking == 1)
            {
                return true;
            }
            else
            {
                playerAnimator.SetTrigger("isLookingUp");
                directionPlayerIsLooking = 1;
                return false;
            }
        }
        else if (input.y == -1)
        {
            if (directionPlayerIsLooking == 0)
            {
                return true;
            }
            else
            {
                playerAnimator.SetTrigger("isLookingDown");
                directionPlayerIsLooking = 0;
                return false;
            }
        }
        return false;
    }
}
