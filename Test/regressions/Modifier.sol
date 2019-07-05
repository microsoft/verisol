pragma solidity >=0.4.24 <0.6.0;

contract Modifier {

    uint x;

    constructor (uint _x) public {
        x = _x;
    }

    modifier onlyGreaterThanTen() {
        require(x > 10, 'Invalid x!');
        _;
    }

    function plusOne() public onlyGreaterThanTen {
        x = x + 1;
        assert (x > 11);
    }

}

// Presence of multiple contracts with modifiers
// generated a modifier multiple times
contract Bar {

}
