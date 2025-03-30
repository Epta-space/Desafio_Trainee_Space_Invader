using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameOverMenu : MonoBehaviour
{
   public string nameLevelMenu;
   public string nameLevelReset;

   public void Reset()
   {
        SceneManager.LoadScene(nameLevelReset);
   }

   public void backMenu()
   {
        SceneManager.LoadScene(nameLevelMenu);
   }


}
