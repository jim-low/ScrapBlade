using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LavaTip : MonoBehaviour
{
    [Header("Tips")]
    public string lavaTip;
    public string lavaTipFloorName;

    Text tooltipText;

    [Header("References")]
    public Image tooltip;
    private Animation tooltipAnimation;
    public AnimationClip moveIn;
    public AnimationClip moveOut;
    public Text textObject;

    // Start is called before the first frame update
    void Start()
    {
        tooltipAnimation = tooltip.GetComponent<Animation>();
    }

    IEnumerator ToolTipMoveOut()
    {
        yield return new WaitForSeconds(7f);                            //waits before moving out
        tooltipAnimation.clip = moveOut;
        tooltipAnimation.Play();
    }

    void OnCollisionEnter(Collision collision)                      
    {
        if (collision.gameObject.name == lavaTipFloorName){             //when collides when the right floor
            textObject.text = lavaTip;                                  //changes the tooltip text 
            tooltipAnimation.clip = moveIn;
            tooltipAnimation.Play();                                    //moves the tooltip in
            StartCoroutine(ToolTipMoveOut());                           //moves the tooltip out
        }
    }
}
