using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PickSword : Interactable
{
    private bool hasTakenSword = false;
    private Light spotlight;
    public Transform spawnPoint;
    public GameObject swordPrefab;
    private AudioSource source;
    public AudioClip swordPull;
    public Image tooltip;
    private Animation tooltipAnimation;
    public AnimationClip moveIn;
    public AnimationClip moveOut;
    private string swordRotPivot;
    private string swordName;
    private string emptyString;
    private string swingSwordTip;
    public Text textObject;

    void Start()
    {
        swordName = "PlayerSword";
        swordRotPivot = "SwordRotationPivot";
        hint = "Take Sword";
        swingSwordTip = "Left click to swing sword";
        emptyString = "";
        spotlight = transform.parent.GetComponentInChildren<Light>();
        source = GetComponent<AudioSource>();
        source.clip = swordPull;
        tooltipAnimation = tooltip.GetComponent<Animation>();
    }

    void Update()
    {
        if (hasTakenSword)
        {
            spotlight.intensity = spotlight.intensity - 0.5f;
            if (spotlight.intensity <= 0)
            {
                spotlight.enabled = false;
            }
        }
    }

    IEnumerator ToolTipMoveOut()
    {
        yield return new WaitForSeconds(7f);
        tooltipAnimation.clip = moveOut;
        tooltipAnimation.Play();
    }

    protected override void Action()
    {
        if (hasTakenSword)
            return;

        // Destroy pivot point
        transform.parent.Find(swordRotPivot).gameObject.SetActive(false);

        // instantiate sword in player hand
        GameObject swordInstance = Instantiate(swordPrefab, spawnPoint);
        swordInstance.name = swordName;

        // setting flags
        hasTakenSword = true;
        textObject.text = swingSwordTip;
        swordInstance.GetComponent<Sword>().SetIsPickedUp(true);
        source.Play();
        tooltipAnimation.clip = moveIn;
        tooltipAnimation.Play();
        StartCoroutine(ToolTipMoveOut());
        hint = emptyString;
    }
}
