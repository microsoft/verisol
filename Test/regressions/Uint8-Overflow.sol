pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow with and without /useModularArithmetic option
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint8 z8;
  uint8 a8;
  uint8 x;
  uint8 y;
  //uint16 b;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y, uint16 b) public {
	 //require(x > 256 - 2); 
     //a8 = 3 + x;
	 //a8 = 257 + x;
      //b = 257 + x;
	  //x = 257 + b;
	  //b = b;
	  assert(x + b == b + x);
	 //a8 = x + 3;                                                                                                     
     require(a8 >= x);        //fails with mod arithm; holds otherwise
	 //assert (false);          //not reachable with mod arithm; reachable otherwise
  }
}