pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint x;
    constructor (uint a) public {x= a; /* assert (x != a); */}
}

contract B is A {
    constructor (uint a) A(a) public {x++; assert (x != a + 2);}
}

contract C is B {
    constructor (uint a) B(a) public {x++; assert (x != a + 2);}
}