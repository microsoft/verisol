pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'FrequentFlyerRewardsCalculator' by AppCodeGen utility
//---------------------------------------------


import "./FrequentFlyerRewardsCalculator.sol";
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
// The wrapper contract FrequentFlyerRewardsCalculator_AzureBlockchainWorkBench invokes functions from FrequentFlyerRewardsCalculator.
// The inheritance order of FrequentFlyerRewardsCalculator_AzureBlockchainWorkBench ensures that functions and variables in FrequentFlyerRewardsCalculator 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract FrequentFlyerRewardsCalculator_AzureBlockchainWorkBench is WorkbenchBase, FrequentFlyerRewardsCalculator {
    // 
    // Constructor
    // 
    function FrequentFlyerRewardsCalculator_AzureBlockchainWorkBench(address flyer, int256 rewardsPerMile)
            WorkbenchBase("FrequentFlyerRewardsCalculator", "FrequentFlyerRewardsCalculator")
            FrequentFlyerRewardsCalculator(flyer, rewardsPerMile)
            public {

        // Check postconditions and access control for constructor of FrequentFlyerRewardsCalculator


        // Constructor should transition the state to StartState
        assert(State == StateType.SetFlyerAndReward);

        // Signals successful creation of contract FrequentFlyerRewardsCalculator 
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


    function Transition_SetFlyerAndReward_Number_0_AddMiles (int256[] miles) public {

        // Transition preconditions
        require(State == StateType.SetFlyerAndReward);
        require(msg.sender == Flyer);

        // Call overridden function AddMiles in this contract
        AddMiles(miles);

        // Transition postconditions
        assert(State == StateType.MilesAdded);
    }


    function Transition_MilesAdded_Number_0_AddMiles (int256[] miles) public {

        // Transition preconditions
        require(State == StateType.MilesAdded);
        require(msg.sender == Flyer);

        // Call overridden function AddMiles in this contract
        AddMiles(miles);

        // Transition postconditions
        assert(State == StateType.MilesAdded);
    }

    function AddMiles(int256[] miles) public {

        // Placeholder for function preconditions

        // Call function AddMiles of FrequentFlyerRewardsCalculator
        FrequentFlyerRewardsCalculator.AddMiles(miles);

        // Placeholder for function postconditions

        // Signals successful execution of function AddMiles 
        WorkbenchBase.ContractUpdated("AddMiles");
    }
}
