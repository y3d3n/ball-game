using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject startUI, gameOverUI, settingUI, doubleUI, playingUI, noThanksButton;

    public Transform stageParent;
    private List <GameObject> stageItem = new List<GameObject>();
    bool isPlaying = false;
    bool isDead = false;
    bool isStarted = false;

    public Sprite stageComplete, stagePlaying, stageNext;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        startUI.SetActive(true);
        playingUI.SetActive(true);
        gameOverUI.SetActive(false);
        settingUI.SetActive(false);
        doubleUI.SetActive(false);
    }
    // Update is called once per frame
    private void Update()
    {
        if (isPlaying)
        {
            startUI.SetActive(false);
            playingUI.SetActive(true);
            
            isPlaying = false;
        }
        else if (isDead)
        {
            doubleUI.SetActive(true);
            playingUI.SetActive(false);
            StartCoroutine(ActiveText());
            isDead = false;
        }
        else if(isStarted)
        {
            startUI.SetActive(true);
            doubleUI.SetActive(false);
            playingUI.SetActive(true);
            isStarted = false;
        }
    }
    public void UpdateCount()
    {
        
    }

    public void PlayButton()
    {
        isPlaying = true;
    }

    public void GameOver()
    {
        isDead = true;
        Debug.Log("Fuck Over");
    }

    public void NoThanksButton()
    {
        isStarted = true;
    }

    IEnumerator ActiveText()
    {
        noThanksButton.SetActive(false);
        yield return new WaitForSeconds(5f);
        noThanksButton.SetActive(true);
        yield return true;
    }
    
    public IEnumerator DeactiveStage(int s)
    {
            foreach (Transform g in stageParent.transform.GetComponentsInChildren<Transform>())
            {
                if (stageParent != g)
                {
                    stageItem.Add(g.gameObject);
                    g.gameObject.SetActive(false);
                    Debug.Log("Current Size: " + stageItem.Count);
                }
            }

            Debug.Log("Total Size: " + stageItem.Count);

            for (int i = 0; i < s; i++)
            {
                stageItem[i].gameObject.SetActive(true);
                stageItem[i + 1].gameObject.GetComponent<Image>().sprite = stageNext;
            }
        yield return true;
    }

    public void ChangeStageUI(int s)
    {
        stageItem[s-2].gameObject.GetComponent<Image>().sprite = stageComplete;
        stageItem[s-1].gameObject.GetComponent<Image>().sprite = stagePlaying;
        Debug.Log("FUcking Stage: " + s);
    }

    public void LevelCompletedUI()
    {
        doubleUI.SetActive(true);
    }
}
