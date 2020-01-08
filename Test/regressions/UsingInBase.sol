pragma solidity >=0.4.24 <0.6.0;

library Lib {
    function add(uint _a, uint _b) public pure returns (uint r) {
        r = _a + _b;
    }
}

contract C {
    using Lib for uint;
}

contract D is C {
     function foo() public {
        uint x = 1;
        uint y = 2;
        uint z = x.add(y);
        assert (z == 3);
    }
}