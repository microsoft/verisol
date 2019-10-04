pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow with and without /useModularArithmetic option
contract UintTest {
  
  uint8 z8;
  uint8 a8;
  
  constructor () public {
  }
  
  function test(uint8 x8, uint8 y8) public {
	 z8 = x8 - y8;
	 require(x8 >= y8);  
	 
	 a8 = z8 + y8;
	 require(a8 >= z8);
	 
	 // Fails with /useModularArithmetic (x8 = 304, y8 = 256);
	 // passes otherwise
	 assert(a8 == x8);
  }
}