using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class GameLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        status Status = gamePlayer.GetComponent<status>();
       
        Status.playerName = lobby.playerName;
        Status.color = lobby.playerColor;
        Status.score = 0;
        Status.lifeCount = 3;
    }
}
