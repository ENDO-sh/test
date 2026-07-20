using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public  class UIScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject tipText;
    GameObject betText;
    GameObject BetTip;
    GameObject Turn;
    public  static int tip;
    int bet;
    int currentBet;
    int choiceBet;
    int currentTurn;
    int MaxTurn;

    public enum State //状態
    {
        PlayerTurn,     // プレイヤーの入力待ち
        Game,           //ゲームスタート
        Win,            // 勝利
        Push,           // 同点
        Lose            // 敗北
    }
    public State currentState;

    void Start()
    {
        this.tipText = GameObject.Find("tip");
        this.betText = GameObject.Find("Bet");
        this.BetTip = GameObject.Find("BetTip");
        this.Turn = GameObject.Find("Turn");

        this.currentState = State.PlayerTurn; //初期状態
        this.betText.SetActive(false);

        tip = 200; //初期チップ数
        this.bet = 10; //ベットする時のかけ数。
        this.choiceBet = 0; //ベット数の決定
        this.currentBet = 0;　//ベット数の現在
        this.currentTurn = 0;
        this.MaxTurn = 5; //最大ターン数（右側）
    }

    // Update is called once per frame
    void Update()
    {
        Render();

        if (this.currentState == State.PlayerTurn)
        {
            this.betText.SetActive(true);
        }
    }


    public void Render()
    {
        //自分のもっているチップ数
        this.tipText.GetComponent<TextMeshProUGUI>().text =
             tip.ToString() + "tip";
    　   //かけるチップ数（ベット）
        this.betText.GetComponent<TextMeshProUGUI>().text =
            currentBet.ToString() + "tip";
        //ベットしたチップ数
        this.BetTip.GetComponent<TextMeshProUGUI>().text =
            choiceBet.ToString() + "bet";
        //ターン数
        this.Turn.GetComponent<TextMeshProUGUI>().text =
            currentTurn.ToString() + "/"+MaxTurn.ToString();

    }



    //ベット数＋
    public void OnClickUp()
    {
            currentBet += bet;
            tip -= bet;
    }
    //ベット数ー
    public void OnClickDown()
    {
            if (currentBet != 0)
            {
                currentBet -= bet;
                tip += bet;
            }
    }
    //ベット数決定
    public void OnClick()
    {
            choiceBet = currentBet;
            currentBet = 0; 
            currentTurn += 1;
            this.betText.SetActive(false); //非表示
    }

    //勝敗決定時、チップ配分、ターン数
    public void Result(State TipState)
    {
        this.currentState = TipState;
        switch (this.currentState)
        {
            case State.Win:
                tip += choiceBet * 2;
                break;

            case State.Push:
                tip += choiceBet;
                break;

            case State.Lose:
                break;
        }
        choiceBet = 0;
        this.currentState=State.PlayerTurn;
    }
}
