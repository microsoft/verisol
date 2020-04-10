pragma solidity >=0.4.24 <0.6.0;

// This is a basic test of a straight chain of base constructors with arguments
// This test passes

// Example of the trace:
// A(x), where x is 718
// B(x+1), where x is 718; B's arg is 719
// C(x+2), where x is 719, C's arg is 721
// ctor C {a = x}, where a = 721
// ctor B, x = 719, a = 721, assertion holds
// ctor A, where x is 718

contract C {
	uint a;
	constructor (uint x) public 
	{
		a = x;
	}
}
contract B is C {
	constructor (uint x) C(x+2) public 
	{
		assert(a == x + 2);
	}
}
//contract A is B, C {                       //solc error: "linearization impossible"
contract A is B {
	constructor (uint x) B(x+1) public 
	{
		assert(a == x + 3);
	}
}
