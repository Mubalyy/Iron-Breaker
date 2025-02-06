using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void openLevel(int id){
        string level = "Level" +id;
        SceneManager.LoadScene(level);
    }

    public void levelScene(){
        SceneManager.LoadScene("Levels");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Control()
    {
        SceneManager.LoadScene("Controls");
    }

   public void QuitGame()
   {
    Application.Quit();
   }
  
}
