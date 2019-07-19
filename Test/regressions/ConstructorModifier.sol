pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint a;
    constructor (uint _a) public {
        a = _a;
    }
}

contract B {
    uint b;
    constructor (uint _b) public {
        b = _b;
    }
}

contract C is A, B {
    constructor () public A(1) B(2) {
        assert (a == 1);
        assert (b == 2);
    }
}
