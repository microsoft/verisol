pragma solidity ^0.5.2;

// Tests that function with inline assembly generates non-det result
contract AssemblyTest
{
	int b;
	int x;
    constructor () public {
  }
	
function foo() public {
		b = bar(x);
		if (b > 10)
			assert(false);
	}
function bar(int x) public returns (int) { 
	int size;
	assembly { size := x }
	return size;
	}
}
