using UnityEngine;

public class LimitFrameRate : MonoBehaviour
{
    public int maxFrameRate = 400;

    //Sets vsync to dont sync and after that
    //limmits the max framerate to 400

    void Start()
    {
        Application.targetFrameRate = maxFrameRate;
        QualitySettings.vSyncCount = 0;
    }
}
