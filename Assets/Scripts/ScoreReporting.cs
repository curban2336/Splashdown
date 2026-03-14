using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreReporting : MonoBehaviour
{
    [SerializeField] private BG_MoveLeft bgMoveLeft;
    public int score = 0;
    private float accumulatedScore = 0f;

    public int totalTrickSuccesses = 0;
    public int totalTricks = 0;
    public int totalTrickFails = TrickHandler.wrongCount;

    private int currentTrickFails = 0;
    private bool addedScoreOnEnter = false;
    private bool wasJumpingPrevFrame = false;

    [SerializeField] private float multiplierStep = 0.25f;
    [SerializeField] private float minMultiplier = 0.75f;

    [Header("Trick Result UI")]
    [SerializeField] private GameObject perfectUI;
    [SerializeField] private GameObject greatUI;
    [SerializeField] private GameObject coolUI;
    [SerializeField] private GameObject okUI;
    [SerializeField] private GameObject badUI;
    [SerializeField] private float resultUIDuration = 1f;

    [SerializeField] TrickHandler trickHandler;

    private Coroutine resultUICoroutine;

    void Awake()
    {
        if (bgMoveLeft == null)
            bgMoveLeft = FindObjectOfType<BG_MoveLeft>();

        if (bgMoveLeft == null)
            Debug.LogWarning("ScoreReporting: No BG_MoveLeft instance found. Score from speed will be 0.");
    }

    void Start()
    {
        accumulatedScore = 0f;
        score = 0;
        addedScoreOnEnter = true;
        wasJumpingPrevFrame = false;//ColorSwitcherWater.isJumping;

        SetAllResultUIsActive(false);
    }

    void Update()
    {
        float currentSpeed = (bgMoveLeft != null) ? bgMoveLeft.speed : 0f;
        bool isJumping = ColorSwitcherWater.isJumping;
        totalTrickFails = TrickHandler.wrongCount;

        if (isJumping && !wasJumpingPrevFrame)
        {
            currentTrickFails = 0;
            totalTricks = trickHandler.trickCount;
            addedScoreOnEnter = false;
            
            //if (resultUICoroutine != null)
            //{
            //    StopCoroutine(resultUICoroutine);
            //    resultUICoroutine = null;
            //}
            SetAllResultUIsActive(false);
        }

        if (isJumping)
        {
            accumulatedScore += currentSpeed * 100f * Time.deltaTime;
        }
        else
        {
            
            if (!addedScoreOnEnter)
            {
                AddScore();
                ShowResultForCurrentTrick();
                addedScoreOnEnter = true;
                accumulatedScore = 0f; 
                currentTrickFails = 0; 
            }
        }

        wasJumpingPrevFrame = isJumping;
    }

    public void AddScore()
    {
        if (WaterMovement.isInWater)
        {
            float currentMultiplier = CalculateMultiplier();
            score += Mathf.FloorToInt(accumulatedScore * currentMultiplier);
        }
    }

    public float CalculateMultiplier()
    {
        int net = trickHandler.trickCount - totalTrickFails;
        float multiplier = 1f + net * multiplierStep;
        if (multiplier < minMultiplier) multiplier = minMultiplier;
        return multiplier;
    }

    public void ApplyMultiplierToAccumulatedScore()
    {
        float m = CalculateMultiplier();
        accumulatedScore *= m;
    }

    public void AddTrickSuccess(int count = 1)
    {
        totalTrickSuccesses += count;
    }

    public void AddTrickFail(int count = 1)
    {
        totalTrickFails += count;
        currentTrickFails += count;
    }

    private void ShowResultForCurrentTrick()
    {
        int defaultInputsPerTrick = totalTricks;

        totalTrickFails = TrickHandler.wrongCount;



        ShowResultForFails(totalTrickFails, defaultInputsPerTrick);
    }

    // Public method to allow caller to supply the actual input count for the trick
    public void ShowResultForFails(int fails, int inputs)
    {
        Debug.Log(fails);
        Debug.Log(inputs);
        // guard
        if (inputs <= 0) inputs = 1;

        float ratio = (float)fails / inputs;

        GameObject toShow = null;
        if (fails <= 0)
        {
            toShow = perfectUI;
        }
        else if (ratio >= 1f)
        {
            toShow = badUI;
        }
        else if (ratio <= 0.25f)
        {
            toShow = greatUI;
        }
        else if (ratio <= 0.5f)
        {
            toShow = coolUI;
        }
        else
        {
            toShow = okUI;
        }

        Debug.Log(ratio);
        Debug.Log(toShow.name);

        if (toShow != null)
        {
            StartCoroutine(ShowUIForSeconds(toShow, resultUIDuration));
            //if (resultUICoroutine != null)
            //{
            //    Debug.Log("Stop previous UI coroutine");
            //    StopCoroutine(resultUICoroutine);
            //}
        }

        TrickHandler.wrongCount = 0;
    }

    private IEnumerator ShowUIForSeconds(GameObject ui, float seconds)
    {
        SetAllResultUIsActive(false);
        ui.SetActive(true);
        yield return new WaitForSeconds(seconds);
        ui.SetActive(false);
        //resultUICoroutine = null;
    }

    private void SetAllResultUIsActive(bool active)
    {
        perfectUI.SetActive(active);
        greatUI.SetActive(active);
        coolUI.SetActive(active);
        okUI.SetActive(active);
        badUI.SetActive(active);
    }

    public void ResetTrickCounters()
    {
        totalTrickSuccesses = 0;
        currentTrickFails = 0;
    }

    public float GetRawScore()
    {
        return accumulatedScore * CalculateMultiplier();
    }
}
