pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow with and without /useModularArithmetic option
// This test checks conversions from int to address
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint8 a8;
  uint8 x;
  uint8 y;
  //uint16 b;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
	 address adr = address(256-2);
	 require(address(x) > adr); 
	 a8 = x + 3;                                                                                                     
     require(a8 >= x);        //fails with mod arithm; holds otherwise
	 assert (false);          //not reachable with mod arithm; reachable otherwise
  }
}