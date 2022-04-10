using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafehouseManager : MonoBehaviour
{
    public bool canSafehouse;
    public bool isSafehouse;
    public bool isCheatOn;
    public bool cannotExit;
    public float hideTime;
    public float fadeTime;
    private CanvasGroup cg;
    public bool isFading;
    private bool checkBoolChange;

    public Enemy enemyScript;

    public ChangeInventory CI;

    public Transform spawnPoint;

    private static SafehouseManager me = null;

    public static SafehouseManager Me
    {
        get
        {
            return me;
        }
    }

    private void Awake()
    {
        if(me != null && me != this)
        {
            Destroy(this.gameObject);
        }

        me = this;
    }

    void Start()
    {
        checkBoolChange = isSafehouse;
        cg = GetComponent<CanvasGroup>();
        CI = GetComponentInChildren<ChangeInventory>();
        enemyScript = GameObject.Find("Bear").GetComponent<Enemy>();
        canSafehouse = false;
    }

    void Update()
    {
        //Test change bool
        if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("BButton") &&
            !PlayerScriptNew.me.anim.GetCurrentAnimatorStateInfo(0).IsName("readingText") &&
            !isFading)
        {
            if (isSafehouse && !cannotExit)
            {
                isSafehouse = false;
                CI.choosenMatIndex = 0;
            }
            else if(!isSafehouse && isCheatOn)
                isSafehouse = true;
        }
        //when fading, but try to change state
        if(isSafehouse != checkBoolChange && isFading)
        {
            isSafehouse = checkBoolChange;
        }
        //when bool is changed, do once
        if(isSafehouse != checkBoolChange && isSafehouse)   
        {
            //Debug.Log("Safehouse");
            ResetMatAmount();
            //AmbienceManager.ambienceManager.SafeHouseAmbiencePlay();
            BGMMan.bGMManger.EnterSafeHoueBaguaMusic();//enter safehouse sound
            //if(enemyScript != null)
                //enemyScript.ResetEnemy(); // [Safehouse update]need some more detail
            //PostProcessingManager.Me.StopAllCoroutines();
            PostProcessingManager.Me.StartCoroutine(PostProcessingManager.Me.ResetFilter());
            StartCoroutine(FadeCanvas(cg, 1f, fadeTime));
            checkBoolChange = isSafehouse;
        }
        else if(isSafehouse != checkBoolChange && !isSafehouse)
        {
            //AmbienceManager.ambienceManager.HallwayAmbiencePlay();
            BGMMan.bGMManger.EndMusic();//off safehouse
            PlayerScriptNew.me.walking = true;
            StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
            //RespawnPlayer(spawnPoint); // [Safehouse update]need a location
            //WallHider.me.roomPlayerIsIn = WallHider.Room.corridor; // currently commented by HG
            PlayerScriptNew.me.selectedMats.Clear();
            checkBoolChange = isSafehouse;
            /*
            if(enemyScript.isPhaseTwo)
            {
                enemyScript.GotoLoc();
            }
            if(enemyScript.phase != Enemy.AIPhase.NotInBattle)
            {
                BGMMan.bGMManger.StartBattleMusic();
            }
            */
        }
    }

    public void ResetMatAmount()
    {
        foreach(var mat in PlayerScriptNew.me.matSlots)
        {
            if(mat != null)
            {
                mat.GetComponent<MatScriptNew>().amount = mat.GetComponent<MatScriptNew>().amount_max;
            }
        }
        
    }

    public void RespawnPlayer(Transform SpawnPoint)
    {
        PlayerScriptNew.me.transform.position = new Vector3(SpawnPoint.position.x, PlayerScript.me.transform.position.y , SpawnPoint.position.z);
        PlayerScriptNew.me.transform.Find("PlayerModel").localPosition = Vector3.zero;
        PlayerScriptNew.me.hp = 30;
        PlayerScriptNew.me.dead = false;
        PlayerScriptNew.me.gameObject.SetActive(true);
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float endValue, float duration)
    {
        isFading = true;
        float elapsedTime = 0;
        float startValue = cg.alpha;
        if(endValue >= 1)
            CI.choosenMatIndex = 4;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            yield return null;
        }
        isFading = false;
    }
}
