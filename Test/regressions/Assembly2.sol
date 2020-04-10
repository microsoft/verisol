pragma solidity ^0.5.2;

// Tests that function with inline assembly generates non-det result
contract AssemblyTest
{
	bool b;
	int x;
    constructor () public {
  }
	
function foo() public {
		b = bar(x);
		if (!b)
			assert(false);
	}
function bar(int x) public returns (bool) { 
	uint256 size;
	assembly { size := x }
	return size > 0;
	}
}
