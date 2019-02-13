pragma solidity ^0.4.24;

contract A {

    uint x;

    constructor (uint _x) public {
        x = _x;
    }

    modifier onlyGreaterThanTen(uint a) {
        require(x > a);
        _;
        x = x + 2;
    }

    function plusOne(uint b) private onlyGreaterThanTen(b) {
        x = x + 1;
    }

    function foo(uint b) public {
        plusOne(b);
        assert (x > b + 3);
    }
}
