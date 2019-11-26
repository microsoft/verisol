pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow of "+=" with and without /useModularArithmetic option
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  int z;
  uint8 a8;
  uint8 x;
  uint8 y;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
     z = 256;
     require(x > z - 2); 
	 a8 = x;
	 a8 += 3;
	 require(a8 >= x);      //fails with mod arithm; holds otherwise
     assert(false);         //not reachable with mod arithm; reachable otherwise
  }
}