//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneMenu : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void OnStartBtnClick()
    {
        ScreenOverlayManager.Instance.FadeToBlack();

        Invoke("LoadGameScene", 2.0f);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
