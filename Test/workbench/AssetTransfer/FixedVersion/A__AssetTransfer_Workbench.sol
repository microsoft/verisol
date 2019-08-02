pragma solidity >=0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'AssetTransfer' by AppCodeGen utility
//---------------------------------------------


import "./AssetTransfer.sol";
contract WorkbenchBase {
    event WorkbenchContractCreated(string applicationName, string  workflowName, address originatingAddress);
    event WorkbenchContractUpdated(string applicationName, string  workflowName, string  action, address originatingAddress);
    
    string internal ApplicationName;
    string internal WorkflowName;

    constructor(string memory applicationName, string memory workflowName) public {
        ApplicationName = applicationName;
        WorkflowName = workflowName;
    }

    function ContractCreated() public {
        emit WorkbenchContractCreated(ApplicationName, WorkflowName, msg.sender);
    }

    function ContractUpdated(string memory action) public {
        emit WorkbenchContractUpdated(ApplicationName, WorkflowName, action, msg.sender);
    }
}

//
// The wrapper contract AssetTransfer_AzureBlockchainWorkBench invokes functions from AssetTransfer.
// The inheritance order of AssetTransfer_AzureBlockchainWorkBench ensures that functions and variables in AssetTransfer 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract AssetTransfer_AzureBlockchainWorkBench is WorkbenchBase, AssetTransfer {
    // 
    // Constructor
    // 
    constructor(string memory description, uint256 price)
            WorkbenchBase("AssetTransfer", "AssetTransfer")
            AssetTransfer(description, price)
            public {

        // Check postconditions and access control for constructor of AssetTransfer


        // Constructor should transition the state to StartState
        assert(State == StateType.Active);

        // Signals successful creation of contract AssetTransfer 
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


    function Transition_Active_Number_0_Terminate () public {

        // Transition preconditions
        require(State == StateType.Active);
        require(msg.sender == InstanceOwner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_Active_Number_1_MakeOffer (address inspector, address appraiser, uint256 offerPrice) public {

        // Transition preconditions
        require(State == StateType.Active);

        // Call overridden function MakeOffer in this contract
        MakeOffer(inspector, appraiser, offerPrice);

        // Transition postconditions
        assert(State == StateType.OfferPlaced);
    }


    function Transition_Active_Number_2_Modify (string memory description, uint256 price) public {

        // Transition preconditions
        require(State == StateType.Active);
        require(msg.sender == InstanceOwner);

        // Call overridden function Modify in this contract
        Modify(description, price);

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_OfferPlaced_Number_0_AcceptOffer () public {

        // Transition preconditions
        require(State == StateType.OfferPlaced);
        require(msg.sender == InstanceOwner);

        // Call overridden function AcceptOffer in this contract
        AcceptOffer();

        // Transition postconditions
        assert(State == StateType.PendingInspection);
    }


    function Transition_OfferPlaced_Number_1_Reject () public {

        // Transition preconditions
        require(State == StateType.OfferPlaced);
        require(msg.sender == InstanceOwner);

        // Call overridden function Reject in this contract
        Reject();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_OfferPlaced_Number_2_Terminate () public {

        // Transition preconditions
        require(State == StateType.OfferPlaced);
        require(msg.sender == InstanceOwner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_OfferPlaced_Number_3_RescindOffer () public {

        // Transition preconditions
        require(State == StateType.OfferPlaced);
        require(msg.sender == InstanceBuyer);

        // Call overridden function RescindOffer in this contract
        RescindOffer();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_OfferPlaced_Number_4_ModifyOffer (uint256 offerPrice) public {

        // Transition preconditions
        require(State == StateType.OfferPlaced);
        require(msg.sender == InstanceBuyer);

        // Call overridden function ModifyOffer in this contract
        ModifyOffer(offerPrice);

        // Transition postconditions
        assert(State == StateType.OfferPlaced);
    }


    function Transition_PendingInspection_Number_0_Reject () public {

        // Transition preconditions
        require(State == StateType.PendingInspection);
        require(msg.sender == InstanceOwner);

        // Call overridden function Reject in this contract
        Reject();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_PendingInspection_Number_1_Terminate () public {

        // Transition preconditions
        require(State == StateType.PendingInspection);
        require(msg.sender == InstanceOwner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_PendingInspection_Number_2_RescindOffer () public {

        // Transition preconditions
        require(State == StateType.PendingInspection);
        require(msg.sender == InstanceBuyer);

        // Call overridden function RescindOffer in this contract
        RescindOffer();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_PendingInspection_Number_3_MarkInspected () public {

        // Transition preconditions
        require(State == StateType.PendingInspection);
        require(msg.sender == InstanceInspector);

        // Call overridden function MarkInspected in this contract
        MarkInspected();

        // Transition postconditions
        assert(State == StateType.Inspected);
    }


    function Transition_PendingInspection_Number_4_MarkAppraised () public {

        // Transition preconditions
        require(State == StateType.PendingInspection);
        require(msg.sender == InstanceAppraiser);

        // Call overridden function MarkAppraised in this contract
        MarkAppraised();

        // Transition postconditions
        assert(State == StateType.Appraised);
    }


    function Transition_Inspected_Number_0_Reject () public {

        // Transition preconditions
        require(State == StateType.Inspected);
        require(msg.sender == InstanceOwner);

        // Call overridden function Reject in this contract
        Reject();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_Inspected_Number_1_Terminate () public {

        // Transition preconditions
        require(State == StateType.Inspected);
        require(msg.sender == InstanceOwner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_Inspected_Number_2_RescindOffer () public {

        // Transition preconditions
        require(State == StateType.Inspected);
        require(msg.sender == InstanceBuyer);

        // Call overridden function RescindOffer in this contract
        RescindOffer();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_Inspected_Number_3_MarkAppraised () public {

        // Transition preconditions
        require(State == StateType.Inspected);
        require(msg.sender == InstanceAppraiser);

        // Call overridden function MarkAppraised in this contract
        MarkAppraised();

        // Transition postconditions
        assert(State == StateType.NotionalAcceptance);
    }


    function Transition_Appraised_Number_0_Reject () public {

        // Transition preconditions
        require(State == StateType.Appraised);
        require(msg.sender == InstanceOwner);

        // Call overridden function Reject in this contract
        Reject();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_Appraised_Number_1_Terminate () public {

        // Transition preconditions
        require(State == StateType.Appraised);
        require(msg.sender == InstanceOwner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_Appraised_Number_2_RescindOffer () public {

        // Transition preconditions
        require(State == StateType.Appraised);
        require(msg.sender == InstanceBuyer);

        // Call overridden function RescindOffer in this contract
        RescindOffer();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_Appraised_Number_3_MarkInspected () public {

        // Transition preconditions
        require(State == StateType.Appraised);
        require(msg.sender == InstanceInspector);

        // Call overridden function MarkInspected in this contract
        MarkInspected();

        // Transition postconditions
        assert(State == StateType.NotionalAcceptance);
    }


    function Transition_NotionalAcceptance_Number_0_Accept () public {

        // Transition preconditions
        require(State == StateType.NotionalAcceptance);
        require(msg.sender == InstanceOwner);

        // Call overridden function Accept in this contract
        Accept();

        // Transition postconditions
        assert(State == StateType.SellerAccepted);
    }


    function Transition_NotionalAcceptance_Number_1_Reject () public {

        // Transition preconditions
        require(State == StateType.NotionalAcceptance);
        require(msg.sender == InstanceOwner);

        // Call overridden function Reject in this contract
        Reject();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_NotionalAcceptance_Number_2_Terminate () public {

        // Transition preconditions
        require(State == StateType.NotionalAcceptance);
        require(msg.sender == InstanceOwner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_NotionalAcceptance_Number_3_Accept () public {

        // Transition preconditions
        require(State == StateType.NotionalAcceptance);
        require(msg.sender == InstanceBuyer);

        // Call overridden function Accept in this contract
        Accept();

        // Transition postconditions
        assert(State == StateType.BuyerAccepted);
    }


    function Transition_NotionalAcceptance_Number_4_RescindOffer () public {

        // Transition preconditions
        require(State == StateType.NotionalAcceptance);
        require(msg.sender == InstanceBuyer);

        // Call overridden function RescindOffer in this contract
        RescindOffer();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_BuyerAccepted_Number_0_Accept () public {

        // Transition preconditions
        require(State == StateType.BuyerAccepted);
        require(msg.sender == InstanceOwner);

        // Call overridden function Accept in this contract
        Accept();

        // Transition postconditions
        // assert(State == StateType.SellerAccepted);
        assert(State == StateType.Accepted);
    }


    function Transition_BuyerAccepted_Number_1_Reject () public {

        // Transition preconditions
        require(State == StateType.BuyerAccepted);
        require(msg.sender == InstanceOwner);

        // Call overridden function Reject in this contract
        Reject();

        // Transition postconditions
        assert(State == StateType.Active);
    }


    function Transition_BuyerAccepted_Number_2_Terminate () public {

        // Transition preconditions
        require(State == StateType.BuyerAccepted);
        require(msg.sender == InstanceOwner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_SellerAccepted_Number_0_Accept () public {

        // Transition preconditions
        require(State == StateType.SellerAccepted);
        require(msg.sender == InstanceBuyer);

        // Call overridden function Accept in this contract
        Accept();

        // Transition postconditions
        assert(State == StateType.Accepted);
    }


    function Transition_SellerAccepted_Number_1_RescindOffer () public {

        // Transition preconditions
        require(State == StateType.SellerAccepted);
        require(msg.sender == InstanceBuyer);

        // Call overridden function RescindOffer in this contract
        RescindOffer();

        // Transition postconditions
        assert(State == StateType.Active);
    }

    function Modify(string memory description, uint256 price) public {

        // Placeholder for function preconditions

        // Call function Modify of AssetTransfer
        AssetTransfer.Modify(description, price);

        // Placeholder for function postconditions

        // Signals successful execution of function Modify 
        WorkbenchBase.ContractUpdated("Modify");
    }

    function Terminate() public {

        // Placeholder for function preconditions

        // Call function Terminate of AssetTransfer
        AssetTransfer.Terminate();

        // Placeholder for function postconditions

        // Signals successful execution of function Terminate 
        WorkbenchBase.ContractUpdated("Terminate");
    }

    function MakeOffer(address inspector, address appraiser, uint256 offerPrice) public {

        // Placeholder for function preconditions

        // Call function MakeOffer of AssetTransfer
        AssetTransfer.MakeOffer(inspector, appraiser, offerPrice);

        // Placeholder for function postconditions

        // Signals successful execution of function MakeOffer 
        WorkbenchBase.ContractUpdated("MakeOffer");
    }

    function Reject() public {

        // Placeholder for function preconditions

        // Call function Reject of AssetTransfer
        AssetTransfer.Reject();

        // Placeholder for function postconditions

        // Signals successful execution of function Reject 
        WorkbenchBase.ContractUpdated("Reject");
    }

    function AcceptOffer() public {

        // Placeholder for function preconditions

        // Call function AcceptOffer of AssetTransfer
        AssetTransfer.AcceptOffer();

        // Placeholder for function postconditions

        // Signals successful execution of function AcceptOffer 
        WorkbenchBase.ContractUpdated("AcceptOffer");
    }

    function RescindOffer() public {

        // Placeholder for function preconditions

        // Call function RescindOffer of AssetTransfer
        AssetTransfer.RescindOffer();

        // Placeholder for function postconditions

        // Signals successful execution of function RescindOffer 
        WorkbenchBase.ContractUpdated("RescindOffer");
    }

    function ModifyOffer(uint256 offerPrice) public {

        // Placeholder for function preconditions

        // Call function ModifyOffer of AssetTransfer
        AssetTransfer.ModifyOffer(offerPrice);

        // Placeholder for function postconditions

        // Signals successful execution of function ModifyOffer 
        WorkbenchBase.ContractUpdated("ModifyOffer");
    }

    function Accept() public {

        // Placeholder for function preconditions

        // Call function Accept of AssetTransfer
        AssetTransfer.Accept();

        // Placeholder for function postconditions

        // Signals successful execution of function Accept 
        WorkbenchBase.ContractUpdated("Accept");
    }

    function MarkInspected() public {

        // Placeholder for function preconditions

        // Call function MarkInspected of AssetTransfer
        AssetTransfer.MarkInspected();

        // Placeholder for function postconditions

        // Signals successful execution of function MarkInspected 
        WorkbenchBase.ContractUpdated("MarkInspected");
    }

    function MarkAppraised() public {

        // Placeholder for function preconditions

        // Call function MarkAppraised of AssetTransfer
        AssetTransfer.MarkAppraised();

        // Placeholder for function postconditions

        // Signals successful execution of function MarkAppraised 
        WorkbenchBase.ContractUpdated("MarkAppraised");
    }
}
