pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'RoomThermostat' by AppCodeGen utility
//---------------------------------------------


import "./RoomThermostat.sol";
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
// The wrapper contract RoomThermostat_AzureBlockchainWorkBench invokes functions from RoomThermostat.
// The inheritance order of RoomThermostat_AzureBlockchainWorkBench ensures that functions and variables in RoomThermostat 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract RoomThermostat_AzureBlockchainWorkBench is WorkbenchBase, RoomThermostat {
    // 
    // Constructor
    // 
    function RoomThermostat_AzureBlockchainWorkBench(address thermostatInstaller, address thermostatUser)
            WorkbenchBase("RoomThermostat", "RoomThermostat")
            RoomThermostat(thermostatInstaller, thermostatUser)
            public {

        // Check postconditions and access control for constructor of RoomThermostat


        // Constructor should transition the state to StartState
        assert(State == StateType.Created);

        // Signals successful creation of contract RoomThermostat 
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


    function Transition_Created_Number_0_StartThermostat () public {

        // Transition preconditions
        require(State == StateType.Created);
        require(msg.sender == Installer);

        // Call overridden function StartThermostat in this contract
        StartThermostat();

        // Transition postconditions
        assert(State == StateType.InUse);
    }


    function Transition_InUse_Number_0_SetTargetTemperature (int256 targetTemperature) public {

        // Transition preconditions
        require(State == StateType.InUse);
        require(msg.sender == User);

        // Call overridden function SetTargetTemperature in this contract
        SetTargetTemperature(targetTemperature);

        // Transition postconditions
        assert(State == StateType.InUse);
    }


    function Transition_InUse_Number_1_SetMode (RoomThermostat.ModeEnum mode) public {

        // Transition preconditions
        require(State == StateType.InUse);
        require(msg.sender == User);

        // Call overridden function SetMode in this contract
        SetMode(mode);

        // Transition postconditions
        assert(State == StateType.InUse);
    }

    function StartThermostat() public {

        // Placeholder for function preconditions

        // Call function StartThermostat of RoomThermostat
        RoomThermostat.StartThermostat();

        // Placeholder for function postconditions

        // Signals successful execution of function StartThermostat 
        WorkbenchBase.ContractUpdated("StartThermostat");
    }

    function SetTargetTemperature(int256 targetTemperature) public {

        // Placeholder for function preconditions

        // Call function SetTargetTemperature of RoomThermostat
        RoomThermostat.SetTargetTemperature(targetTemperature);

        // Placeholder for function postconditions

        // Signals successful execution of function SetTargetTemperature 
        WorkbenchBase.ContractUpdated("SetTargetTemperature");
    }

    function SetMode(RoomThermostat.ModeEnum mode) public {

        // Placeholder for function preconditions

        // Call function SetMode of RoomThermostat
        RoomThermostat.SetMode(mode);

        // Placeholder for function postconditions

        // Signals successful execution of function SetMode 
        WorkbenchBase.ContractUpdated("SetMode");
    }
}
