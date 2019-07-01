pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'DefectiveComponentCounter' by AppCodeGen utility
//---------------------------------------------


import "./DefectiveComponentCounter.sol";
contract WorkbenchBase {
    event WorkbenchContractCreated(string applicationName, string workflowName, address originatingAddress);
    event WorkbenchContractUpdated(string applicationName, string workflowName, string action, address originatingAddress);
    
    string internal ApplicationName;
    string internal WorkflowName;

    constructor(string applicationName, string workflowName) public {
        ApplicationName = applicationName;
        WorkflowName = workflowName;
    }

    function ContractCreated() public {
        emit WorkbenchContractCreated(ApplicationName, WorkflowName, msg.sender);
    }

    function ContractUpdated(string action) public {
        emit WorkbenchContractUpdated(ApplicationName, WorkflowName, action, msg.sender);
    }
}

//
// The wrapper contract DefectiveComponentCounter_AzureBlockchainWorkBench invokes functions from DefectiveComponentCounter.
// The inheritance order of DefectiveComponentCounter_AzureBlockchainWorkBench ensures that functions and variables in DefectiveComponentCounter 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract DefectiveComponentCounter_AzureBlockchainWorkBench is WorkbenchBase, DefectiveComponentCounter {
    // 
    // Constructor
    // 
    function DefectiveComponentCounter_AzureBlockchainWorkBench(int256[12] defectiveComponentsCount)
            WorkbenchBase("DefectiveComponentCounter", "DefectiveComponentCounter")
            DefectiveComponentCounter(defectiveComponentsCount)
            public {

        // Check postconditions and access control for constructor of DefectiveComponentCounter


        // Constructor should transition the state to StartState
        assert(State == StateType.Create);

        // Signals successful creation of contract DefectiveComponentCounter 
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


    function Transition_Create_Number_0_ComputeTotal () public {

        // Transition preconditions
        require(State == StateType.Create);
        require(msg.sender == Manufacturer);

        // Call overridden function ComputeTotal in this contract
        ComputeTotal();

        // Transition postconditions
        assert(State == StateType.ComputeTotal);
    }

    function ComputeTotal() public {

        // Placeholder for function preconditions

        // Call function ComputeTotal of DefectiveComponentCounter
        DefectiveComponentCounter.ComputeTotal();

        // Placeholder for function postconditions

        // Signals successful execution of function ComputeTotal 
        WorkbenchBase.ContractUpdated("ComputeTotal");
    }
}
