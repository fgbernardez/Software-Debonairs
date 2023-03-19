using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public string _newGame;
    private string leveltoLoad;

    public void NewGameConfirmationYes()
    {
        SceneManager.LoadScene(1);
    }

    //    public string GoToNewGame(string newGame){
    public void GoToNewGame(string newGame){
    SceneManager.LoadScene(newGame);
   }
   public void ExitApp(){
    Application.Quit();
    Debug.Log("Exit Application Success!");
   }
}
