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
        yield return new WaitForSeconds(7f);
        tooltipAnimation.clip = moveOut;
        tooltipAnimation.Play();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == lavaTipFloorName){
            textObject.text = lavaTip;
            tooltipAnimation.clip = moveIn;
            tooltipAnimation.Play();
            StartCoroutine(ToolTipMoveOut());
        }
    }
}
