pragma solidity ^0.4.24;

contract ModifierReturn {

    uint x;

    constructor (uint _x) public {
        x = _x;
    }

    modifier onlyGreaterThanTen() {
        _;
        x = x + 1;
    }

    function wrapper() public {
        uint returnVal = plusOne();
        assert (returnVal == 10);
        assert (x == 11);
    }

    function plusOne() private onlyGreaterThanTen returns (uint){
        require(x == 10);
        return x;
    }

}
