pragma solidity >=0.4.24 <0.6.0;

// This test shows an order in which base constructors for C are called:
// B(a) calls base ctor A(a) {x = a};
// ctor B is called {x++};
// ctor C is called {x++}


contract A {
    uint x;
    constructor (uint a) public {x = a;}
}

contract B is A {
    constructor (uint a) A(a) public {x++;}
}

contract C is A, B {
    constructor (uint a) B(a) public {x++; assert (x == a + 2);}
}