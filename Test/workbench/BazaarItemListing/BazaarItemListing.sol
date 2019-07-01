pragma solidity ^0.4.20;

import "./ItemListing.sol";

contract Bazaar /* is WorkbenchBase('BazaarItemListing', 'Bazaar') */
{
    enum StateType { PartyProvisioned, ItemListed, CurrentSaleFinalized}

    StateType public State;

    address public InstancePartyA;
    int public PartyABalance;

    address public InstancePartyB;
    int public PartyBBalance;

    address public InstanceBazaarMaintainer;
    address public CurrentSeller;

    string public ItemName;
    int public ItemPrice;

    ItemListing currentItemListing;
    address public CurrentContractAddress;

    function Bazaar (address partyA, int balanceA, address partyB, int balanceB) public {
        InstanceBazaarMaintainer = msg.sender;

        // ensure the two parties are different
        if (partyA == partyB) {
            revert();
        }

        InstancePartyA = partyA;
        PartyABalance = balanceA;

        InstancePartyB = partyB;
        PartyBBalance = balanceB;

        CurrentContractAddress = address(this);

        State = StateType.PartyProvisioned;

        // ContractCreated();
    }

    function HasBalance(address buyer, int itemPrice) public returns (bool) {
        if (buyer == InstancePartyA) {
            return (PartyABalance >= itemPrice);
        }

        if (buyer == InstancePartyB) {
            return (PartyBBalance >= itemPrice);
        }

        return false;
    }

    function UpdateBalance(address sellerParty, address buyerParty, int itemPrice) public {
        ChangeBalance(sellerParty, itemPrice);
        ChangeBalance(buyerParty, -itemPrice);

        State = StateType.CurrentSaleFinalized;
        // ContractUpdated('UpdateBalance');
    }

    function ChangeBalance(address party, int balance) public {
        if (party == InstancePartyA) {
            PartyABalance += balance;
        }

        if (party == InstancePartyB) {
            PartyBBalance += balance;
        }
    }

    function ListItem(string itemName, int itemPrice) public
    {
        CurrentSeller = msg.sender;

        currentItemListing = new ItemListing();
        currentItemListing.AddItem(itemName, itemPrice, CurrentSeller, CurrentContractAddress, InstancePartyA, InstancePartyB);

        State = StateType.ItemListed;
        // ContractUpdated('ListItem');
    }
}
