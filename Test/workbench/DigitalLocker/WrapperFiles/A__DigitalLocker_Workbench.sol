pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'DigitalLocker' by AppCodeGen utility
//---------------------------------------------


import "./DigitalLocker.sol";
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
// The wrapper contract DigitalLocker_AzureBlockchainWorkBench invokes functions from DigitalLocker.
// The inheritance order of DigitalLocker_AzureBlockchainWorkBench ensures that functions and variables in DigitalLocker 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract DigitalLocker_AzureBlockchainWorkBench is WorkbenchBase, DigitalLocker {
    // 
    // Constructor
    // 
    function DigitalLocker_AzureBlockchainWorkBench(string memory lockerFriendlyName, address bankAgent)
            WorkbenchBase("DigitalLocker", "DigitalLocker")
            DigitalLocker(lockerFriendlyName, bankAgent)
            public {

        // Check postconditions and access control for constructor of DigitalLocker


        // Constructor should transition the state to StartState
        assert(State == StateType.Requested);

        // Signals successful creation of contract DigitalLocker 
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


    function Transition_Requested_Number_0_BeginReviewProcess () public {

        // Transition preconditions
        require(State == StateType.Requested);

        // Call overridden function BeginReviewProcess in this contract
        BeginReviewProcess();

        // Transition postconditions
        assert(State == StateType.DocumentReview);
    }


    function Transition_DocumentReview_Number_0_UploadDocuments (string memory lockerIdentifier, string memory image) public {

        // Transition preconditions
        require(State == StateType.DocumentReview);
        require(msg.sender == BankAgent);

        // Call overridden function UploadDocuments in this contract
        UploadDocuments(lockerIdentifier, image);

        // Transition postconditions
        assert(State == StateType.AvailableToShare);
    }


    function Transition_AvailableToShare_Number_0_ShareWithThirdParty (address thirdPartyRequestor, string memory expirationDate, string memory intendedPurpose) public {

        // Transition preconditions
        require(State == StateType.AvailableToShare);
        require(msg.sender == Owner);

        // Call overridden function ShareWithThirdParty in this contract
        ShareWithThirdParty(thirdPartyRequestor, expirationDate, intendedPurpose);

        // Transition postconditions
        assert(State == StateType.SharingWithThirdParty);
    }


    function Transition_AvailableToShare_Number_1_Terminate () public {

        // Transition preconditions
        require(State == StateType.AvailableToShare);
        require(msg.sender == Owner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_AvailableToShare_Number_2_RequestLockerAccess (string memory intendedPurpose) public {

        // Transition preconditions
        require(State == StateType.AvailableToShare);

        // Call overridden function RequestLockerAccess in this contract
        RequestLockerAccess(intendedPurpose);

        // Transition postconditions
        assert(State == StateType.SharingRequestPending);
    }


    function Transition_SharingRequestPending_Number_0_AcceptSharingRequest () public {

        // Transition preconditions
        require(State == StateType.SharingRequestPending);
        require(msg.sender == Owner);

        // Call overridden function AcceptSharingRequest in this contract
        AcceptSharingRequest();

        // Transition postconditions
        assert(State == StateType.SharingWithThirdParty);
    }


    function Transition_SharingRequestPending_Number_1_RejectSharingRequest () public {

        // Transition preconditions
        require(State == StateType.SharingRequestPending);
        require(msg.sender == Owner);

        // Call overridden function RejectSharingRequest in this contract
        RejectSharingRequest();

        // Transition postconditions
        assert(State == StateType.AvailableToShare);
    }


    function Transition_SharingRequestPending_Number_2_Terminate () public {

        // Transition preconditions
        require(State == StateType.SharingRequestPending);
        require(msg.sender == Owner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_SharingWithThirdParty_Number_0_RevokeAccessFromThirdParty () public {

        // Transition preconditions
        require(State == StateType.SharingWithThirdParty);
        require(msg.sender == Owner);

        // Call overridden function RevokeAccessFromThirdParty in this contract
        RevokeAccessFromThirdParty();

        // Transition postconditions
        assert(State == StateType.AvailableToShare);
    }


    function Transition_SharingWithThirdParty_Number_1_Terminate () public {

        // Transition preconditions
        require(State == StateType.SharingWithThirdParty);
        require(msg.sender == Owner);

        // Call overridden function Terminate in this contract
        Terminate();

        // Transition postconditions
        assert(State == StateType.Terminated);
    }


    function Transition_SharingWithThirdParty_Number_2_ReleaseLockerAccess () public {

        // Transition preconditions
        require(State == StateType.SharingWithThirdParty);
        require(msg.sender == ThirdPartyRequestor);

        // Call overridden function ReleaseLockerAccess in this contract
        ReleaseLockerAccess();

        // Transition postconditions
        assert(State == StateType.AvailableToShare);
    }

    function BeginReviewProcess() public {

        // Placeholder for function preconditions

        // Call function BeginReviewProcess of DigitalLocker
        DigitalLocker.BeginReviewProcess();

        // Placeholder for function postconditions

        // Signals successful execution of function BeginReviewProcess 
        WorkbenchBase.ContractUpdated("BeginReviewProcess");
    }

    function RejectApplication(string memory rejectionReason) public {

        // Placeholder for function preconditions

        // Call function RejectApplication of DigitalLocker
        DigitalLocker.RejectApplication(rejectionReason);

        // Placeholder for function postconditions

        // Signals successful execution of function RejectApplication 
        WorkbenchBase.ContractUpdated("RejectApplication");
    }

    function UploadDocuments(string memory lockerIdentifier, string memory image) public {

        // Placeholder for function preconditions

        // Call function UploadDocuments of DigitalLocker
        DigitalLocker.UploadDocuments(lockerIdentifier, image);

        // Placeholder for function postconditions

        // Signals successful execution of function UploadDocuments 
        WorkbenchBase.ContractUpdated("UploadDocuments");
    }

    function ShareWithThirdParty(address thirdPartyRequestor, string memory expirationDate, string memory intendedPurpose) public {

        // Placeholder for function preconditions

        // Call function ShareWithThirdParty of DigitalLocker
        DigitalLocker.ShareWithThirdParty(thirdPartyRequestor, expirationDate, intendedPurpose);

        // Placeholder for function postconditions

        // Signals successful execution of function ShareWithThirdParty 
        WorkbenchBase.ContractUpdated("ShareWithThirdParty");
    }

    function AcceptSharingRequest() public {

        // Placeholder for function preconditions

        // Call function AcceptSharingRequest of DigitalLocker
        DigitalLocker.AcceptSharingRequest();

        // Placeholder for function postconditions

        // Signals successful execution of function AcceptSharingRequest 
        WorkbenchBase.ContractUpdated("AcceptSharingRequest");
    }

    function RejectSharingRequest() public {

        // Placeholder for function preconditions

        // Call function RejectSharingRequest of DigitalLocker
        DigitalLocker.RejectSharingRequest();

        // Placeholder for function postconditions

        // Signals successful execution of function RejectSharingRequest 
        WorkbenchBase.ContractUpdated("RejectSharingRequest");
    }

    function RequestLockerAccess(string memory intendedPurpose) public {

        // Placeholder for function preconditions

        // Call function RequestLockerAccess of DigitalLocker
        DigitalLocker.RequestLockerAccess(intendedPurpose);

        // Placeholder for function postconditions

        // Signals successful execution of function RequestLockerAccess 
        WorkbenchBase.ContractUpdated("RequestLockerAccess");
    }

    function ReleaseLockerAccess() public {

        // Placeholder for function preconditions

        // Call function ReleaseLockerAccess of DigitalLocker
        DigitalLocker.ReleaseLockerAccess();

        // Placeholder for function postconditions

        // Signals successful execution of function ReleaseLockerAccess 
        WorkbenchBase.ContractUpdated("ReleaseLockerAccess");
    }

    function RevokeAccessFromThirdParty() public {

        // Placeholder for function preconditions

        // Call function RevokeAccessFromThirdParty of DigitalLocker
        DigitalLocker.RevokeAccessFromThirdParty();

        // Placeholder for function postconditions

        // Signals successful execution of function RevokeAccessFromThirdParty 
        WorkbenchBase.ContractUpdated("RevokeAccessFromThirdParty");
    }

    function Terminate() public {

        // Placeholder for function preconditions

        // Call function Terminate of DigitalLocker
        DigitalLocker.Terminate();

        // Placeholder for function postconditions

        // Signals successful execution of function Terminate 
        WorkbenchBase.ContractUpdated("Terminate");
    }
}
