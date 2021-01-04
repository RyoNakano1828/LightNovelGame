using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public Text message;

    public List<string> Texts;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Texts = new List<string>()
        {
            "梅雨が明けて夏が来た",
            "「いい天気だな、散歩に出かけよう♪」"
        };
        message.text = Texts[index];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if(index == Texts.Count)
            {
                SceneManager.LoadScene("Scene2");
            }
            index %= Texts.Count;
            message.text = Texts[index];
        }
        
    }
}
