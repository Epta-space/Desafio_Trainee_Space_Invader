using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
   public string nameLevel;
   public GameObject painelMenu;
   public GameObject painelcredits;

   public void jogar()
   {
        SceneManager.LoadScene(nameLevel);
   }

   public void credits ()
   {
        painelMenu.SetActive(false);
        painelcredits.SetActive(true);
   }

   public void coloseCredits ()
   {
        painelcredits.SetActive(false);
        painelMenu.SetActive(true);
   }


   public void quit()
   {
        Application.Quit();
   }

}
