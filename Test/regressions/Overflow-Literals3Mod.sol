pragma solidity >=0.4.24 <0.6.0;

// TODO: same translation error as for Overflow9.sol; debug
// Test uint8 overflow with and without /useModularArithmetic option
// This test checks mixed expression with constants and variables
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint z;
  uint8 z1;
  uint8 a8;
  uint8 x;
  uint8 y;
  
  constructor () public {
  }
  
  function test(uint8 x, uint8 y) public  {
	 z1 = 254;
	 // Same results for: z1+1+1; 1+z1+1; 1+1+z1;
	 //z = 1 + z1 + 1;           //OK
	 //z = z1 + 1 + 1;             //OK
	 z = 1 + 1 + z1;
     require(x > z - 2); 
	 a8 = x + 3;
	 require(a8 >= x);      //fails with mod arithm; holds otherwise
     assert(false);         //not reachable with mod arithm; reachable otherwise
  }
}