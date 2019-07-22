pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'PingPongGame' by AppCodeGen utility
//---------------------------------------------


import "./PingPongGame.sol";
contract WorkbenchBase {
    event WorkbenchContractCreated(string applicationName, string workflowName, address originatingAddress);
    event WorkbenchContractUpdated(string applicationName, string workflowName, string action, address originatingAddress);
    
    string internal ApplicationName;
    string internal WorkflowName;

    function WorkbenchBase(string applicationName, string workflowName) public {
        ApplicationName = applicationName;
        WorkflowName = workflowName;
    }

    function ContractCreated() public {
        WorkbenchContractCreated(ApplicationName, WorkflowName, msg.sender);
    }

    function ContractUpdated(string action) public {
        WorkbenchContractUpdated(ApplicationName, WorkflowName, action, msg.sender);
    }
}

//
// The wrapper contract Starter_AzureBlockchainWorkBench invokes functions from Starter.
// The inheritance order of Starter_AzureBlockchainWorkBench ensures that functions and variables in Starter 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract Starter_AzureBlockchainWorkBench is WorkbenchBase, Starter {
    // 
    // Constructor
    // 
    function Starter_AzureBlockchainWorkBench(string gameName)
            WorkbenchBase("PingPongGame", "Starter")
            Starter(gameName)
            public {

        // Check postconditions and access control for constructor of Starter


        // Constructor should transition the state to StartState
        assert(State == StateType.GameProvisioned);

        // Signals successful creation of contract Starter 
        WorkbenchBase.ContractCreated();
    }



    ////////////////////////////////////////////
    // Workbench Transitions    
    //    
    // Naming convention of transition functions:
    //    Transition_<CurrentState>_Number_<TransitionNumberFromCurrentState>_<FunctionNameOnTransition>
    //    Transition function arguments same as underlying function
    //    
    //////////////////////////////////////////////


    function Transition_GameProvisioned_Number_0_StartPingPong (int256 pingPongTimes) public {

        // Transition preconditions
        require(State == StateType.GameProvisioned);
        require(msg.sender == GameStarter);

        // Call overridden function StartPingPong in this contract
        StartPingPong(pingPongTimes);

        // Transition postconditions
        assert(State == StateType.Pingponging);
    }

    function StartPingPong(int256 pingPongTimes) public {

        // Placeholder for function preconditions

        // Call function StartPingPong of Starter
        Starter.StartPingPong(pingPongTimes);

        // Placeholder for function postconditions

        // Signals successful execution of function StartPingPong 
        WorkbenchBase.ContractUpdated("StartPingPong");
    }

    function Pong(int256 currentPingPongTimes) public {

        // Placeholder for function preconditions

        // Call function Pong of Starter
        Starter.Pong(currentPingPongTimes);

        // Placeholder for function postconditions

        // Signals successful execution of function Pong 
        WorkbenchBase.ContractUpdated("Pong");
    }

    function FinishGame() public {

        // Placeholder for function preconditions

        // Call function FinishGame of Starter
        Starter.FinishGame();

        // Placeholder for function postconditions

        // Signals successful execution of function FinishGame 
        WorkbenchBase.ContractUpdated("FinishGame");
    }
}
//
// The wrapper contract Player_AzureBlockchainWorkBench invokes functions from Player.
// The inheritance order of Player_AzureBlockchainWorkBench ensures that functions and variables in Player 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract Player_AzureBlockchainWorkBench is WorkbenchBase, Player {
    // 
    // Constructor
    // 
    function Player_AzureBlockchainWorkBench(string pingPongGameName)
            WorkbenchBase("PingPongGame", "Player")
            Player(pingPongGameName)
            public {

        // Check postconditions and access control for constructor of Player


        // Constructor should transition the state to StartState
        assert(State == StateType.PingpongPlayerCreated);

        // Signals successful creation of contract Player 
        WorkbenchBase.ContractCreated();
    }



    ////////////////////////////////////////////
    // Workbench Transitions    
    //    
    // Naming convention of transition functions:
    //    Transition_<CurrentState>_Number_<TransitionNumberFromCurrentState>_<FunctionNameOnTransition>
    //    Transition function arguments same as underlying function
    //    
    //////////////////////////////////////////////

    function Ping(int256 currentPingPongTimes) public {

        // Placeholder for function preconditions

        // Call function Ping of Player
        Player.Ping(currentPingPongTimes);

        // Placeholder for function postconditions

        // Signals successful execution of function Ping 
        WorkbenchBase.ContractUpdated("Ping");
    }

    function FinishGame() public {

        // Placeholder for function preconditions

        // Call function FinishGame of Player
        Player.FinishGame();

        // Placeholder for function postconditions

        // Signals successful execution of function FinishGame 
        WorkbenchBase.ContractUpdated("FinishGame");
    }
}
