using UnityEngine;

public class ScoreReporting : MonoBehaviour
{
    [SerializeField] private BG_MoveLeft bgMoveLeft;
    public int score = 0;
    private float accumulatedScore = 0f;

    public int totalTrickSuccesses = 0;
    public int totalTrickFails = 0;

    
    [SerializeField] private float multiplierStep = 0.25f; 
    [SerializeField] private float minMultiplier = 0.75f;

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
    }

    void Update()
    {
        float currentSpeed = (bgMoveLeft != null) ? bgMoveLeft.speed : 0f;

        if (ColorSwitcherWater.isJumping)
        {
            accumulatedScore += currentSpeed * 100f * Time.deltaTime;
        }

        float currentMultiplier = CalculateMultiplier();
        score = Mathf.FloorToInt(accumulatedScore * currentMultiplier);
    }

    public float CalculateMultiplier()
    {
        int net = totalTrickSuccesses - totalTrickFails;
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
    }

    public void ResetTrickCounters()
    {
        totalTrickSuccesses = 0;
        totalTrickFails = 0;
    }

    public float GetRawScore()
    {
        return accumulatedScore * CalculateMultiplier();
    }
}
