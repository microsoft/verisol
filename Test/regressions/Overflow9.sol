pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow with and without /useModularArithmetic option
// Power operation for two constants is tested
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint8 a8;
  uint8 x;
  uint8 y;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
	 require(x > 2**8 - 2);	 
	 a8 = x + 3;                                                                                                     
     require(a8 >= x);        //fails with mod arithm; holds otherwise
	 assert (false);          //not reachable with mod arithm; reachable otherwise
  }
}