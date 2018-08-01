using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class status : NetworkBehaviour
{

    //Network syncvar
    [SyncVar(hook = "OnScoreChanged")]
    public int score;
    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int lifeCount;

    [SerializeField]
    protected Text _scoreText;

    [SerializeField]
    [SyncVar]
    private bool alive = true;

    //hard to control WHEN Init is called (networking make order between object spawning non deterministic)
    //so we call init from multiple location (depending on what between spaceship & manager is created first).
    protected bool _wasInit = false;

    void Awake()
    {
        //register the spaceship in the gamemanager, that will allow to loop on it.
        gameManagerScript.sCharacters.Add(this);
    }

    void Update()
    {
        if(_wasInit == false)
        {
            Init();
        }
    }

    void Start()
    { 
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            //Debug.Log("Material.name: "+ r.material.name);
            if (r.material.name.Contains("PlayerShere") || r.material.name.Contains("Spieler"))
            {
                r.material.color = color;
            }
        }
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = isServer;
        }
        if (NetworkGameManager.sInstance != null)
        {//we MAY be awake late (see comment on _wasInit above), so if the instance is already there we init
            Init();
        }
    }

    public void Init()
    {
        if (_wasInit)
            return;

        GameObject scoreGO = new GameObject(playerName + "score");
        scoreGO.transform.SetParent(gameManagerScript.sInstance.uiScoreZone.transform, false);
        _scoreText = scoreGO.AddComponent<Text>();
        _scoreText.alignment = TextAnchor.MiddleCenter;
        _scoreText.font = gameManagerScript.sInstance.uiScoreFont;
        _scoreText.resizeTextForBestFit = true;
        _scoreText.color = color;
        _wasInit = true;

        transform.SetParent(GameObject.Find("Players").transform);
        gameManagerScript.sCharacters.Add(this);
        UpdateScoreLifeText();
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy() of status");
        gameManagerScript.sCharacters.Remove(this);
    }

    // --- Score & Life management & display
    void OnScoreChanged(int newValue)
    {
       // Debug.Log("OnScoreChanged");
        score = newValue;
        UpdateScoreLifeText();
    }


    void UpdateScoreLifeText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = playerName + "\nSCORE : " + score + "\nLIFE : ";
            for (int i = 1; i <= lifeCount; ++i)
                _scoreText.text += "X";
        }
    }

    public int getLifeCount()
    {
        return lifeCount;
    }


    [ClientRpc]
    public void RpcTakeDamage()
    {
        if (lifeCount > -1)
        {
           // Debug.Log("lifeCount reduced from " + lifeCount + " to " + lifeCount + "-1");
            lifeCount = lifeCount - 1;

            UpdateScoreLifeText();
        }
        if (lifeCount == 0)
        {
            died();
        }
    }



    public bool isAlive()
    {
        return alive;
    }

    /**
     *  Called once upon death (Health == 0)
     **/
    void died()
    {
        alive = false;
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }


}

