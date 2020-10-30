pragma solidity >=0.5.1;

contract MarketPlace
{
    address seller;
    address buyer;

    uint sellingPrice;
    uint buyingPrice;

    enum State
    {
        MarketPlace_Active, MarketPlace_OfferPlaced, MarketPlace_NotionalAccept,
         MarketPlace_BuyerAccept, MarketPlace_SellerAccept, MarketPlace_Accept
    }
    State private ContractCurrentState;

    event eMakeOffer(address, uint);
    event eAcceptOffer(address, uint);

    modifier isSeller (address _caller)
    {
        require(_caller == seller);
        _;
    }

    modifier isBuyer (address _caller)
    {
        require(_caller == buyer);
        _;
    }

    constructor (address _seller, address _buyer)
        public
    {
        seller = _seller;
        buyer = _buyer;

        ContractCurrentState = State.MarketPlace_Active;
    }

    function MakeOffer (uint _sellingPrice)
        public
        isSeller (msg.sender)
    {
        require(ContractCurrentState == State.MarketPlace_Active, "<MakeOffer> function invoked in invalid state");

        sellingPrice = _sellingPrice;
        ContractCurrentState = State.MarketPlace_OfferPlaced;
        emit eMakeOffer(buyer, sellingPrice);
    }

    function ModifyOffer (bool _increase, uint _change)
        public
        isSeller (msg.sender)
    {
        require(ContractCurrentState == State.MarketPlace_OfferPlaced, "<ModifyOffer> function invoked in invalid state");

        if (_increase)
        {
            sellingPrice = sellingPrice + _change; // overflow
        }
        else
        {
            sellingPrice = sellingPrice - _change;  // underflow
        }
    }

    function RejectOffer ()
        public
        isBuyer (msg.sender)
    {
        require(ContractCurrentState == State.MarketPlace_OfferPlaced, "<RejectOffer> function invoked in invalid state");

        ContractCurrentState = State.MarketPlace_Active;
    }

    function AcceptOffer ()
        public
        payable
        isBuyer (msg.sender)
    {
        // Workbench bug: MarketPlace_Active
        require(ContractCurrentState == State.MarketPlace_OfferPlaced, "<AcceptOffer> function invoked in invalid state");

        if (msg.value >= sellingPrice)
        {
            buyingPrice = msg.value;
            emit eAcceptOffer(seller, buyingPrice);
            ContractCurrentState = State.MarketPlace_NotionalAccept;
        }
    }

    function Accept()
        public
    {
        require(ContractCurrentState == State.MarketPlace_NotionalAccept || ContractCurrentState == State.MarketPlace_BuyerAccept ||
                ContractCurrentState == State.MarketPlace_SellerAccept, "<Accept> function invoked in invalid state");

        if (ContractCurrentState == State.MarketPlace_NotionalAccept && msg.sender == buyer)
        {
            ContractCurrentState = State.MarketPlace_BuyerAccept;
        }

        else if (ContractCurrentState == State.MarketPlace_NotionalAccept && msg.sender == seller)
        {
            ContractCurrentState = State.MarketPlace_SellerAccept;
        }

        else if (ContractCurrentState == State.MarketPlace_BuyerAccept && msg.sender == seller)
        {
            // workbench bug: went to SellerAccept
            ContractCurrentState = State.MarketPlace_Accept;
        }

        else if (ContractCurrentState == State.MarketPlace_SellerAccept && msg.sender == buyer)
        {
            ContractCurrentState = State.MarketPlace_Accept;
        }
    }

    function Withdraw()
        public
        isSeller (msg.sender)
    {
        require(ContractCurrentState == State.MarketPlace_Accept, "<Withdraw> function invoked in invalid state");

        (bool success, ) = seller.call.value(buyingPrice)("");
        if(!success)
            revert();
    }
}