pragma solidity >=0.4.24 <0.6.0;

import "./SafeMath.sol";

// Test uint256 overflow with and without /useModularArithmetic option
// The test fails with /useModularArithmetic option and passes otherwise
// When using SafeMath.mul, the test passes in both cases, since "require"
// in SafeMath.mul does not hold.
// When b is a non-constant stvar (line 17 uncommented), the test passes in both cases; reason: 
// In .bpl: var b_UintTest: [Ref]int;
// - hence, b cannot get too big for amount to overflow?
// Compare this test with Uint256-Overflow-Mult.sol 
contract UintTest {
  using SafeMath for uint;
  
  //uint constant b = 2 ** 254;
  uint b;
  
  constructor () public {
  }
  
  function test(uint256 x) public { 
	uint amount = x * b;
	//uint amount = SafeMath.mul(x, b);
	//require(x > 0);
	//require(b > 0); 
	//require(amount == 0);  //not satisfiable without mod arithm
	assert(false);         
  }
}