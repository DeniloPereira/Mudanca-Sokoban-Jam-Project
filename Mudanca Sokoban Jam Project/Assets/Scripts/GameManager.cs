using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text packageTextCount, movesNumberText;
    public GameObject gameOverScreen;
    private bool readyForInput; //Variable that allow move the player
    public Player player;
    private string stage = "nomex";
    public bool isGameOver = false;
    private int targetPoints, points = 0;


    //<string Scene Name, (int Max Moves, int Initial Packs, int Max Points)
    public Dictionary<string, (int, int, int)> levelMoves = new Dictionary<string, (int, int, int)>
    {
        {"nomex", (8, 0, 1) },
        {"nomey", (10, 1, 2) }
    };

    private void Start()
    {
        gameOverScreen.SetActive(false);
        Debug.Log(levelMoves[stage]);
        player.movesNumber = levelMoves[stage].Item1;
        player.packageNumber = levelMoves[stage].Item2;
        targetPoints = levelMoves[stage].Item3;
    }

    void Update()
    {
        //Set the counters in the UI texts
        packageTextCount.text = player.packageNumber.ToString();
        movesNumberText.text = player.movesNumber.ToString();

        if (!isGameOver)
        {
            //Set the place where the player want to go
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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
                isGameOver = true;
                gameOverScreen.SetActive(true);
                Time.timeScale = 0;
            }

        }
        //Check if the space is pressed
        if (Input.GetKeyDown("space"))
        {
            player.BoxTheFurniture();
        }

        //Check if the "r" key is pressed to restart the scene
        if (Input.GetKeyDown("r"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddPoint()
    {

        points++;
        Debug.Log("Faltam " + (targetPoints - points));
        if (points < targetPoints)
        {
            
        }
        else
        {
            Debug.Log("Passa de fase");
            SceneManager.LoadScene("cenay");
        }
    }
}
