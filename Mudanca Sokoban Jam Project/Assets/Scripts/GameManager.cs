using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool readyForInput; //Variable that allow move the player
    public Player player;

    void Update()
    {
        //Set the place where the player want to go
        Vector2 moveInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Change the movement value to exactly 1, avoiding movement bugs
        moveInput.Normalize();

        //Move the character if check some input
        if(moveInput.sqrMagnitude > 0.5)
        {
            if (readyForInput)
            {
                readyForInput = false;
                player.Move(moveInput);
            }
        }
        else
        {
            readyForInput = true;
        }
    }
}
