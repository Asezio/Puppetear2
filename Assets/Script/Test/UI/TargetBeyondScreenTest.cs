using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBeyondScreenTest : MonoBehaviour
{
    private GameObject go;//Object
    private GameObject targetGo;//Target object
    public GameObject outsideMarkpf;
    private GameObject markCanvas;
    private Image labelUI;//Mark UI

    private void Awake()
    {
        go = GameObject.FindGameObjectWithTag("Player");
        targetGo = this.gameObject;
        labelUI = Instantiate(outsideMarkpf, null).GetComponent<Image>();
        markCanvas = GameObject.Find("OutsideMarkCanvas");
        labelUI.gameObject.transform.parent = markCanvas.transform;
    }

    private void Update()
    {

            if (IsInView(targetGo.transform.position))
            {
                labelUI.enabled = false;
            }
            else
            {
                labelUI.enabled = true;
            }
        


        Vector2 v3 = Vector2.zero;
        Vector2 v4 = Vector2.zero;
        Vector2 v1 = Camera.main.WorldToScreenPoint(go.transform.position);
        Vector2 v2 = Camera.main.WorldToScreenPoint(targetGo.transform.position);
        Vector2 offset = v2 - v1;
        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            if (offset.x > 0)
            {
                v3 = new Vector2(Screen.width, 0);
                v4 = new Vector2(Screen.width, Screen.height);
            }
            else
            {
                v3 = new Vector2(0, 0);
                v4 = new Vector2(0, Screen.height);
            }
        }
        else
        {
            if (offset.y > 0)
            {
                v3 = new Vector2(0, Screen.height);
                v4 = new Vector2(Screen.width, Screen.height);
            }
            else
            {
                v3 = new Vector2(0, 0);
                v4 = new Vector2(Screen.width, 0);
            }
        }

        labelUI.transform.position = CalculateCrossPoint(go.transform.position, targetGo.transform.position, v3, v4);
    }

    /// <summary>
    /// Given four points, find the intersection of two line segments (y=a1x+b1, y=a2x+b2)
    /// </summary>
    private Vector2 CalculateCrossPoint(Vector3 v1, Vector3 v2, Vector2 v3, Vector2 v4)
    {
        float a1 = 0, b1 = 0, a2 = 0, b2 = 0;
        Vector2 crossPoint = Vector2.zero;
        v1 = Camera.main.WorldToScreenPoint(v1);
        v2 = Camera.main.WorldToScreenPoint(v2);

        if (v1.x != v2.x)
        {
            a1 = (v1.y - v2.y) / (v1.x - v2.x);
        }
        if (v1.y != v2.y)
        {
            b1 = v1.y - v1.x * (v1.y - v2.y) / (v1.x - v2.x);
        }

        if (v3.x != v4.x)
        {
            a2 = (v3.y - v4.y) / (v3.x - v4.x);
        }
        if (v3.y != v4.y)
        {
            b2 = v3.y - v3.x * (v3.y - v4.y) / (v3.x - v4.x);
        }

        if (a1 == a2 && b1 == b2)
        {
            Debug.LogWarning("The two lines are collinear and there is no intersection");
            return Vector2.zero;
        }
        else if (a1 == a2)
        {
            Debug.LogWarning("The two lines are parallel and there is no intersection");
            return Vector2.zero;
        }
        else
        {
            float x = 0;
            float y = 0;
            if (v3.x == v4.x)
            {
                if (v3.x == 0)
                {
                    x = 0;
                    y = b1;
                }
                else
                {
                    x = v3.x;
                    y = x * a1 + b1;
                }
            }
            else if (v3.y == v4.y)
            {
                if (v3.y == 0)
                {
                    y = 0;
                    x = -b1 / a1;
                }
                else
                {
                    y = v3.y;
                    x = (y - b1) / a1;
                }
            }
            else
            {
                x = (b2 - b1) / (a1 - a2);
                y = a1 * (b2 - b1) / (a1 - a2) + b1;
            }

            x = Mathf.Clamp(x, 0, Screen.width);
            y = Mathf.Clamp(y, 0, Screen.height);
            crossPoint = new Vector2(x, y);
            return crossPoint;
        }
    }

    /// <summary>
    /// Is it within the field of view of the specified camera
    /// </summary>
    /// <param name="worldPos">Object world coordinates</param>
    /// <returns>Whether in the camera's field of view</returns>
    public bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        //Determine whether the object is in front of the camera  
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
