pragma solidity ^0.5.0;


contract UnreachableAssertion {
    address private owner;

    constructor() public {
        owner = address(this);
        require(owner != address(0));
    }

    modifier onlyOwner() {  
        require(msg.sender == owner);
        _;
    }

    function neverSuccessful() onlyOwner view public {
        assert(false);
    }
}
