using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text packageTextCount, movesNumberText;
    public GameObject gameOverScreen;
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
        gameOverScreen.SetActive(false);
        Debug.Log(levelMoves[fase]);
        player.movesNumber = levelMoves[fase].Item1;
        player.packageNumber = levelMoves[fase].Item2;
    }

    void Update()
    {
        //Set the counters in the UI texts
        packageTextCount.text = player.packageNumber.ToString();
        movesNumberText.text = player.movesNumber.ToString();

        //Set the place where the player want to go
        Vector2 moveInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Change the movement value to exactly 1, avoiding movement bugs
        moveInput.Normalize();

        //Move the character if check some movement input
        if (moveInput.sqrMagnitude > 0.5)
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

        if (player.movesNumber == 0)
        {
            gameOverScreen.SetActive(true);
        }

        //Check if the space is pressed
        if (Input.GetKeyDown("space"))
        {
            player.BoxTheFurniture();
        }
    }
}
