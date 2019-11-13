pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow of "--" with and without /useModularArithmetic option
// The test fails with /useModularArithmetic option and passes otherwise
contract UintTest {
  
  int z;
  uint8 a8;
  uint8 b8;
  uint8 x;
  uint8 y;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
     z = 1;
     require(x < z + 1);       
	 a8 = x;
	 a8--;
	 a8--;
	 require(a8 >= 254);    //fails without mod arithm; holds otherwise
     assert(false);         //not reachable without mod arithm; reachable otherwise
  }
}