using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafehouseManager : MonoBehaviour
{
    private float timer;
    public bool canSafehouse;
    public bool isSafehouse;
    public bool isCheatOn;
    public float hideTime;
    public float fadeTime;
    private CanvasGroup cg;
    private bool isFading;
    private bool checkBoolChange;

    public Enemy enemyScript;

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
        enemyScript = GameObject.Find("Bear").GetComponent<Enemy>();
        canSafehouse = false;
    }

    void Update()
    {
        //Test change bool
        if (Input.GetKeyDown(KeyCode.B) && isSafehouse)
        {
            isSafehouse = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && !isSafehouse && isCheatOn)
        {
            isSafehouse = true;
        }
        //when fading, but try to change state
        if(isSafehouse != checkBoolChange && isFading)
        {
            isSafehouse = checkBoolChange;
        }
        //check if fading or not
        if(timer >= fadeTime)
        {
            isFading = false;
        }
        else
        {
            timer += Time.deltaTime;
        }
        //when bool is changed, do once
        if(isSafehouse != checkBoolChange && isSafehouse)   
        {
            //Debug.Log("Safehouse");
            ResetMatAmount();
            AmbienceManager.ambienceManager.SafeHouseAmbiencePlay();//enter safehouse sound
            if(enemyScript != null)
                enemyScript.ResetEnemy();
            PostProcessingManager.Me.StopAllCoroutines();
            PostProcessingManager.Me.StartCoroutine(PostProcessingManager.Me.ResetFilter());
            StartCoroutine(FadeCanvas(cg, 1f, fadeTime));
            checkBoolChange = isSafehouse;
        }
        else if(isSafehouse != checkBoolChange && !isSafehouse)
        {
            AmbienceManager.ambienceManager.HallwayAmbiencePlay();//off safehouse
            PlayerScriptNew.me.walking = true;
            StartCoroutine(FadeCanvas(cg, 0f, fadeTime));
            RespawnPlayer(spawnPoint);
            WallHider.me.roomPlayerIsIn = WallHider.Room.corridor;
            PlayerScriptNew.me.selectedMats.Clear();
            checkBoolChange = isSafehouse;
            if(enemyScript.isPhaseTwo)
            {
                enemyScript.GotoLoc();
            }
            if(enemyScript.phase != Enemy.AIPhase.NotInBattle)
            {
                BGMMan.bGMManger.StartBattleMusic();
            }
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
        timer = 0;
        isFading = true;
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
