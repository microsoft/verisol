pragma solidity ^0.4.24;

library Lib {

    function add(uint _a, uint _b) public pure returns (uint r) {
        r = _a + _b;
    }

}

contract C {
    using Lib for uint;

    function foo() public {
        uint x = 1;
        uint y = 2;
        uint z = x.add(y);
        assert (z == 3);
    }

}
