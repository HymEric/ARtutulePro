using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    public Text showTip;
    bool saved = false;
    //public new AudioSource audio;
    public GameObject UICanvas;

    void Start()
    {
        ScreenshotManager.ScreenshotFinishedSaving += ScreenshotSaved;
    }
    public void TakeScreenShot()
    {
        //stateCanvas = State.Normal;
        UICanvas.SetActive(false);
        StartCoroutine(ScreenshotManager.Save("MyScreenshot", "AR华服绘本", true)); //保存到相册

        //audio.Play();
        StartCoroutine(WaitForOneSeconds());
    }

    void ScreenshotSaved()
    {
        Debug.Log("screenshot finished saving");
        saved = true;
    }
    IEnumerator WaitForOneSeconds()
    {
        yield return new WaitForSeconds(0.5f);

        UICanvas.SetActive(true);
        //if (saved)
        //{
            showTip.text = "图片保存成功";
        //}
        yield return new WaitForSeconds(1.5f);
        showTip.text = "";
    }
}