using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //[Header("Debug Purposes")]
    [System.NonSerialized]
    public PaddleControl usingPaddle;
    [System.NonSerialized]
    public Ball usingBall;

    private bool IsClear
    {
        get
        {
            return currentNumberOfBlock.Value <= 0;
        }
    }

    private bool CanEndGame
    {
        get
        {
            return isLost.Value || IsClear;
        }
    }

    [Header("Events")]
    [SerializeField] private GameEvent OnBeginLevel;
    [Tooltip("Show UI, Save Data")]
    [SerializeField] private GameEvent OnLose;
    [Tooltip("Show UI, Unlock new Level, Save Data")]
    [SerializeField] private GameEvent OnClear;

    [Header("References")]
    public BoolReference playable;
    [SerializeField] private Transform paddleSpawnPosition;
    [Tooltip("Triggered when ball hit dead region")]
    [SerializeField] private BoolReference isLost;
    [SerializeField] private LevelBuilder levelBuilder;
    [SerializeField] private IntReference currentNumberOfBlock;

    [Header("Delay Duration")]
    [SerializeField] private float startDelay;
    [SerializeField] private float endDelay;    

    public void CreateGamePlayObject()
    {
        usingPaddle = GameState.Instance.inventorySystem.paddleInventory.SelectingObject.
            CreateObject(paddleSpawnPosition).GetComponent<PaddleControl>();

        usingBall = GameState.Instance.inventorySystem.ballInventory.SelectingObject.
            CreateObject(usingPaddle.ballLockPosition.position).GetComponent<Ball>();

        usingPaddle.ball = usingBall;
    }

    private void Awake()
    {
        ResetReferences();
        levelBuilder.ConstructLevel(LevelDataBase.Instance.currentLevelId);
        CreateGamePlayObject();
    }

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private void ResetReferences()
    {
        currentNumberOfBlock.Value = default;
        isLost.Value = default;
    }

    private IEnumerator GameLoop()
    {
        // Start off by running StartingLevel coroutine and not return until it finishes
        yield return StartCoroutine(StartingLevel());

        // Once StartingLevel finished, run PlayingLevel coroutine and not return until it finishes, then run EndingLevel coroutine
        yield return StartCoroutine(PlayingLevel());

        yield return StartCoroutine((EndingLevel()));
    }

    private IEnumerator StartingLevel()
    {
        OnBeginLevel.RaiseEvent();
        playable.Value = false;

        yield return new WaitForSeconds(startDelay);
    }

    private IEnumerator PlayingLevel()
    {
        playable.Value = true;

        while (!CanEndGame)
        {
            yield return null;
        }
    }

    private IEnumerator EndingLevel()
    {
        HandleFinishLevelEvent();

        playable.Value = false;

        yield return new WaitForSeconds(endDelay);
    }

    private void HandleFinishLevelEvent()
    {
        if (isLost.Value)
            OnLose.RaiseEvent();
        else if (IsClear) OnClear.RaiseEvent();
    }
}
