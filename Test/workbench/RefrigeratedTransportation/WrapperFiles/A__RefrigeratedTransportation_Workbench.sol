pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'RefrigeratedTransportation' by AppCodeGen utility
//---------------------------------------------


import "./RefrigeratedTransportation.sol";
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
// The wrapper contract RefrigeratedTransportation_AzureBlockchainWorkBench invokes functions from RefrigeratedTransportation.
// The inheritance order of RefrigeratedTransportation_AzureBlockchainWorkBench ensures that functions and variables in RefrigeratedTransportation 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract RefrigeratedTransportation_AzureBlockchainWorkBench is WorkbenchBase, RefrigeratedTransportation {
    // 
    // Constructor
    // 
    function RefrigeratedTransportation_AzureBlockchainWorkBench(address device, address supplyChainOwner, address supplyChainObserver, int256 minHumidity, int256 maxHumidity, int256 minTemperature, int256 maxTemperature)
            WorkbenchBase("RefrigeratedTransportation", "RefrigeratedTransportation")
            RefrigeratedTransportation(device, supplyChainOwner, supplyChainObserver, minHumidity, maxHumidity, minTemperature, maxTemperature)
            public {

        // Check postconditions and access control for constructor of RefrigeratedTransportation


        // Constructor should transition the state to StartState
        assert(State == StateType.Created);

        // Signals successful creation of contract RefrigeratedTransportation 
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


    function Transition_Created_Number_1_IngestTelemetry (int256 humidity, int256 temperature, int256 timestamp) public {

        // Transition preconditions
        require(State == StateType.Created);
        require(msg.sender == Device);

        // Call overridden function IngestTelemetry in this contract
        IngestTelemetry(humidity, temperature, timestamp);

        // Transition postconditions
        assert(State == StateType.OutOfCompliance || State == StateType.Created);
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


    function Transition_InTransit_Number_1_IngestTelemetry (int256 humidity, int256 temperature, int256 timestamp) public {

        // Transition preconditions
        require(State == StateType.InTransit);
        require(msg.sender == Device);

        // Call overridden function IngestTelemetry in this contract
        IngestTelemetry(humidity, temperature, timestamp);

        // Transition postconditions
        assert(State == StateType.OutOfCompliance || State == StateType.InTransit);
    }


    function Transition_InTransit_Number_2_Complete () public {

        // Transition preconditions
        require(State == StateType.InTransit);
        require(msg.sender == Owner);

        // Call overridden function Complete in this contract
        Complete();

        // Transition postconditions
        assert(State == StateType.Completed);
    }

    function IngestTelemetry(int256 humidity, int256 temperature, int256 timestamp) public {

        // Placeholder for function preconditions

        // Call function IngestTelemetry of RefrigeratedTransportation
        RefrigeratedTransportation.IngestTelemetry(humidity, temperature, timestamp);

        // Placeholder for function postconditions

        // Signals successful execution of function IngestTelemetry 
        WorkbenchBase.ContractUpdated("IngestTelemetry");
    }

    function TransferResponsibility(address newCounterparty) public {

        // Placeholder for function preconditions

        // Call function TransferResponsibility of RefrigeratedTransportation
        RefrigeratedTransportation.TransferResponsibility(newCounterparty);

        // Placeholder for function postconditions

        // Signals successful execution of function TransferResponsibility 
        WorkbenchBase.ContractUpdated("TransferResponsibility");
    }

    function Complete() public {

        // Placeholder for function preconditions

        // Call function Complete of RefrigeratedTransportation
        RefrigeratedTransportation.Complete();

        // Placeholder for function postconditions

        // Signals successful execution of function Complete 
        WorkbenchBase.ContractUpdated("Complete");
    }
}
