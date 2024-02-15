using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
// How Tic-Tac-Toe works
// When the game begins, the board should be empty (all will be filled with 0s)
    // X will begin first, then O
    // When its a player's turn, they get to pick an empty slot and mark that slot with their associated symbol
        // after that, they pass their turn to other player
    // The game will keep going until one of these conditions are met
    // A player who succeeds in placing three of their marks in a horizontal, vertical, or diagonal row is the winner
        // Horizontal: {1,1,1}{0,0,0}{0,0,0} or {0,0,0}{1,1,1}{0,0,0} or {0,0,0}{0,0,0}{1,1,1}
        // Vertical: {1,0,0}{1,0,0}{1,0,0} or {0,1,0}{0,1,0}{0,1,0} or {0,0,1}{0,0,1}{0,0,1}
        // Diagonal: {1,0,0}{0,1,0}{0,0,1} or {0,0,1}{0,1,0}{1,0,0}
    // If the board is full then its a draw


public class GameController : MonoBehaviour
{
    // a 3x3 array as the board
        // 0: empty
        // 1: X
        // 2: O
    private int[,] board;
    
    //is it X's turn?
    private bool isXTurn;

    private bool didXWin = false;
    private bool didOWin = false;
    [SerializeField] private Sprite spriteO;
    [SerializeField] private Sprite spriteX;
    [SerializeField] private Sprite spriteDefault;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private TMP_Text rematch;
    [SerializeField] private Image turnImage;

    // Start is called before the first frame update
    void Start()
    {
        // the board will be empty at first
        board = new int[3,3]{{0,0,0},{0,0,0},{0,0,0}};
        
        // by convention, X will begin first
        isXTurn = true;
        turnImage.sprite = spriteX;

        didXWin = false;
        didOWin = false;

        winnerText.text = "";
        rematch.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // For everytime a turn is over, the game will check the board for victory condition
        VictoryCheck();
        if(didOWin || didXWin)
        {
            rematch.text = "\'E\' to rematch \n or \n\'Q\' to quit";
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(1);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }
    bool isEmpty(int row, int column)
    {
        return board[row,column] == 0;
    }

    //This function checks the current condition for a horizontal, vertical, or diagonal victory
    //If the condition is met, send a signal to end the game
    void VictoryCheck()
    {
        //Horizontal victory
        // {1,1,1}{0,0,0}{0,0,0} or {0,0,0}{1,1,1}{0,0,0} or {0,0,0}{0,0,0}{1,1,1} => X wins
        // Same for O but replace all 1s with 2s
        for(int row = 0; row < 3; row++)
        {
            if(board[row,0] == 1 && board[row,1] == 1 && board[row,2] == 1)
            {
                Debug.Log("Horizontal victory");
                didXWin = true;
                winnerText.text = "X wins";
            }
            if(board[row,0] == 2 && board[row,1] == 2 && board[row,2] == 2)
            {
                Debug.Log("Horizontal victory");
                didOWin = true;
                winnerText.text = "O wins";
            }
        }

        //Vertical victory
        for(int column = 0; column < 3; column++)
        {
            if(board[0, column] == 1 && board[1, column] == 1 && board[2, column] == 1)
            {
                Debug.Log("Vertical victory");
                didXWin = true;
                winnerText.text = "X wins";
            }
            if(board[0, column] == 2 && board[1, column] == 2 && board[2, column] == 2)
            {
                Debug.Log("Vertical victory");
                didOWin = true;
                winnerText.text = "O wins";
            }
        }

        //Diagonal victory
        // Diagonal: {1,0,0}{0,1,0}{0,0,1} or {0,0,1}{0,1,0}{1,0,0}
        if((board[0,0] == 1 && board[1,1] == 1 && board[2,2] == 1) || (board[0,2] == 1 && board[1,1] == 1 && board[2,0] == 1))
        {
            Debug.Log("Diagonal victory");
            didXWin = true;
            winnerText.text = "X wins";
        }
        if(board[0,0] == 2 && board[1,1] == 2 && board[2,2] == 2 || (board[0,2] == 2 && board[1,1] == 2 && board[2,0] == 2))
        {
            Debug.Log("Diagonal victory");
            didOWin = true;
            winnerText.text = "O wins";
        }
    }

    public void Clicked(string rowAndColumn)
    {
        string[] split = rowAndColumn.Split(",");
        int row =  System.Int32.Parse(split[0]);
        int column = System.Int32.Parse(split[1]);

        //if the slot is empty and neither player had won yet
        if(isEmpty(row, column) && (!didXWin && !didOWin))
        {
            switch(isXTurn)
            {
                case true:
                    board[row, column] = 1;
                    turnImage.sprite = spriteO;
                    isXTurn = false;
                    Debug.Log("Clicked X");
                    break;
                default:
                    board[row, column] = 2;
                    turnImage.sprite = spriteX;
                    isXTurn = true;
                    Debug.Log("Clicked O");
                    break;
            }
            Debug.Log("Row: " + row + " | Column: " + column);
        }
    }
    public void Change(Button button)
    {
        if(!didOWin && !didXWin)
        {
            switch(isXTurn)
            {
                case true:
                    button.targetGraphic.GetComponent<Image>().sprite = spriteX;
                    break;
                default:
                    button.targetGraphic.GetComponent<Image>().sprite = spriteO;
                    break;
            }
        }
    }
}
