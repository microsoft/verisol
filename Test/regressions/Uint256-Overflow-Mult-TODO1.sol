pragma solidity >=0.4.24 <0.6.0;

// Test uint256 overflow with and without /useModularArithmetic option
// The test fails with /useModularArithmetic option and passes otherwise
// TODO: in .bpl: x=1, b=1, amount=0???
contract UintTest {
  
  //uint b;
  
  constructor () public {
  }
  
  function test(uint256 x, uint256 b) public { 
	uint amount = x * b;
	require(x > 0);
	require(b > 0); 
	require(amount == 0);  //not satisfiable without mod arithm
	assert(false);         
  }
}