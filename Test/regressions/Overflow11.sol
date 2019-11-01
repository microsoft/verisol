pragma solidity >=0.4.24 <0.6.0;

// Test uint overflow with and without /useModularArithmetic option
// THe test shows diff between BinaryOperation and TupleExpression
// Power operation for multiple constants is tested
// The test passes 
contract UintTest {
  uint a;
  uint b;
  
  constructor () public {
  }
  
  function test(uint x, uint y) public {
    //a = 2**2**2;              //the test passes: BinaryOperation in the rhs
	//a = 2+2+2+2+2+2+2+2;
	a = (2**2)**2;          //VeriSol translation error: not supported (power operation too complex
	assert(a == 16);          //holds
  }
}