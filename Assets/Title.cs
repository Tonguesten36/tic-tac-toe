using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // Load the game
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // Quit the game
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
