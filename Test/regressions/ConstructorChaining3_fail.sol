pragma solidity >=0.4.24 <0.6.0;

// Possible bug:
// In this test, ctor D is NOT called twice -
// Only ctor C is called
// compare with ConstructorChaining2_fail.sol

contract D {
	uint a;
    constructor (uint x) public { a = x; }
}

contract B is D {
    constructor (uint x) D(x+1) public {}
}

contract C is B {
    constructor (uint x) public { assert (a == x + 2); }   
}