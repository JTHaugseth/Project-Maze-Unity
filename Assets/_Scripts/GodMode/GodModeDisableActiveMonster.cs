using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModeDisableActiveMonster : MonoBehaviour
{
    public GameObject phase1Enemy;
    public GameObject crawlerMain;
    public GameObject crawlerSecondary1;
    public GameObject crawlerSecondary2;
    public GameObject crawlerSecondary3;
    public GameObject phase4Enemy;  

    void Update()
    {
        //Deactivate monsters
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            phase1Enemy.SetActive(false);
            crawlerMain.SetActive(false);
            crawlerSecondary1.SetActive(false);
            crawlerSecondary2.SetActive(false);
            crawlerSecondary3.SetActive(false);
            phase4Enemy.SetActive(false);
        }
    }
}
