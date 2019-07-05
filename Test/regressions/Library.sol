pragma solidity >=0.4.24 <0.6.0;

//simple library with no internal state

library Lib {
    function add(uint _a, uint _b) public view returns (uint r) {
        address x = address(this);
        assert(x == msg.sender);
        r = _a + _b;
    }
}

contract C {
    // using Lib for uint;

    function foo() public {
        uint x = 1;
        uint y = 2;
        uint z = Lib.add(x, y);
        assert (z == 3);
    }
}
