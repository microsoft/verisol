pragma solidity >=0.4.24 <0.6.0;

// Test uint256 overflow with and without /useModularArithmetic option
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint256 z256;
  uint256 a256;
  uint256 x;
  uint256 y;
  
  constructor () public {
  }
  
  function test(uint256 x, uint256 y) public {  
	 require(x > 115792089237316195423570985008687907853269984665640564039457584007913129639936 - 2);  
	 //require(x > 2**256 -2);
	 a256 = x + 3;                                                                                                     
     require(a256 >= x);        //fails with mod arithm; holds otherwise
	 assert (false);          //not reachable with mod arithm; reachable otherwise
  }
}