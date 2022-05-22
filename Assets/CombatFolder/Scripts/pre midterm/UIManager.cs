using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private float timer;
    [Header("UI_Fade")]
    public float hideTime;
    public float fadeTime;

    [Header("UI_Icon")]
    public Image leftIcon;
    public Image rightIcon;
    public Image upIcon;
    public Image downIcon;

    [Header("UI_ColorChange")]
    public Color selectedColor;
    public Color outOfStock;
    public Image leftSlot;
    public Image rightSlot;
    public Image upSlot;
    public Image downSlot;
    public float colorAlpha;

    [Header("UI_Amount")]
    public TextMeshProUGUI slot0_TMP;
    public TextMeshProUGUI slot1_TMP;
    public TextMeshProUGUI slot2_TMP;
    public TextMeshProUGUI slot3_TMP;

    [Header("UI_CD")]
    public TextMeshProUGUI slot0_CD;
    public TextMeshProUGUI slot1_CD;
    public TextMeshProUGUI slot2_CD;
    public Image slot0_Fill;
    public Image slot1_Fill;
    public Image slot2_Fill;
    public float fillAlpha;
    public float expandAmount;
    public float expandSpeed;
    public Coroutine left_C;
    public Coroutine up_C;
    public Coroutine right_C;

    [Header("UI_Choosen")]
    public GameObject choLeft;
    public GameObject choUp;
    public GameObject choRight;
    public GameObject choDown;

    [Header("UI_Combo")]
    public TextMeshProUGUI comboInstruct_TMP;
    public GameObject match;
    public GameObject nail;
    public GameObject tear;

    private bool isHided;
    private CanvasGroup cg;

    private static UIManager me;
    public static UIManager Me
    {
        get
        {
            return me;
        }
    }

    private void Awake()
    {
        if(me!=null && me != this)
        {
            Destroy(gameObject);
        }

        me = this;
    }

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        UI_ChangeIcon();
        choLeft.SetActive(false);
        choUp.SetActive(false);
        choRight.SetActive(false);
        choDown.SetActive(false);
        //ComboInstruct();
    }

    void Update()
    {
        UI_SelectMat();
        UI_ChangeAmount();
        UI_CD();
        ComboInstruct();

        timer += Time.deltaTime;
        if(timer >= hideTime & !isHided)
        {
            isHided = true;
            StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
        }
        if (Input.anyKeyDown || Input.GetAxis("LT") != 0 || Input.GetAxis("RT") != 0)
        {
            if(!(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.E)))
            {
                timer = 0;
                isHided = false;
                StartCoroutine(FadeCanvas(cg, 1f, fadeTime));
            }
        }
    }

    public void ComboInstruct()//I love hard coding
    {
        List<GameObject> selectedMats = PlayerScriptNew.me.selectedMats;
        if (selectedMats.Count <= 0)
        {
            comboInstruct_TMP.text = "";
        }
        else
        {
            if(selectedMats.Count == 1)
            {
                if (selectedMats.Contains(match))
                {
                    comboInstruct_TMP.text = "Recover Materials";
                    comboInstruct_TMP.color = new Color32(100, 170, 200, 255);
                }
                else if (selectedMats.Contains(nail))
                {
                    comboInstruct_TMP.text = "Less effective Stun";
                    comboInstruct_TMP.color = new Color32(222, 173, 122, 255);
                }
                else if (selectedMats.Contains(tear))
                {
                    comboInstruct_TMP.text = "Spread";
                    comboInstruct_TMP.color = ColorStorage.me.funChoCol;
                }
                else
                {
                    comboInstruct_TMP.text = "Evil Material";
                    comboInstruct_TMP.color = ColorStorage.me.bosChoCol;
                }
            }
            else if(selectedMats.Count == 2)
            {
                comboInstruct_TMP.text = "<i><b>Combo:</b></i> ";
                if (selectedMats.Contains(match))
                {
                    if (selectedMats.Contains(nail))
                    {
                        comboInstruct_TMP.text += "Damage";
                        comboInstruct_TMP.color = new Color32(200, 70, 51, 255);
                    }
                    else if (selectedMats.Contains(tear))
                    {
                        comboInstruct_TMP.text += "Recover a Lot of Materials";
                        comboInstruct_TMP.color = ColorStorage.me.ampChoCol;
                    }
                    else
                    {
                        comboInstruct_TMP.text += "Recovery Evil Material";
                        comboInstruct_TMP.color = ColorStorage.me.bosChoCol;
                    }
                }
                else if (selectedMats.Contains(nail))
                {
                    if (selectedMats.Contains(tear))
                    {
                        comboInstruct_TMP.text += "Easy to Stun";
                        comboInstruct_TMP.color = ColorStorage.me.atkChoCol;
                    }
                    else
                    {
                        comboInstruct_TMP.text += "Stun Evil Material";
                        comboInstruct_TMP.color = ColorStorage.me.bosChoCol;
                    }
                }
                else if (selectedMats.Contains(tear))
                {
                    comboInstruct_TMP.text += "Spread Evil Material";
                    comboInstruct_TMP.color = ColorStorage.me.bosChoCol;
                }
            }
        }
    }

    public IEnumerator MakePulse(Image Icon, float duration)
    {
        Vector2 startSize = Icon.GetComponent<RectTransform>().sizeDelta;
        Vector2 targetSize = startSize * expandAmount;

        float scrollAmount = 0;

        while (scrollAmount < duration)
        {
            scrollAmount += Time.deltaTime * expandSpeed;

            Icon.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(startSize, targetSize, scrollAmount / duration);

            print(Icon.GetComponent<RectTransform>().sizeDelta.x);
            yield return null;
        }

        scrollAmount = 0;

        while (scrollAmount < duration)
        {
            scrollAmount += Time.deltaTime * expandSpeed;

            Icon.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(targetSize, startSize, scrollAmount / duration);

            //print(Icon.GetComponent<RectTransform>().sizeDelta.x);
            yield return null;
        }

        if(Icon == leftIcon)
        {
            left_C = null;
        }
        else if(Icon == upIcon)
        {
            up_C = null;
        }
        else if(Icon == rightIcon)
        {
            right_C = null;
        }
    }

    public void UI_CD()
    {
        for(int i = 0; i < 3; i++)
        {
            if(PlayerScriptNew.me.matSlots[i] != null &&
               PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount < PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount_max)
            {
                if (i == 0)
                {
                    slot0_Fill.color = ColorStorage.me.ChoColor(i);
                    slot0_Fill.color = new Color(slot0_Fill.color.r, slot0_Fill.color.g, slot0_Fill.color.b, fillAlpha);
                    slot0_Fill.fillAmount = ((float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD_max -
                                             (float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD) /
                                             (float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD_max;
                }
                else if(i == 1)
                {
                    slot1_Fill.color = ColorStorage.me.ChoColor(i);
                    slot1_Fill.color = new Color(slot1_Fill.color.r, slot1_Fill.color.g, slot1_Fill.color.b, fillAlpha);
                    slot1_Fill.fillAmount = ((float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD_max -
                                             (float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD) /
                                             (float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD_max;
                }
                else if(i == 2)
                {
                    slot2_Fill.color = ColorStorage.me.ChoColor(i);
                    slot2_Fill.color = new Color(slot2_Fill.color.r, slot2_Fill.color.g, slot2_Fill.color.b, fillAlpha);
                    slot2_Fill.fillAmount = ((float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD_max -
                                             (float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD) /
                                             (float)PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD_max;
                }
            }
            else if(PlayerScriptNew.me.matSlots[i] == null
                    || PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount >= PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount_max)
            {
                if (i == 0)
                    slot0_Fill.color = new Color(0, 0, 0, 0);
                else if (i == 1)
                    slot1_Fill.color = new Color(0, 0, 0, 0);
                else if (i == 2)
                    slot2_Fill.color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void UI_ChangeIcon()
    {
        if (PlayerScriptNew.me.matSlots[0] != null)
        {
            leftIcon.sprite = PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().matIcon;
            leftIcon.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            leftIcon.sprite = null;
            leftIcon.color = new Color32(255, 255, 255, 0);
        }

        if (PlayerScriptNew.me.matSlots[1] != null)
        {
            upIcon.sprite = PlayerScriptNew.me.matSlots[1].GetComponent<MatScriptNew>().matIcon;
            upIcon.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            upIcon.sprite = null;
            upIcon.color = new Color32(255, 255, 255, 0);
        }

        if (PlayerScriptNew.me.matSlots[2] != null)
        {
            rightIcon.sprite = PlayerScriptNew.me.matSlots[2].GetComponent<MatScriptNew>().matIcon;
            rightIcon.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            rightIcon.sprite = null;
            rightIcon.color = new Color32(255, 255, 255, 0);
        }

        if (PlayerScriptNew.me.matSlots[3] != null)
        {
            downIcon.sprite = PlayerScriptNew.me.matSlots[3].GetComponent<MatScriptNew>().matIcon;
            downIcon.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            downIcon.sprite = null;
            downIcon.color = new Color32(255, 255, 255, 0);
        }
    }

    public void UI_SelectMat()
    {
        if (PlayerScriptNew.me.selectedMats.Contains(PlayerScriptNew.me.matSlots[0]))
        {
            choLeft.SetActive(true);
            choLeft.GetComponent<Image>().color = ColorStorage.me.ChoColor(0);
        }
        else
        {
            choLeft.SetActive(false);
        }
        if (PlayerScriptNew.me.selectedMats.Contains(PlayerScriptNew.me.matSlots[1]))
        {
            choUp.SetActive(true);
            choUp.GetComponent<Image>().color = ColorStorage.me.ChoColor(1);
        }
        else
        {
            choUp.SetActive(false);
        }
        if (PlayerScriptNew.me.selectedMats.Contains(PlayerScriptNew.me.matSlots[2]))
        {
            choRight.SetActive(true);
            choRight.GetComponent<Image>().color = ColorStorage.me.ChoColor(2);
        }
        else
        {
            choRight.SetActive(false);
        }
        if (PlayerScriptNew.me.selectedMats.Contains(PlayerScriptNew.me.matSlots[3]))
        {
            choDown.SetActive(true);
            choDown.GetComponent<Image>().color = ColorStorage.me.ChoColor(3);
        }
        else
        {
            choDown.SetActive(false);
        }
    }

    public void UI_ChangeAmount()
    {
        if (PlayerScriptNew.me.matSlots[0] != null)
            slot0_TMP.text = PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().amount.ToString();
        else
            slot0_TMP.text = "0";
        if (PlayerScriptNew.me.matSlots[1] != null)
            slot1_TMP.text = PlayerScriptNew.me.matSlots[1].GetComponent<MatScriptNew>().amount.ToString();
        else
            slot1_TMP.text = "0";
        if (PlayerScriptNew.me.matSlots[2] != null)
            slot2_TMP.text = PlayerScriptNew.me.matSlots[2].GetComponent<MatScriptNew>().amount.ToString();
        else
            slot2_TMP.text = "0";
        if (PlayerScriptNew.me.matSlots[3] != null)
            slot3_TMP.text = PlayerScriptNew.me.matSlots[3].GetComponent<MatScriptNew>().amount.ToString();
        else
            slot3_TMP.text = "0";

        leftSlot.color = ColorStorage.me.ChoColor(0);
        if (ColorStorage.me.ChoColor(0) == ColorStorage.me.noMat)
            leftSlot.color = new Color(leftSlot.color.r, leftSlot.color.g, leftSlot.color.b, colorAlpha);
        upSlot.color = ColorStorage.me.ChoColor(1);
        if (ColorStorage.me.ChoColor(1) == ColorStorage.me.noMat)
            upSlot.color = new Color(upSlot.color.r, upSlot.color.g, upSlot.color.b, colorAlpha);
        rightSlot.color = ColorStorage.me.ChoColor(2);
        if (ColorStorage.me.ChoColor(2) == ColorStorage.me.noMat)
            rightSlot.color = new Color(rightSlot.color.r, rightSlot.color.g, rightSlot.color.b, colorAlpha);
        downSlot.color = ColorStorage.me.ChoColor(3);
        if (ColorStorage.me.ChoColor(3) == ColorStorage.me.noMat)
            downSlot.color = new Color(downSlot.color.r, downSlot.color.g, downSlot.color.b, colorAlpha);
    }

    public IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = cg.alpha;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
    }
}
