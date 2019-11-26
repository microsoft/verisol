pragma solidity >=0.4.24 <0.6.0;

// Test uint256 overflow with and without /useModularArithmetic option
// The test fails with /useModularArithmetic option and passses otherwise
// Compare this test with Uint256-Overflow-Mult-TODO.sol
contract UintTest {
  
  //uint b;
  
  constructor () public {
  }
  
  function test(uint256 x) public { 
	//uint b = 2 ** 255;
	uint b;
	uint256 amount = x * b;
	require(x > 0);
	require(b > 0); 
	require(amount == 0);  //not satisfiable without mod arithm
	assert(false);         
	 //require(x > 115792089237316195423570985008687907853269984665640564039457584007913129639936 - 2);  
  }
}