pragma solidity >=0.4.24 <0.6.0;

// Test uint overflow with and without /useModularArithmetic option
// Power operation for two constants is tested
// Is test is similar to Uint8-Overflow.sol, but for uint type 
// instead of uint8 type
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  uint a;
  
  constructor () public {
  }
  
  function test(uint x, uint y) public {
	require(x > 2**256 - 2);
	a = x + 3;
	require(a >= x);   //fails with mod arithm; holds otherwise (a overflows)
	assert(false);     //not reachable with mod arithm; reachable otherwise
  }
}