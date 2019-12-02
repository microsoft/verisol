pragma solidity >=0.4.24 <0.6.0;

// Test uint8 overflow with and without /useModularArithmetic option
// This test checks conversions between ints and fixed-size byte arrays
// The test should pass with /useModularArithmetic option and fail    otherwise
contract UintTest {
  
  uint8 a8;
  uint8 x;
  uint8 y;
  //uint16 b;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public {
	 int256 b = - 2;
	 bytes4 c = hex"0100"; //256
	 uint32 d = uint32(c);
	 require(x > d + b); 
	 a8 = x + 3;                                                                                                     
     require(a8 >= x);        //fails with mod arithm; holds otherwise
	 assert (false);          //not reachable with mod arithm; reachable otherwise
  }
}