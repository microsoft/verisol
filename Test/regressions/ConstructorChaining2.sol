pragma solidity >=0.4.24 <0.6.0;

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