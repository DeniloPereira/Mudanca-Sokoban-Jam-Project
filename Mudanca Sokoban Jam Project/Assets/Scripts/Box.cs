using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private LayerMask wallLayer, boxLayer, furnitureLayer, targetLayer;
    public bool isBoxed, changeTheSprite;
    public GameManager gameManager;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    public Sprite furnitureSprite, boxSprite;

    public AudioSource src;
    public AudioClip box_move, error;

    public bool Move(Vector2 direction)
    {
        //Check if the box can be pushed
        if (CanBePushed(transform.position, direction))
        {
            transform.Translate(direction);
            src.PlayOneShot(box_move);
            return true;
        }
        else
        {
            src.PlayOneShot(error);
            return false;
        }
    }

    bool CanBePushed (Vector3 position, Vector2 direction)
    {
        //If isn't boxed, can't be pushed
        if (!isBoxed)
        {
            src.PlayOneShot(error);
            return false;
        }

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

        if (Physics2D.OverlapCircle(newPos, .01f, targetLayer))
        {
            spriteRenderer.material.color = new Color (0.6f, 0.5f, 0.3f, 1);
            Debug.Log("Color changed");
            gameManager.AddPoint();

            return true;
        }
        return true;
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        wallLayer = LayerMask.GetMask("Wall");
        boxLayer = LayerMask.GetMask("Box");
        furnitureLayer = LayerMask.GetMask("Furniture");
        targetLayer = LayerMask.GetMask("Target");
        //gameManager = gameObject
    }

    void Update()
    {
        if (changeTheSprite)
        {
            if (isBoxed)
            {
                spriteRenderer.sprite = furnitureSprite;
                isBoxed = false;
                changeTheSprite = false;
            }
            else
            {
                spriteRenderer.sprite = boxSprite;
                isBoxed = true;
                changeTheSprite = false;
            }
        }
    }
}
