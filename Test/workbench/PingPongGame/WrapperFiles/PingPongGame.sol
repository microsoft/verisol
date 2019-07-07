pragma solidity ^0.4.24;

contract Starter
{
    enum StateType { GameProvisioned, Pingponging, GameFinished}

    StateType public State;

    string public PingPongGameName;
    address public GameStarter;
    address public GamePlayer;
    int public PingPongTimes;

    // constructor (string gameName) public{
    function Starter (string gameName) public{
        PingPongGameName = gameName;
        GameStarter = msg.sender;

        GamePlayer = new Player_AzureBlockchainWorkBench(PingPongGameName); // Replaced new A(..) with new A_AzureBlockchainWorkBench(..) 

        State = StateType.GameProvisioned;
    }

    function StartPingPong(int pingPongTimes) public 
    {
        PingPongTimes = pingPongTimes;

        Player player = Player(GamePlayer);
        State = StateType.Pingponging;

        player.Ping(pingPongTimes);
    }

    function Pong(int currentPingPongTimes) public 
    {
        currentPingPongTimes = currentPingPongTimes - 1;

        Player player = Player(GamePlayer);
        if(currentPingPongTimes > 0)
        {
            State = StateType.Pingponging;
            player.Ping(currentPingPongTimes);
        }
        else
        {
            State = StateType.GameFinished;
            player.FinishGame();
        }
    }

    function FinishGame() public
    {
        State = StateType.GameFinished;
    }
}

contract Player
{
    enum StateType {PingpongPlayerCreated, PingPonging, GameFinished}

    StateType public State;

    address public GameStarter;
    string public PingPongGameName;

    // constructor (string pingPongGameName) public {
    function Player(string pingPongGameName) public {
        GameStarter = msg.sender;
        PingPongGameName = pingPongGameName;

        State = StateType.PingpongPlayerCreated;
    }

    function Ping(int currentPingPongTimes) public 
    {
        currentPingPongTimes = currentPingPongTimes - 1;

        Starter starter = Starter(msg.sender);
        if(currentPingPongTimes > 0)
        {
            State = StateType.PingPonging;
            starter.Pong(currentPingPongTimes);
        }
        else
        {
            State = StateType.GameFinished;
            starter.FinishGame();
        }
    }

    function FinishGame() public
    {
        State = StateType.GameFinished;
    }
}

import "./A__PingPongGame_Workbench.sol"; 
