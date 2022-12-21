using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private Animator _SapphiArtChanAnimator;                //Character Animation
    internal string _SapphiArtChanAnimation = null;         //Character Animation Name
    //private AnimationManagerUI _AnimationManagerUI;         //Character Animation UI Connection
    private string _SapphiArtChanLastAnimation = null;      //Character Last Animation

    private SkinnedMeshRenderer _SapphiArtChanRenderer_Face;        //Character Skin Mesh Renderer for Face
    private SkinnedMeshRenderer _SapphiArtChanRenderer_Brow;        //Character Skin Mesh Renderer for Eyebrows
    private SkinnedMeshRenderer _SapphiArtChanRenderer_BottomTeeth;        //Character Skin Mesh Renderer for Bottom Teeth
    private SkinnedMeshRenderer _SapphiArtChanRenderer_Tongue;        //Character Tongue Skinned Mesh Renderer
    private SkinnedMeshRenderer _SapphiArtChanRenderer_TopTeeth;      //Character Top Teeth Skinned Mesh Renderer

    private string _EyesLastChangeType = null;      // Character Last Facial Type Update (Eyes)
    private string _EyebrowsLastChangeType = null;  // Character Last Facial Type Update (Eyebrows)
    private string _MouthLastChangeType = null;     // Character Last Facial Type Update (Mouth)

    private float _SapphiArtChanFacial = 0.0f;    //Character Facial Parameter
    private float _SapphiArtChanLastFacial = 0.0f;    //Character Last Facial Parameter
    private bool _SapphiArtChanFacialBool = false;    //Character Facial Parameter Bool
    private bool _SapphiArtChanLastFacialBool = false;    //Character Last Facial Parameter Bool
    //BlendShapeValues
    private float _SapphiArtChanFacial_Eye_L_Happy = 0.0f;
    private float _SapphiArtChanFacial_Eye_R_Happy = 0.0f;
    private float _SapphiArtChanFacial_Eye_L_Closed = 0.0f;
    private float _SapphiArtChanFacial_Eye_R_Closed = 0.0f;
    private float _SapphiArtChanFacial_Eye_L_Wide = 0.0f;
    private float _SapphiArtChanFacial_Eye_R_Wide = 0.0f;

    private float _SapphiArtChanFacial_Mouth_Sad = 0.0f;
    private float _SapphiArtChanFacial_Mouth_Puff = 0.0f;
    private float _SapphiArtChanFacial_Mouth_Smile = 0.0f;

    private float _SapphiArtChanFacial_Eyebrow_L_Up = 0.0f;
    private float _SapphiArtChanFacial_Eyebrow_R_Up = 0.0f;
    private float _SapphiArtChanFacial_Eyebrow_L_Angry = 0.0f;
    private float _SapphiArtChanFacial_Eyebrow_R_Angry = 0.0f;
    private float _SapphiArtChanFacial_Eyebrow_L_Sad = 0.0f;
    private float _SapphiArtChanFacial_Eyebrow_R_Sad = 0.0f;

    private float _SapphiArtChanFacial_Mouth_E = 0.0f;
    private float _SapphiArtChanFacial_Mouth_O = 0.0f;
    private float _SapphiArtChanFacial_Mouth_JawOpen = 0.0f;
    private float _SapphiArtChanFacial_Mouth_Extra01 = 0.0f;
    private float _SapphiArtChanFacial_Mouth_Extra02 = 0.0f;
    private float _SapphiArtChanFacial_Mouth_Extra03 = 0.0f;
    private float _SapphiArtChanFacial_Mouth_BottomTeeth = 0.0f;

    private bool _SapphiArtChanFacial_Mouth_TopTeeth = false;
    private bool _SapphiArtChanFacial_Mouth_Tongue = false;

    //Animation variables
    string idle = "idle";
    string walk = "walk";
    string running = "running";
    string jump = "jump";
    string winpose = "winpose";
    string ko_big = "ko_big";
    string damage = "damage";
    string hit01 = "hit01";
    string hit02 = "hit02";
    string hit03 = "hit03";
    string param_toidle = "param_toidle";
    string param_idletowalk = "param_idletowalk";
    string param_idletorunning = "param_idletorunning";
    string param_idletojump = "param_idletojump";
    string param_idletowinpose = "param_idletowinpose";
    string param_idletoko_big = "param_idletoko_big";
    string param_idletodamage = "param_idletodamage";
    string param_idletohit01 = "param_idletohit01";
    string param_idletohit02 = "param_idletohit02";
    string param_idletohit03 = "param_idletohit03";

    //Facial Variables
    string eyes = "eyes";
    string eyebrows = "eyebrows";
    string face = "face";
    string brow = "brow";
    string BottomTeeth = "BottomTeeth";
    string tongue = "tongue";
    string TopTeeth = "TopTeeth";
    string mouth = "mouth";
    string happyL = "happyL";
    string happyR = "happyR";
    string closedL = "closedL";
    string closedR = "closedR";
    string wideL = "wideL";
    string wideR = "wideR";
    string upL = "upL";
    string upR = "upR";
    string angerL = "angerL";
    string angerR = "angerR";
    string sadL = "sadL";
    string sadR = "sadR";
    string mouthE = "mouthE";
    string mouthO = "mouthO";
    string mouthJawOpen = "mouthJawOpen";
    string mouthExtra01 = "mouthExtra01";
    string mouthExtra02 = "mouthExtra02";
    string mouthExtra03 = "mouthExtra03";
    string sad = "sad";
    string puff = "puff";
    string smile = "smile";
    string mouthBottomTeeth = "mouthBottomTeeth";
    string mouthTopTeeth = "mouthTopTeeth";
    string mouthTongue = "mouthTongue";

    string _Animation = null;
    string _EyesChangeType = null;
    string _EyebrowsChangeType = null;
    string _MouthChangeType = null;
    string _GeneralChangeType = null;
    float _FacialValue = 0.0f;
    bool _FacialValueBool = false;

    float mouthMovement = 0;
    bool closeMouth = true;

    //Cutscene Phases
    void HappyTalking() {
        if (mouthMovement <= 0)
        {
            closeMouth = true;
            
        }
        else if (mouthMovement >= 90)
        {
            closeMouth = false;
        }

        if (closeMouth == true) mouthMovement += (350 * Time.deltaTime);
        else if (closeMouth == false) mouthMovement -= (350 * Time.deltaTime);

        _GeneralChangeType = mouth;
        _MouthChangeType = mouthExtra02;
        _FacialValue = mouthMovement;
        SetFacial();
    }
    //Happy Expression
    void HappyFace() {
        //_SapphiArtChanAnimation = winpose;

        _GeneralChangeType = eyes;
        _EyesChangeType = happyR;
        _FacialValue = 100;
        SetFacial();

        _EyesChangeType = happyL;
        SetFacial();

        _EyesChangeType = closedR;
        SetFacial();

        _EyesChangeType = closedL;
        SetFacial();

        _GeneralChangeType = mouth;
        _MouthChangeType = mouthExtra02;
        _FacialValue = 100;
        SetFacial();
    }

    // Start is called before the first frame update
    void Start()
    {
        _SapphiArtChanAnimator = this.gameObject.GetComponent<Animator>();
        //_AnimationManagerUI = GameObject.Find("AnimationManagerUI").GetComponent<AnimationManagerUI>();

        Transform[] SapphiArtchanChildren = GetComponentsInChildren<Transform>();

        foreach (Transform t in SapphiArtchanChildren)
        {
            if (t.name == face)
                _SapphiArtChanRenderer_Face = t.gameObject.GetComponent<SkinnedMeshRenderer>();
            if (t.name == brow)
                _SapphiArtChanRenderer_Brow = t.gameObject.GetComponent<SkinnedMeshRenderer>();
            if (t.name == BottomTeeth)
                _SapphiArtChanRenderer_BottomTeeth = t.gameObject.GetComponent<SkinnedMeshRenderer>();
            if (t.name == tongue)
                _SapphiArtChanRenderer_Tongue = t.gameObject.GetComponent<SkinnedMeshRenderer>();
            if (t.name == TopTeeth)
                _SapphiArtChanRenderer_TopTeeth = t.gameObject.GetComponent<SkinnedMeshRenderer>();
        }
        //_SapphiArtChanRenderer_Tongue.enabled = false;
        //_SapphiArtChanRenderer_TopTeeth.enabled = true;


    }

    void GetAnimation()
    {
        //Record Last Animation
        _SapphiArtChanLastAnimation = _SapphiArtChanAnimation;

        if (_SapphiArtChanAnimation == null)
            _SapphiArtChanAnimation = idle;

        else
        {
            //Set Animation Parameter
            _SapphiArtChanAnimation = _Animation;
            //_SapphiArtChanAnimation = "hit01";
        }
    }

    void SetAllAnimationFlagsToFalse()
    {
        _SapphiArtChanAnimator.SetBool(param_idletowalk, false);
        _SapphiArtChanAnimator.SetBool(param_idletorunning, false);
        _SapphiArtChanAnimator.SetBool(param_idletojump, false);
        _SapphiArtChanAnimator.SetBool(param_idletowinpose, false);
        _SapphiArtChanAnimator.SetBool(param_idletoko_big, false);
        _SapphiArtChanAnimator.SetBool(param_idletodamage, false);
        _SapphiArtChanAnimator.SetBool(param_idletohit01, false);
        _SapphiArtChanAnimator.SetBool(param_idletohit02, false);
        _SapphiArtChanAnimator.SetBool(param_idletohit03, false);
    }


    void SetAnimation()
    {
        SetAllAnimationFlagsToFalse();

        //IDLE
        if (_SapphiArtChanAnimation == idle)
        {
            _SapphiArtChanAnimator.SetBool(param_toidle, true);
        }

        //WALK
        else if (_SapphiArtChanAnimation == walk)
        {
            _SapphiArtChanAnimator.SetBool(param_idletowalk, true);
        }

        //RUN
        else if (_SapphiArtChanAnimation == running)
        {
            _SapphiArtChanAnimator.SetBool(param_idletorunning, true);
        }

        //JUMP
        else if (_SapphiArtChanAnimation == jump)
        {
            _SapphiArtChanAnimator.SetBool(param_idletojump, true);
        }

        //WIN POSE
        else if (_SapphiArtChanAnimation == winpose)
        {
            _SapphiArtChanAnimator.SetBool(param_idletowinpose, true);
        }

        //KO
        else if (_SapphiArtChanAnimation == ko_big)
        {
            _SapphiArtChanAnimator.SetBool(param_idletoko_big, true);
        }

        //DAMAGE
        else if (_SapphiArtChanAnimation == damage)
        {
            _SapphiArtChanAnimator.SetBool(param_idletodamage, true);
        }

        //HIT 1
        else if (_SapphiArtChanAnimation == hit01)
        {
            _SapphiArtChanAnimator.SetBool(param_idletohit01, true);
        }

        //HIT 2
        else if (_SapphiArtChanAnimation == hit02)
        {
            _SapphiArtChanAnimator.SetBool(param_idletohit02, true);
        }

        //HIT 3
        else if (_SapphiArtChanAnimation == hit03)
        {
            _SapphiArtChanAnimator.SetBool(param_idletohit03, true);
        }
    }

    void ReturnToIdle()
    {
        if (_SapphiArtChanAnimator.GetCurrentAnimatorStateInfo(0).IsName(_SapphiArtChanAnimation))
        {
            if (
                _SapphiArtChanAnimation != walk &&
                _SapphiArtChanAnimation != running &&
                _SapphiArtChanAnimation != ko_big &&
                _SapphiArtChanAnimation != winpose
                )
            {
                SetAllAnimationFlagsToFalse();
                _SapphiArtChanAnimator.SetBool(param_toidle, true);
            }
        }
    }


    void SetFacial()
    {

        //Override the Animator
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(0, _SapphiArtChanFacial_Eye_L_Happy);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(1, _SapphiArtChanFacial_Eye_R_Happy);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(4, _SapphiArtChanFacial_Eye_L_Closed);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(5, _SapphiArtChanFacial_Eye_R_Closed);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(2, _SapphiArtChanFacial_Eye_L_Wide);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(3, _SapphiArtChanFacial_Eye_R_Wide);

        _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(0, _SapphiArtChanFacial_Eyebrow_L_Up);
        _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(1, _SapphiArtChanFacial_Eyebrow_R_Up);
        _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(2, _SapphiArtChanFacial_Eyebrow_L_Angry);
        _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(3, _SapphiArtChanFacial_Eyebrow_R_Angry);
        _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(4, _SapphiArtChanFacial_Eyebrow_L_Sad);
        _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(5, _SapphiArtChanFacial_Eyebrow_R_Sad);

        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(6, _SapphiArtChanFacial_Mouth_E);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(8, _SapphiArtChanFacial_Mouth_O);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(7, _SapphiArtChanFacial_Mouth_JawOpen);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(12, _SapphiArtChanFacial_Mouth_Extra01);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(13, _SapphiArtChanFacial_Mouth_Extra02);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(14, _SapphiArtChanFacial_Mouth_Extra03);

        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(9, _SapphiArtChanFacial_Mouth_Sad);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(10, _SapphiArtChanFacial_Mouth_Puff);
        _SapphiArtChanRenderer_Face.SetBlendShapeWeight(11, _SapphiArtChanFacial_Mouth_Smile);

        if (_SapphiArtChanRenderer_BottomTeeth.isVisible)
            _SapphiArtChanRenderer_BottomTeeth.SetBlendShapeWeight(0, _SapphiArtChanFacial_Mouth_BottomTeeth);

        _SapphiArtChanLastFacial = _SapphiArtChanFacial;
        _SapphiArtChanFacial = _FacialValue;
        _SapphiArtChanLastFacialBool = _SapphiArtChanFacialBool;
        _SapphiArtChanFacialBool = _FacialValueBool;

        if (_GeneralChangeType == null)
        {
            return;
        }

        else if (_GeneralChangeType == eyes)
        {
            if (_EyesChangeType == null)
                return;

            if (_EyesChangeType == _EyesLastChangeType && _SapphiArtChanFacial == _SapphiArtChanLastFacial)
                return;

            else if (_EyesChangeType == happyL)
            {
                _EyesLastChangeType = _EyesChangeType;
                _SapphiArtChanFacial_Eye_L_Happy = _SapphiArtChanFacial;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(0, _SapphiArtChanFacial);
            }
            else if (_EyesChangeType == happyR)
            {
                _EyesLastChangeType = _EyesChangeType;
                _SapphiArtChanFacial_Eye_R_Happy = _SapphiArtChanFacial;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(1, _SapphiArtChanFacial);
            }
            else if (_EyesChangeType == closedL)
            {
                _SapphiArtChanFacial_Eye_L_Closed = _SapphiArtChanFacial;
                _EyesLastChangeType = _EyesChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(4, _SapphiArtChanFacial);

            }
            else if (_EyesChangeType == closedR)
            {
                _SapphiArtChanFacial_Eye_R_Closed = _SapphiArtChanFacial;
                _EyesLastChangeType = _EyesChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(5, _SapphiArtChanFacial);

            }
            else if (_EyesChangeType == wideL)
            {
                _SapphiArtChanFacial_Eye_L_Wide = _SapphiArtChanFacial;
                _EyesLastChangeType = _EyesChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(2, _SapphiArtChanFacial);

            }
            else if (_EyesChangeType == wideR)
            {
                _SapphiArtChanFacial_Eye_R_Wide = _SapphiArtChanFacial;
                _EyesLastChangeType = _EyesChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(3, _SapphiArtChanFacial);
            }
        }



        else if (_GeneralChangeType == eyebrows)
        {
            if (_EyebrowsChangeType == null)
                return;

            if (_EyebrowsChangeType == _EyebrowsLastChangeType && _SapphiArtChanFacial == _SapphiArtChanLastFacial)
                return;

            else if (_EyebrowsChangeType == upL)
            {
                _SapphiArtChanFacial_Eyebrow_L_Up = _SapphiArtChanFacial;
                _EyebrowsLastChangeType = _EyebrowsChangeType;
                _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(0, _SapphiArtChanFacial);
            }

            else if (_EyebrowsChangeType == upR)
            {
                _SapphiArtChanFacial_Eyebrow_R_Up = _SapphiArtChanFacial;
                _EyebrowsLastChangeType = _EyebrowsChangeType;
                _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(1, _SapphiArtChanFacial);
            }

            else if (_EyebrowsChangeType == angerL)
            {
                _SapphiArtChanFacial_Eyebrow_L_Angry = _SapphiArtChanFacial;
                _EyebrowsLastChangeType = _EyebrowsChangeType;
                _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(2, _SapphiArtChanFacial);
            }

            else if (_EyebrowsChangeType == angerR)
            {
                _SapphiArtChanFacial_Eyebrow_R_Angry = _SapphiArtChanFacial;
                _EyebrowsLastChangeType = _EyebrowsChangeType;
                _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(3, _SapphiArtChanFacial);
            }

            else if (_EyebrowsChangeType == sadL)
            {
                _SapphiArtChanFacial_Eyebrow_L_Sad = _SapphiArtChanFacial;
                _EyebrowsLastChangeType = _EyebrowsChangeType;
                _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(4, _SapphiArtChanFacial);
            }

            else if (_EyebrowsChangeType == sadR)
            {
                _SapphiArtChanFacial_Eyebrow_R_Sad = _SapphiArtChanFacial;
                _EyebrowsLastChangeType = _EyebrowsChangeType;
                _SapphiArtChanRenderer_Brow.SetBlendShapeWeight(5, _SapphiArtChanFacial);
            }
        }

        else if (_GeneralChangeType == mouth)
        {
            if (_MouthChangeType == null)
                return;

            if (_MouthChangeType == _MouthLastChangeType &&
                _SapphiArtChanFacial == _SapphiArtChanLastFacial &&
                _SapphiArtChanFacialBool == _SapphiArtChanLastFacialBool)
                return;

            else if (_MouthChangeType == mouthE)
            {
                _SapphiArtChanFacial_Mouth_E = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(6, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == mouthO)
            {
                _SapphiArtChanFacial_Mouth_O = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(8, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == mouthJawOpen)
            {
                _SapphiArtChanFacial_Mouth_JawOpen = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(7, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == mouthExtra01)
            {
                _SapphiArtChanFacial_Mouth_Extra01 = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(12, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == mouthExtra02)
            {
                _SapphiArtChanFacial_Mouth_Extra02 = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(13, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == mouthExtra03)
            {
                _SapphiArtChanFacial_Mouth_Extra03 = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(14, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == sad)
            {
                _SapphiArtChanFacial_Mouth_Sad = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(9, _SapphiArtChanFacial);

            }
            else if (_MouthChangeType == puff)
            {
                _SapphiArtChanFacial_Mouth_Puff = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(10, _SapphiArtChanFacial);
            }
            else if (_MouthChangeType == smile)
            {
                _SapphiArtChanFacial_Mouth_Smile = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_Face.SetBlendShapeWeight(11, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == mouthBottomTeeth)
            {
                _SapphiArtChanFacial_Mouth_BottomTeeth = _SapphiArtChanFacial;
                _MouthLastChangeType = _MouthChangeType;
                _SapphiArtChanRenderer_BottomTeeth.SetBlendShapeWeight(0, _SapphiArtChanFacial);
            }

            else if (_MouthChangeType == mouthTopTeeth)
            {
                _SapphiArtChanFacial_Mouth_TopTeeth = _SapphiArtChanFacialBool;
                Debug.Log(_SapphiArtChanFacialBool);
                _MouthLastChangeType = _MouthChangeType;
                if (_SapphiArtChanFacialBool == false)
                    _SapphiArtChanRenderer_TopTeeth.enabled = _SapphiArtChanFacialBool;
                else
                    _SapphiArtChanRenderer_TopTeeth.enabled = true;
            }

            else if (_MouthChangeType == mouthTongue)
            {
                _SapphiArtChanFacial_Mouth_Tongue = _SapphiArtChanFacialBool;
                _MouthLastChangeType = _MouthChangeType;
                if (_SapphiArtChanFacialBool == false)
                    _SapphiArtChanRenderer_Tongue.enabled = _SapphiArtChanFacialBool;
                else
                    _SapphiArtChanRenderer_Tongue.enabled = true;
            }
        }
    }



    void Update()
    {
        

        //Get Animation from UI
        GetAnimation();

        HappyFace();
        HappyTalking();

        //Set New Animation
        if (_SapphiArtChanLastAnimation != _SapphiArtChanAnimation)
            SetAnimation();
        else
        {
            ReturnToIdle();
        }
    }

    void LateUpdate()
    {
        //Set Facial
        SetFacial();
    }
}
