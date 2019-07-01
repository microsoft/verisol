pragma solidity ^0.4.20;

import "./BazaarItemListing.sol";

contract ItemListing /* is WorkbenchBase('BazaarItemListing', 'ItemListing') */
{
    enum StateType { ItemAvailable, ItemSold }

    StateType public State;

    address public Seller;
    address public InstanceBuyer;
    address public ParentContract;
    string public ItemName;
    int public ItemPrice;
    address public PartyA;
    address public PartyB;
    

    function ItemListing() public {}

    function AddItem(string itemName, int itemPrice, address seller, address parentContractAddress, address partyA, address partyB) public {
        Seller = seller;
        ParentContract = parentContractAddress;
        ItemName = itemName;
        ItemPrice = itemPrice;

        PartyA = partyA;
        PartyB = partyB;

        State = StateType.ItemAvailable;

        // ContractCreated();
    }

    function BuyItem() public
    {
        InstanceBuyer = msg.sender;

        // ensure that the buyer is not the seller
        if (Seller == InstanceBuyer) {
            revert();
        }

        Bazaar bazaar = Bazaar(ParentContract);

        // check Buyer's balance
        if (!bazaar.HasBalance(InstanceBuyer, ItemPrice)) {
            revert();
        }

        // indicate item bought by updating seller and buyer balances
        bazaar.UpdateBalance(Seller, InstanceBuyer, ItemPrice);

        State = StateType.ItemSold;
        // ContractUpdated('BuyItem');
    }
}
