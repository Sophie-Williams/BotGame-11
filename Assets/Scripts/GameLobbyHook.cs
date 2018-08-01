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

       /*if(lobby.playerName.StartsWith("BOT:"))
        {
            switch(lobby.playerName.Substring(4).ToLower())
            {
                case "firstbot": gamePlayer.AddComponent<firstBot>();
                    Destroy(gamePlayer.GetComponent<controlls>());
                    break;
                default:
                    Debug.Log("The Bot was not found. Botname: " + lobby.playerName.Substring(4).ToLower());
                    break;
                    
                }
        }*/

        Status.playerName = lobby.playerName;
        Status.color = lobby.playerColor;
        Status.score = 0;
        Status.lifeCount = 3;
    }
}
