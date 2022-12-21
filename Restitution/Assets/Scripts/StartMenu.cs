using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
   public Button playGame;

    // Start is called before the first frame update
    void Start()
    {
         playGame.onClick.AddListener( delegate { ProcessButtonInput(); });
    }

    
    private void ProcessButtonInput() {
        var parameters = new LoadSceneParameters(LoadSceneMode.Single);
        SceneManager.LoadScene("_LEVEL1_", parameters);
    }
}
