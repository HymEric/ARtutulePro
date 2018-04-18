using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEditor.Sprites;

public class MyTrackbleEventHandler : MonoBehaviour,ITrackableEventHandler {
    enum State { Normal, Conformed };
    State state = State.Normal;
    State RotateState = State.Normal;

    private TrackableBehaviour mTrackableBehaviour;
    public GameObject comformBtn;
    public GameObject quitBtn;
    public GameObject hintImage;
    public GameObject aimImage;
    public GameObject gameobject;
    public GameObject rotateBtn;

    public Sprite rotateSprite;
    public Sprite rotatePauseSprite;
    Button changeBtn;
   // UnityEngine.UI.Image myImage;
    //public GameObject empty;

    bool isFirstTime = true;

	// Use this for initialization
	void Start () {
       // myImage.sprite = Resources.Load("Image/pic", typeof(Sprite)) as Sprite;// Image/pic 在 Assets/Resources/目录下  
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
	}
	
	// Update is called once per frame
	void Update () {

		if(RotateState==State.Conformed)
            gameobject.GetComponent<Transform>().Rotate(Vector3.up * 0.5f);
            //this.transform.Rotate(Vector3.up * 0.3f);
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if(isFirstTime)
            {
                Debug.Log("Target found");

                switch (state)
                {
                    case State.Normal:
                        comformBtn.SetActive(true);
                        rotateBtn.SetActive(true);
                        hintImage.SetActive(false);
                        aimImage.SetActive(false);
                        break;
                    case State.Conformed:
                        showModel();
                        isFirstTime = false;
                        break;
                }  
            } 
            
        }
        else
        {
            if(isFirstTime)
            {
                Debug.Log("Target lost");
                comformBtn.SetActive(false);
                rotateBtn.SetActive(false);
                hintImage.SetActive(true);
                aimImage.SetActive(true);
                state = State.Normal;
                Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
                // Enable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = false;
                }
            }
            else if (!isFirstTime)
            {
                Debug.Log("Target lost second more");
                //comformBtn.SetActive(false);
                //rotateBtn.SetActive(false);
                //text.SetActive(true);
                //state = State.Normal;
                Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
                // Enable rendering:
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = true;
                }
                //empty = gameobject;
                //gameobject.SetActive(false);
              //  empty.SetActive(true);
            }
           
        }
    }

    public void showModel()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }
    }
    public void Comformed()
    {
        hintImage.SetActive(false);
        aimImage.SetActive(false);
        MyGetTexture.instance.GetTexture();
        showModel();
        //comformBtn.SetActive(false);
        //rotateBtn.SetActive(true);
        state = State.Conformed;
        isFirstTime = false;
    }

    // 重新开始
    public void reStart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       //Application.loadedLevel(Application.loadedLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnRotate()
    {
        //m_Transform.Rotate(Vector3.up, 100.0f);
       // m_Transform.Rotate(Vector3.up * 0.3f);
        changeBtn = rotateBtn.GetComponent<Button>();
        if(RotateState==State.Normal)
        {
            changeBtn.GetComponent<UnityEngine.UI.Image>().sprite = rotatePauseSprite;
            RotateState = State.Conformed;
        }
        
        else if (RotateState == State.Conformed)
        {
            changeBtn.GetComponent<UnityEngine.UI.Image>().sprite = rotateSprite;
            RotateState = State.Normal;
        }     
    }


}
