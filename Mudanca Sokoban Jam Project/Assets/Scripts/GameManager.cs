using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool readyForInput; //Variable that allow move the player
    public Player player;
    private string fase = "nomey";
    public Dictionary<string, (int, int)> levelMoves = new Dictionary<string, (int, int)>
    {
        {"nomex", (8, 0) },
        {"nomey", (10, 1) }
    };

    private void Start()
    {
        Debug.Log(levelMoves[fase]);
        player.packCount = levelMoves[fase].Item2;

    }

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
