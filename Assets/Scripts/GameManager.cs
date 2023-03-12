using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool HasEnded = false;
    public float restartDelay = 3f;


    public void EndGame()
    {
        if(HasEnded == false)
        {
            HasEnded = true;
            Invoke("Restart",restartDelay);
        }
        
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
