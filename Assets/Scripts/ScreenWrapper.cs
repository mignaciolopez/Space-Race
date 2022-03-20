using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    //bool isWrappingX = false;
    //bool isWrappingY = false;

    //Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        //renderers = GetComponentsInChildren<Renderer>();
    }

    void FixedUpdate()
    {
        ScreenWrap();
    }

    void ScreenWrap()
    {
        /*bool isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }*/

        //if (isWrappingX || isWrappingY)
            //return;

        Vector3 newPosition = transform.position;

        if (newPosition.x > 18f || newPosition.x < -18f)
        {
            newPosition.x = -newPosition.x;
            //isWrappingX = true;
        }

        if (newPosition.y > 10f) /* || newPosition.y < -10f)*/
        {
            newPosition.y = -newPosition.y;
            //isWrappingY = true;
            //Debug.Log($"{name} isWrappingY");
            GameManager.instance.UpdateScore(gameObject.tag);
        }

        transform.position = newPosition;
    }

    /*bool CheckRenderers()
    {
        foreach(Renderer renderer in renderers)
        {
            if (renderer.isVisible)
                return true;
        }

        return false;
    }*/
}
