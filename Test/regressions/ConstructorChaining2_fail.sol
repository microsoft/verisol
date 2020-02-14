pragma solidity >=0.4.24 <0.6.0;

// Shows that constructor chaining still has an issue:
// D constructor is called twice - see corral.txt

contract D {
	uint x;
	// why x gets 1 every time this constructor executes?
    constructor () public { x++; }
}

contract B is D {
    constructor () D() public {}
}

contract C is B {
    constructor () public { assert (x == 2); }    //holds, despite D constructor being called twice
}