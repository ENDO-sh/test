using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public  class UIScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //プレイヤのUI
    GameObject tipText;
    GameObject tipText2;
    GameObject BetTipText; 
    GameObject BetTipText2;
    GameObject betText;
    GameObject Turn;

    public  static int tip;　//所持チップ（ｐ１）
    public static int tip2;  //所持チップ　（ｐ２）
    int bet; //UI選択時に増える減るベット数
    int currentBet; //掛けるチップ
    int choiceBet;　//掛け数を決めた時のチップ数
    int choiceBet2;
    int currentTurn; //現在ターン
    int MaxTurn; //最大ターン

    public enum State //状態
    {
        PlayerTurn,     // プレイヤーの入力待ち(p1)
        PlayerTurn2,     // プレイヤーの入力待ち(p2)
        Game,           //ゲームスタート
        Win,             //勝利(p1)
        Win2,           //勝利 (p2)
        Push,           // 同点
        Push2,         //同点(p2)
        Lose,            // 敗北
        Lose2           //敗北(p2)
    }
    public State currentState;
    public State currentState2;

    void Start()
    {
        this.tipText = GameObject.Find("tip");
        this.tipText2 = GameObject.Find("tip2");
        this.BetTipText= GameObject.Find("BetTip");
        this.BetTipText2 = GameObject.Find("BetTip2");
        this.betText = GameObject.Find("Bet");
        this.Turn = GameObject.Find("Turn");

        this.currentState = State.PlayerTurn; //初期状態 
        this.betText.SetActive(false);

        tip = 200; //初期チップ数
        tip2 = 200;

        this.bet = 10; //ベットする時のかけ数。
        this.choiceBet = 0; //ベット数の決定
        this.choiceBet2 = 0;
        this.currentBet = 0;　//ベット数の現在
        this.currentTurn = 0;　//現在ターン（左）
        this.MaxTurn = 5; //最大ターン数（右側）
    }

    // Update is called once per frame
    void Update()
    {
        Render();

        if (this.currentState == State.PlayerTurn||this.currentState==State.PlayerTurn2)
        {
            this.betText.SetActive(true);
        }
    }


    public void Render()
    {
        //自分のもっているチップ数(p1)
        this.tipText.GetComponent<TextMeshProUGUI>().text =
             tip.ToString() + "tip";
        //自分のもっているチップ数(p2)
        this.tipText2.GetComponent<TextMeshProUGUI>().text =
             tip2.ToString() + "tip";
        //ベットしたチップ数(p1)
        this.BetTipText.GetComponent<TextMeshProUGUI>().text =
            choiceBet.ToString() + "bet";
        //ベットしたチップ数(p2)
        this.BetTipText2.GetComponent<TextMeshProUGUI>().text =
            choiceBet2.ToString() + "bet";
        //かけるチップ数（ベット）
        this.betText.GetComponent<TextMeshProUGUI>().text =
            currentBet.ToString() + "tip";
        //ターン数
        this.Turn.GetComponent<TextMeshProUGUI>().text =
            currentTurn.ToString() + "/"+MaxTurn.ToString();
    }

    //ベット数＋
    public void OnClickUp()
    {
        if (this.currentState == State.PlayerTurn)
        {
            if (tip > 0 )
            {
                currentBet += bet;
                tip -= bet;
            }
        }
        if(this.currentState == State.PlayerTurn2)
        {
            if (tip2 > 0)
            {
                currentBet += bet;
                tip2 -= bet;
            }
        }
    }
    //ベット数ー
    public void OnClickDown()
    {
        if (this.currentState == State.PlayerTurn)
        {
              if (currentBet != 0) 
             {
              currentBet -= bet;
              tip += bet;
              };
        }
        if (this.currentState == State.PlayerTurn2)
        {
            if (currentBet != 0)
            {
                currentBet -= bet;
                tip2 += bet;
            }
            ;
        }
    }
    //ベット数決定
    public void OnClick()
    {
        if(this.currentState == State.PlayerTurn)
        {
            choiceBet = currentBet;
            currentBet = 0;
            this.currentState = State.PlayerTurn2;
        }
        if (this.currentState == State.PlayerTurn2)
        {
            choiceBet2 = currentBet;
            currentBet = 0;
            currentTurn += 1;
            this.betText.SetActive(false);//非表示
        }
      
       // Result(State.Lose);
        //Result(State.Win2);
      
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

            case State.Win2:
                tip2 += choiceBet * 2;
                break;

            case State.Push:
                tip += choiceBet;
                break;

            case State.Push2:
                tip2 += choiceBet;
                break;

            case State.Lose:
                break;

            case State.Lose2:
                break;
        }
        if (this.currentState == State.Win2 || this.currentState == State.Push2 || this.currentState == State.Lose2)
        {
             choiceBet = 0;
            this.currentState = State.PlayerTurn;
        }
    }
}
