pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'SimpleMarketplace' by AppCodeGen utility
//---------------------------------------------


import "./SimpleMarketplace.sol";
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
// The wrapper contract SimpleMarketplace_AzureBlockchainWorkBench invokes functions from SimpleMarketplace.
// The inheritance order of SimpleMarketplace_AzureBlockchainWorkBench ensures that functions and variables in SimpleMarketplace 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract SimpleMarketplace_AzureBlockchainWorkBench is WorkbenchBase, SimpleMarketplace {
    // 
    // Constructor
    // 
    function SimpleMarketplace_AzureBlockchainWorkBench(string memory description, int256 price)
            WorkbenchBase("SimpleMarketplace", "SimpleMarketplace")
            SimpleMarketplace(description, price)
            public {

        // Check postconditions and access control for constructor of SimpleMarketplace


        // Constructor should transition the state to StartState
        assert(State == StateType.ItemAvailable);

        // Signals successful creation of contract SimpleMarketplace 
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


    function Transition_ItemAvailable_Number_0_MakeOffer (int256 offerPrice) public {

        // Transition preconditions
        require(State == StateType.ItemAvailable);

        // Call overridden function MakeOffer in this contract
        MakeOffer(offerPrice);

        // Transition postconditions
        assert(State == StateType.OfferPlaced);
    }


    function Transition_OfferPlaced_Number_0_AcceptOffer () public {

        // Transition preconditions
        require(State == StateType.OfferPlaced);
        require(msg.sender == InstanceOwner);

        // Call overridden function AcceptOffer in this contract
        AcceptOffer();

        // Transition postconditions
        assert(State == StateType.Accepted);
    }


    function Transition_OfferPlaced_Number_1_Reject () public {

        // Transition preconditions
        require(State == StateType.OfferPlaced);
        require(msg.sender == InstanceOwner);

        // Call overridden function Reject in this contract
        Reject();

        // Transition postconditions
        assert(State == StateType.ItemAvailable);
    }

    function MakeOffer(int256 offerPrice) public {

        // Placeholder for function preconditions

        // Call function MakeOffer of SimpleMarketplace
        SimpleMarketplace.MakeOffer(offerPrice);

        // Placeholder for function postconditions

        // Signals successful execution of function MakeOffer 
        WorkbenchBase.ContractUpdated("MakeOffer");
    }

    function AcceptOffer() public {

        // Placeholder for function preconditions

        // Call function AcceptOffer of SimpleMarketplace
        SimpleMarketplace.AcceptOffer();

        // Placeholder for function postconditions

        // Signals successful execution of function AcceptOffer 
        WorkbenchBase.ContractUpdated("AcceptOffer");
    }

    function Reject() public {

        // Placeholder for function preconditions

        // Call function Reject of SimpleMarketplace
        SimpleMarketplace.Reject();

        // Placeholder for function postconditions

        // Signals successful execution of function Reject 
        WorkbenchBase.ContractUpdated("Reject");
    }
}
