pragma solidity ^0.4.24;

contract A {
    uint a;
    function A() public {
        a = 1;
    }
}

contract B is A {
    uint b;
    function B() public {
        a = 2;
        b = 2;
    }
}

contract C is A {
    uint c;
    function C() public {
        a = 3;
        c = 3;
    }
}

contract D is B, C {
    function D() public
    {
        assert (a == 3);
        assert (b == 2);
        assert (c == 3);
    }
}
