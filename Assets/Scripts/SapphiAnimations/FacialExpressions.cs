using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressions : MonoBehaviour
{
    string eyes = "eyes";
    string mouth = "mouth";
    string smile = "smile";
    string happyL = "happyL";
    string happyR = "happyR";
    string closedL = "closedL";
    string closedR = "closedR";
    string mouthTopTeeth = "mouthTopTeeth";
    internal string _Animation = null;
    internal string _EyesChangeType = null;
    internal string _EyebrowsChangeType = null;
    internal string _MouthChangeType = null;
    internal string _GeneralChangeType = null;
    internal float _FacialValue = 0.0f;
    internal bool _FacialValueBool = false;

    //Facial Expressions

    //Happy ^o^
    void Happy() {
        //happy eyes
        _GeneralChangeType = eyes;
        _EyesChangeType = happyL;
        _FacialValue = 1 * 100;

        _EyesChangeType = happyR;
        _FacialValue = 1 * 100;

        _EyesChangeType = closedL;
        _FacialValue = 1 * 100;

        _EyesChangeType = closedR;
        _FacialValue = 1 * 100;

        //show teeth
        _GeneralChangeType = mouth;
        _MouthChangeType = mouthTopTeeth;
        _FacialValueBool = true;

        //smile
        _MouthChangeType = smile;
        _FacialValue = 1 * 100;
    }
    
    //

    // Start is called before the first frame update
    void Start()
    {
        Happy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
