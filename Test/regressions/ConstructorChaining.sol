pragma solidity ^0.4.24;

contract A {
    uint x;
    constructor (uint a) public {x= a;}
}

contract B is A {
    constructor (uint a) A(a) public {x++;}
}

contract C is B {
    constructor (uint a) B(a) public {x++; assert (x == a + 2);}
}