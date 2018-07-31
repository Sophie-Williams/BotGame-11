using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;

public class gameManagerScript : NetworkBehaviour
{

    static public List<status> sCharacters = new List<status>();
    static public gameManagerScript sInstance = null;

    protected bool _running = true;

    public GameObject uiScoreZone;
    public Font uiScoreFont;

    void Awake()
    {
        sInstance = this;
    }

    void Start()
    {
        for (int i = 0; i < sCharacters.Count; ++i)
        {
            sCharacters[i].Init();
        }
    }

    IEnumerator ReturnToLoby()
    {
        _running = false;
        yield return new WaitForSeconds(3.0f);
        LobbyManager.s_Singleton.ServerReturnToLobby();
    }


    // Update is called once per frame
    void Update()
    {
        if (!_running || sCharacters.Count == 1)
            return;

        int aliveCount = 0;
        for (int i = 0; i < sCharacters.Count; ++i)
        {
            if(sCharacters[i].isAlive())
                aliveCount += 1;
        }

        if (aliveCount < 2)
        {
            StartCoroutine(ReturnToLoby());
        }
    }
}
