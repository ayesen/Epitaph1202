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

    [Header("UI_Amount")]
    public TextMeshProUGUI slot0_TMP;
    public TextMeshProUGUI slot1_TMP;
    public TextMeshProUGUI slot2_TMP;
    public TextMeshProUGUI slot3_TMP;

    [Header("UI_CD")]
    public TextMeshProUGUI slot0_CD;
    public TextMeshProUGUI slot1_CD;
    public TextMeshProUGUI slot2_CD;

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
            Destroy(this.gameObject);
        }

        me = this;
    }

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        UI_ChangeIcon();
    }

    void Update()
    {
        UI_SelectMat();
        UI_ChangeAmount();
        UI_CD();

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

    public void UI_CD()
    {
        for(int i = 0; i < 3; i++)
        {
            if(PlayerScriptNew.me.matSlots[i] != null
               && PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount < PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount_max)
            {
                if (i == 0)
                    slot0_CD.text = "" + PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD;
                else if(i == 1)
                    slot1_CD.text = "" + PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD;
                else if(i == 2)
                    slot2_CD.text = "" + PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().CD;
            }
            else if(PlayerScriptNew.me.matSlots[i] == null
                    || PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount >= PlayerScriptNew.me.matSlots[i].GetComponent<MatScriptNew>().amount_max)
            {
                if(i == 0)
                    slot0_CD.text = "";
                else if(i == 1)
                    slot1_CD.text = "";
                else if(i == 2)
                    slot2_CD.text = "";
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
            if(PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().amount > 0)
                leftSlot.color = selectedColor;
        }
        else
        {
            leftSlot.color = Color.white;

            if (PlayerScriptNew.me.matSlots[0] != null)
            {
                if (PlayerScriptNew.me.matSlots[0].GetComponent<MatScriptNew>().amount <= 0)
                    leftSlot.color = outOfStock;
            }
        }
        if (PlayerScriptNew.me.selectedMats.Contains(PlayerScriptNew.me.matSlots[1]))
        {
            if (PlayerScriptNew.me.matSlots[1].GetComponent<MatScriptNew>().amount > 0)
                upSlot.color = selectedColor;
        }
        else
        {
            upSlot.color = Color.white;

            if (PlayerScriptNew.me.matSlots[1] != null)
            {
                if (PlayerScriptNew.me.matSlots[1].GetComponent<MatScriptNew>().amount <= 0)
                    upSlot.color = outOfStock;
            }
        }
        if (PlayerScriptNew.me.selectedMats.Contains(PlayerScriptNew.me.matSlots[2]))
        {
            if (PlayerScriptNew.me.matSlots[2].GetComponent<MatScriptNew>().amount > 0)
                rightSlot.color = selectedColor;
        }
        else
        {
            rightSlot.color = Color.white;

            if (PlayerScriptNew.me.matSlots[2] != null)
            {
                if (PlayerScriptNew.me.matSlots[2].GetComponent<MatScriptNew>().amount <= 0)
                    rightSlot.color = outOfStock;
            }
        }
        if (PlayerScriptNew.me.selectedMats.Contains(PlayerScriptNew.me.matSlots[3]))
        {
            if (PlayerScriptNew.me.matSlots[3].GetComponent<MatScriptNew>().amount > 0)
                downSlot.color = selectedColor;
        }
        else
        {
            downSlot.color = Color.white;

            if (PlayerScriptNew.me.matSlots[3] != null)
            {
                if (PlayerScriptNew.me.matSlots[3].GetComponent<MatScriptNew>().amount <= 0)
                    downSlot.color = outOfStock;
            }
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
