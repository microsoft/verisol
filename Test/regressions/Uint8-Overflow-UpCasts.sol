pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow with and without /useModularArithmetic option
// The test checks that upcast operators are noop.
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint16 a16;
  uint8 x;
  uint8 y;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
	 require(x > 256 - 2); 
	 a16 = uint16(x + 3);        //upcast: noop                                                                                                 
	 require(a16 >= uint24(x));  //fails with mod arithm; holds otherwise
	 assert (false);             //not reachable with mod arithm; reachable otherwise
  }
}