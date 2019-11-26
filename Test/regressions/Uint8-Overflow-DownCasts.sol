pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow with and without /useModularArithmetic option
// The test checks that downcast operators are not allowed.
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint8 a8;
  uint16 x;
  uint8 y;
  
  constructor () public {
  }
  
  function test(uint16 x) public {
	 require(uint8(x) > 256 - 2);   //downcast: warning
	 a8 = uint8(x) + 3;             //downcast: warning                                                                                                 
	 require(a8 >= uint24(x));      //fails with mod arithm; holds otherwise
	 assert (false);                //not reachable with mod arithm; reachable otherwise
  }
}