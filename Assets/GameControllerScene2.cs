using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameControllerScene2 : MonoBehaviour
{
    [SerializeField]
    public Text message;

    [SerializeField]
    Transform buttonPanel;

    [SerializeField]
    Button optionButton;

    public List<string> Texts;
    int index = 0;

    class Scenario
    {
        public string ScenarioID;
        public List<string> Texts = new List<string>();
        public List<Option> Options = new List<Option>();
        public string NextScenarioID;
    }

    class Option
    {
        public string Text;
        public Action Action;
    }

    Scenario currentScenario;
    List<Scenario> scenarios = new List<Scenario>();

    // Start is called before the first frame update
    void Start()
    {
        Scenario scenario01 = new Scenario()
        {
            ScenarioID = "scenario01",
            Texts = new List<string>()
            {
                "「そろそろ暗くなってきたなぁ」"
            },
            NextScenarioID = "scenario02"
        };
        Scenario scenario02 = new Scenario()
        {
            ScenarioID = "scenario02",
            Texts = new List<string>()
            {
                "どうする？"
            },
            Options = new List<Option>
            {
                new Option()
                {
                    Text = "家に帰る",
                    Action = GoHome
                },
                new Option()
                {
                    Text = "まだ散歩する",
                    Action = WalkAround
                }
            }
        };
        scenarios.Add(scenario02);
        SetScenario(scenario01);

    }

    void SetScenario(Scenario scenario)
    {
        currentScenario = scenario;
        message.text = currentScenario.Texts[0];
        if(currentScenario.Options.Count > 0)
        {
            SetNextMessage();
        }
    }

    void SetNextMessage()
    {
        if(currentScenario.Texts.Count > index + 1)
        {
            index++;
            message.text = currentScenario.Texts[index];
        }
        else
        {
            ExitScenario();
        }
    }

    void ExitScenario()
    {
        index = 0;
        if(currentScenario.Options.Count > 0)
        {
            SetButtons();
        }
        else
        {
            message.text = "";
            Scenario nextScenario = null;

            foreach(Scenario scenario in scenarios)
            {
                if(scenario.ScenarioID == currentScenario.NextScenarioID)
                {
                    nextScenario = scenario;
                }
            }

            if(nextScenario != null)
            {
                SetScenario(nextScenario);
            }
            else
            {
                currentScenario = null;
            }
        }
    }

    void SetButtons()
    {
        foreach(Option o in currentScenario.Options)
        {
            Button b = Instantiate(optionButton);
            Text text = b.GetComponentInChildren<Text>();
            text.text = o.Text;
            b.onClick.AddListener(() => o.Action());
            b.onClick.AddListener(() => ClearButtons());
            b.transform.SetParent(buttonPanel, false);
        }
    }

    void ClearButtons()
    {
        foreach(Transform t in buttonPanel)
        {
            Destroy(t.gameObject);
        }
    }

    public void WalkAround()
    {
        var scenario = new Scenario();
        scenario.NextScenarioID = "scenario02";
        scenario.Texts.Add("「綺麗な夕焼けだなぁ」");
        scenario.Texts.Add("「そろそろおなかが空いたなぁ」");
        SetScenario(scenario);
    }

    public void GoHome()
    {
        var scenario = new Scenario();
        scenario.Texts.Add("「お家に帰らなきゃ」");
        scenario.Texts.Add("「今日の夜ご飯は何かな～♪」");
        SetScenario(scenario);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScenario != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(buttonPanel.GetComponentsInChildren<Button>().Length < 1)
                {
                    SetNextMessage();
                }
            }
        }
        
    }
}
