//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnExitBtnClick()
    {
        if(BattleManager.instance.onExitBattle != null)
        {
            ScreenOverlayManager.Instance.FadeToBlack();

            BattleManager.instance.onExitBattle.Invoke();

            Invoke("ToDoOnExitBtnClick", 2.0f);
        }
    }

    void ToDoOnExitBtnClick()
    {
        gameObject.SetActive(false);

        ScreenOverlayManager.Instance.FadeFromBlack();
    }
}
