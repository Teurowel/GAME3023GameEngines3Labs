using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenOverlayManager : MonoBehaviour
{
    Animator animator = null;

    private ScreenOverlayManager() { }
    private static ScreenOverlayManager instance;
    public static ScreenOverlayManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScreenOverlayManager>();
            }

            return instance;
        }

        private set { }

    }

    // Start is called before the first frame update
    void Start()
    {
        ScreenOverlayManager[] screenOverlayManagers = FindObjectsOfType<ScreenOverlayManager>();
        foreach (ScreenOverlayManager mgr in screenOverlayManagers)
        {
            if (mgr != Instance)
            {
                Destroy(mgr.gameObject);
            }
        }

        DontDestroyOnLoad(transform.root);
        animator = GetComponent<Animator>();
        SceneManager.sceneLoaded += OnEnterNewScene;

    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void FadeToBlack()
    {
        animator.Play("FadeToBlack");
    }

    public void FadeFromBlack()
    {
        animator.Play("FadeFromBlack");
    }

    void OnEnterNewScene(Scene newScene, LoadSceneMode mode)
    {
        animator.Play("FadeFromBlack");

        if(newScene.name == "GameScene")
        {
            //BattleManager.instance.onEnterBattle += OnEnterBattleCallback;
        }
    }

    public void OnEnterBattleCallback()
    {
        StartCoroutine(FadeToBlackAndFromBlackCoroutine());
    }

    IEnumerator FadeToBlackAndFromBlackCoroutine()
    {
        FadeToBlack();

        yield return new WaitForSeconds(2.0f);

        FadeFromBlack();
    }
}
