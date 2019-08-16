pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint a;
    constructor () public {
        a = 1;
    }
}

contract B is A {
    uint b;
    constructor () public {
        a = 2;
        b = 2;
    }
}

contract C is A {
    uint c;
    constructor () public {
        a = 3;
        c = 3;
    }
}

contract D is B, C {
    constructor () public
    {
        assert (a != 3);
        assert (b != 2);
        assert (c != 3);
    }
}
