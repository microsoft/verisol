pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint x;
    constructor (uint a) public {x= a;}
}

contract B is A {
    constructor (uint a) A(a) public {x++; assert (x == a + 1);}
}

contract C is A, B {
}