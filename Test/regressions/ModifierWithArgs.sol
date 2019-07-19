pragma solidity >=0.4.24 <0.6.0;

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

    modifier GtX(uint a) {
        require(x > a);
        _;
        x = x; //just a dummy post
    }

    function plusOne(uint b) private GtX(b) onlyGreaterThanTen(b) {
        x = x + 1;
    }

    function foo(uint b) public {
        plusOne(b);
        assert (x > b + 3);
    }
}
