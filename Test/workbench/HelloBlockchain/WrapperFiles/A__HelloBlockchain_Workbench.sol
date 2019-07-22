pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'HelloBlockchain' by AppCodeGen utility
//---------------------------------------------


import "./HelloBlockchain.sol";
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
// The wrapper contract HelloBlockchain_AzureBlockchainWorkBench invokes functions from HelloBlockchain.
// The inheritance order of HelloBlockchain_AzureBlockchainWorkBench ensures that functions and variables in HelloBlockchain 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract HelloBlockchain_AzureBlockchainWorkBench is WorkbenchBase, HelloBlockchain {
    // 
    // Constructor
    // 
    function HelloBlockchain_AzureBlockchainWorkBench(string memory message)
            WorkbenchBase("HelloBlockchain", "HelloBlockchain")
            HelloBlockchain(message)
            public {

        // Check postconditions and access control for constructor of HelloBlockchain


        // Constructor should transition the state to StartState
        assert(State == StateType.Request);

        // Signals successful creation of contract HelloBlockchain 
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


    function Transition_Request_Number_0_SendResponse (string memory responseMessage) public {

        // Transition preconditions
        require(State == StateType.Request);

        // Call overridden function SendResponse in this contract
        SendResponse(responseMessage);

        // Transition postconditions
        assert(State == StateType.Respond);
    }


    function Transition_Respond_Number_0_SendRequest (string memory requestMessage) public {

        // Transition preconditions
        require(State == StateType.Respond);
        require(msg.sender == Requestor);

        // Call overridden function SendRequest in this contract
        SendRequest(requestMessage);

        // Transition postconditions
        assert(State == StateType.Request);
    }

    function SendRequest(string memory requestMessage) public {

        // Placeholder for function preconditions

        // Call function SendRequest of HelloBlockchain
        HelloBlockchain.SendRequest(requestMessage);

        // Placeholder for function postconditions

        // Signals successful execution of function SendRequest 
        WorkbenchBase.ContractUpdated("SendRequest");
    }

    function SendResponse(string memory responseMessage) public {

        // Placeholder for function preconditions

        // Call function SendResponse of HelloBlockchain
        HelloBlockchain.SendResponse(responseMessage);

        // Placeholder for function postconditions

        // Signals successful execution of function SendResponse 
        WorkbenchBase.ContractUpdated("SendResponse");
    }
}
