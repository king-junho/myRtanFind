using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public GameObject card;
    public Text timeTxt;
    float time = 30.0f;
    public static gameManager I;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endPanel;
    public AudioClip match;
    public AudioSource audioSource;
    //새로 추가할 내용
    public int attempt = 0;
    public Text tryTxt;
    public bool isEnd = false;
    public GameObject timeAnim;

    void Awake()
    {
        I = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for(int i=0; i<16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;


            float x = (i / 4) * 1.4f -2.1f;
            float y = (i % 4) * 1.4f -3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if(time<10.0f)
        {
            timeAnim.GetComponent<timeTxt>().timeLimit();
        }

        if (time < 0.0f)
        {
            time = 0f;
            if (!isEnd)
            {
                endGame();
            }

        }
    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft==2)
            {
                endGame();
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
            time -= 2.0f;
        }

        firstCard = null;
        secondCard = null;
    }

    public void endGame()
    {
        endPanel.SetActive(true);
        Time.timeScale = 0f;
        timeTxt.text = time.ToString("N2");
        tryTxt.text = attempt.ToString();;
        GameObject.Find("cards").SetActive(false);
        isEnd = true;       //게임이 끝나면 update에서 더이상 endGame()호출을 못하게끔 하기 위해 
    }
}
