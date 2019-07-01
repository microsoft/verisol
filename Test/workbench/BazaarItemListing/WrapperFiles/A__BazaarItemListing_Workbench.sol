pragma solidity ^0.4.20; // Solidity compiler version supported by Azure Blockchain Workbench


//---------------------------------------------
//Generated automatically for application 'BazaarItemListing' by AppCodeGen utility
//---------------------------------------------


import "./BazaarItemListing.sol";
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
// The wrapper contract Bazaar_AzureBlockchainWorkBench invokes functions from Bazaar.
// The inheritance order of Bazaar_AzureBlockchainWorkBench ensures that functions and variables in Bazaar 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract Bazaar_AzureBlockchainWorkBench is WorkbenchBase, Bazaar {
    // 
    // Constructor
    // 
    function Bazaar_AzureBlockchainWorkBench(address partyA, int256 balanceA, address partyB, int256 balanceB)
            WorkbenchBase("BazaarItemListing", "Bazaar")
            Bazaar(partyA, balanceA, partyB, balanceB)
            public {

        // Check postconditions and access control for constructor of Bazaar


        // Constructor should transition the state to StartState
        assert(State == StateType.PartyProvisioned);

        // Signals successful creation of contract Bazaar 
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


    function Transition_PartyProvisioned_Number_0_ListItem (string memory itemName, int256 itemPrice) public {

        // Transition preconditions
        require(State == StateType.PartyProvisioned);
        require(msg.sender == InstancePartyA || msg.sender == InstancePartyB);

        // Call overridden function ListItem in this contract
        ListItem(itemName, itemPrice);

        // Transition postconditions
        assert(State == StateType.ItemListed);
    }


    function Transition_PartyProvisioned_Number_1_UpdateBalance (address sellerParty, address buyerParty, int256 itemPrice) public {

        // Transition preconditions
        require(State == StateType.PartyProvisioned);

        // Call overridden function UpdateBalance in this contract
        UpdateBalance(sellerParty, buyerParty, itemPrice);

        // Transition postconditions
        assert(State == StateType.CurrentSaleFinalized);
    }


    function Transition_ItemListed_Number_0_ListItem (string memory itemName, int256 itemPrice) public {

        // Transition preconditions
        require(State == StateType.ItemListed);
        require(msg.sender == InstancePartyA || msg.sender == InstancePartyB);

        // Call overridden function ListItem in this contract
        ListItem(itemName, itemPrice);

        // Transition postconditions
        assert(State == StateType.ItemListed);
    }


    function Transition_CurrentSaleFinalized_Number_0_ListItem (string memory itemName, int256 itemPrice) public {

        // Transition preconditions
        require(State == StateType.CurrentSaleFinalized);
        require(msg.sender == InstancePartyA || msg.sender == InstancePartyB);

        // Call overridden function ListItem in this contract
        ListItem(itemName, itemPrice);

        // Transition postconditions
        assert(State == StateType.ItemListed);
    }

    function ListItem(string memory itemName, int256 itemPrice) public {

        // Placeholder for function preconditions

        // Call function ListItem of Bazaar
        Bazaar.ListItem(itemName, itemPrice);

        // Placeholder for function postconditions

        // Signals successful execution of function ListItem 
        WorkbenchBase.ContractUpdated("ListItem");
    }

    function UpdateBalance(address sellerParty, address buyerParty, int256 itemPrice) public {

        // Placeholder for function preconditions

        // Call function UpdateBalance of Bazaar
        Bazaar.UpdateBalance(sellerParty, buyerParty, itemPrice);

        // Placeholder for function postconditions

        // Signals successful execution of function UpdateBalance 
        WorkbenchBase.ContractUpdated("UpdateBalance");
    }
}
//
// The wrapper contract ItemListing_AzureBlockchainWorkBench invokes functions from ItemListing.
// The inheritance order of ItemListing_AzureBlockchainWorkBench ensures that functions and variables in ItemListing 
// are not shadowed by WorkbenchBase.
// Any access of WorkbenchBase function or variables is qualified with WorkbenchBase
//
contract ItemListing_AzureBlockchainWorkBench is WorkbenchBase, ItemListing {
    // 
    // Constructor
    // 
    function ItemListing_AzureBlockchainWorkBench()
            WorkbenchBase("BazaarItemListing", "ItemListing")
            ItemListing()
            public {

        // Check postconditions and access control for constructor of ItemListing


        // Constructor should transition the state to StartState
        assert(State == StateType.ItemAvailable);

        // Signals successful creation of contract ItemListing 
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


    function Transition_ItemAvailable_Number_0_BuyItem () public {

        // Transition preconditions
        require(State == StateType.ItemAvailable);
        require(msg.sender == PartyA || msg.sender == PartyB);

        // Call overridden function BuyItem in this contract
        BuyItem();

        // Transition postconditions
        assert(State == StateType.ItemSold);
    }

    function BuyItem() public {

        // Placeholder for function preconditions

        // Call function BuyItem of ItemListing
        ItemListing.BuyItem();

        // Placeholder for function postconditions

        // Signals successful execution of function BuyItem 
        WorkbenchBase.ContractUpdated("BuyItem");
    }
}
