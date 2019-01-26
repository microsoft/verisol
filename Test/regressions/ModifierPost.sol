pragma solidity ^0.4.24;

contract ModifierPost {

    uint x;
    bool modified;

    constructor (uint _x) public {
        x = _x;
        modified = false;
    }

    modifier onlyGreaterThanTen() {
        _;
        require(x > 10, 'Invalid x!');
    }

    function wrapper() public {
        plusOne();
        assert (x > 10);
    }

    function plusOne() private onlyGreaterThanTen {
        x = x - 1;
    }

}
