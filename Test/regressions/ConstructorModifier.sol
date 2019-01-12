pragma solidity ^0.4.24;

contract A {
    uint a;
    function A(uint _a) public {
        a = _a;
    }
}

contract B {
    uint b;
    function B(uint _b) public {
        b = _b;
    }
}

contract C is A, B {
    function C() public A(1) B(2) {
        assert (a == 1);
        assert (b == 2);
    }
}
