pragma solidity >=0.4.24 <0.6.0;

// ctor D is called once in this test:
// ctor D(x+1) {a == x+1}
// ctor B(x)
// ctor C

// Compare this test with ConstructorChaining3_fail.sol -
// the only diff is that here, ctor C calls B(x)
// Also, compare this test with ConstructorChaining2_fail.sol,
// where ctor D is called twice

contract D {
	uint a;
    constructor (uint x) public { a = x; }
}

contract B is D {
    constructor (uint x) D(x+1) public {}
}

contract C is B {
    constructor (uint x) B(x) public { assert (a == x + 1); }    
}