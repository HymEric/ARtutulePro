using UnityEngine;
using System.Collections;


public class RotateandAmplify : MonoBehaviour
{

    private Touch oldTouch1;  //上次触摸点1(手指1)    
    private Touch oldTouch2;  //上次触摸点2(手指2)    

    void Start()
    {

    }

    void Update()
    {

        //没有触摸    
        if (Input.touchCount <= 0)
        {
            return;
        }

        // 一点触摸，水平上下旋
        if (1 == Input.touchCount)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;
            transform.Rotate(Vector3.down * deltaPos.x, Space.World);     //左右旋
            //	transform.Rotate(Vector3.right * deltaPos.y , Space.World);    //上下旋
        }

        //多点触摸，控制大小  
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        //   
        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        //   
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        //两个距离之差   
        float offset = newDistance - oldDistance;

        //放大系数  
        float scaleFactor = offset / 300f;
        Vector3 localScale = transform.localScale;
        Vector3 scale = new Vector3(localScale.x + scaleFactor,
            localScale.y + scaleFactor,
            localScale.z + scaleFactor);
        //最小放到 0.1 倍    
        if (scale.x > 0.1f && scale.y > 0.1f && scale.z > 0.1f)
        {
            //transform.localPosition = new Vector3(0, transform.localPosition.y + scaleFactor / 10f, 0);
            //Debug.Log(transform.position.y + scaleFactor / 10f);
            //transform.Translate(new Vector3(0, transform.position.y + scaleFactor/10f, 0));
            transform.localScale = scale;
        }

        //最新的触摸点，下次使用    
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;
    }

}
