pragma solidity >=0.4.24 <0.6.0;

// Shows that constructor chaining still has a bug
// D constructor is called twice - see trace in corral.txt:
// ctor D {x = 1}
// ctor D {x = 1}
// ctor B
// ctor C

contract D {
	uint x;
    constructor () public { x++; }
}

contract B is D {
    constructor () D() public {}
}

contract C is B {
    constructor () public { assert (x != 1); }   
}