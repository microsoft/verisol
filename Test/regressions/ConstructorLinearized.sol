pragma solidity >=0.4.24 <0.6.0;

// This test should pass when the bug in constructor chaining is fixed
// This (failing) test exposes a bug in constructor chaining:
// ctor A is called three times (a value should be 3 in ctor C)

// Example of the trace:
// ctor A {a = 1}  
// ctor A {a = 1}   (for ctor B)   - wrong
// ctor B {a = 2; b = 2}
// ctor A {a = 1}   (for ctor C)   - wrong
// ctor C {a = 2; c = 3}
// ctor D


contract A {
    uint a;
    constructor () public {
        a++;
    }
}

contract B is A {
    uint b;
    constructor () public {
        a++;
        b = 2;
    }
}

contract C is A {
    uint c;
    constructor () public {
        a++;
        c = 3;
    }
}

contract D is B, C {
    constructor () public
    {
        assert (a == 3);              //should pass
        assert (b == 2);
        assert (c == 3);
    }
}
