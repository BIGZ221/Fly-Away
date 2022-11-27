using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Quit() {
        Application.Quit();
    }

}
