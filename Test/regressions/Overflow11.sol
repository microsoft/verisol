pragma solidity >=0.4.24 <0.6.0;

// Tests constant expressions with multuple operators;
// power operation for multiple constants is tested
// The test shows diff between BinaryOperation expr and TupleExpression
// The test passes, but generates Verisol translation error if line 20 is uncommented

contract UintTest {
  uint a;
  uint b;
  
  constructor () public {
  }
  
  function test(uint x, uint y) public {
    a = 2**2**2;              //the test passes: BinaryOperation in the rhs
	assert(a == 16);          //holds
	a = 2+2+2+2+2+2+2+2;      //the test passes: BinaryOperation in the rhs
	assert(a == 16);          //holds
	//a = (2**2)**2;          //VeriSol translation error: not supported (power operation is a TupleExpression)	
  }
}