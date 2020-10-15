//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Saver : MonoBehaviour
{
    public static UnityEvent OnSave = new UnityEvent(); //It's like delegate
    public static UnityEvent OnLoad = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.K))
        //{
        //    Save();
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Load();
        //}
    }

    public void Save()
    {
        if (OnSave != null)
        {
            OnSave.Invoke();
        }

        //PlayerPrefs.SetString("PlayerPos", "10, 20, 10");

        PlayerPrefs.Save();
        Debug.Log("Saved");
    }

    public void Load()
    {
        if (OnLoad != null)
        {
            OnLoad.Invoke();
        }

        Debug.Log("Loaded");
    }
}

