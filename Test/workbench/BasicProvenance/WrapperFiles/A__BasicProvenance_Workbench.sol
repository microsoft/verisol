pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'BasicProvenance' by AppCodeGen utility
//---------------------------------------------


import "./BasicProvenance.sol";
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
// The wrapper contract BasicProvenance_AzureBlockchainWorkBench invokes functions from BasicProvenance.
// The inheritance order of BasicProvenance_AzureBlockchainWorkBench ensures that functions and variables in BasicProvenance 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract BasicProvenance_AzureBlockchainWorkBench is WorkbenchBase, BasicProvenance {
    // 
    // Constructor
    // 
    function BasicProvenance_AzureBlockchainWorkBench(address supplyChainOwner, address supplyChainObserver)
            WorkbenchBase("BasicProvenance", "BasicProvenance")
            BasicProvenance(supplyChainOwner, supplyChainObserver)
            public {

        // Check postconditions and access control for constructor of BasicProvenance


        // Constructor should transition the state to StartState
        assert(State == StateType.Created);

        // Signals successful creation of contract BasicProvenance 
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


    function Transition_Created_Number_0_TransferResponsibility (address newCounterparty) public {

        // Transition preconditions
        require(State == StateType.Created);
        require(msg.sender == InitiatingCounterparty);

        // Call overridden function TransferResponsibility in this contract
        TransferResponsibility(newCounterparty);

        // Transition postconditions
        assert(State == StateType.InTransit);
    }


    function Transition_InTransit_Number_0_TransferResponsibility (address newCounterparty) public {

        // Transition preconditions
        require(State == StateType.InTransit);
        require(msg.sender == Counterparty);

        // Call overridden function TransferResponsibility in this contract
        TransferResponsibility(newCounterparty);

        // Transition postconditions
        assert(State == StateType.InTransit);
    }


    function Transition_InTransit_Number_1_Complete () public {

        // Transition preconditions
        require(State == StateType.InTransit);

        // Call overridden function Complete in this contract
        Complete();

        // Transition postconditions
        assert(State == StateType.Completed);
    }

    function TransferResponsibility(address newCounterparty) public {

        // Placeholder for function preconditions

        // Call function TransferResponsibility of BasicProvenance
        BasicProvenance.TransferResponsibility(newCounterparty);

        // Placeholder for function postconditions

        // Signals successful execution of function TransferResponsibility 
        WorkbenchBase.ContractUpdated("TransferResponsibility");
    }

    function Complete() public {

        // Placeholder for function preconditions

        // Call function Complete of BasicProvenance
        BasicProvenance.Complete();

        // Placeholder for function postconditions

        // Signals successful execution of function Complete 
        WorkbenchBase.ContractUpdated("Complete");
    }
}
