pragma solidity >=0.4.24 <0.6.0;

// Issue with the test when lines 15-21 are uncommented: no counterexample is generated; invariant needed
// Tests uint256 overflow with and without /useModularArithmetic option
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  
  uint256 a256;
  
  constructor () public {
  }
  
  function test(uint256 x, uint256 y) public {  
	//uint const = 1;
	//uint i = 0;
	// const := 2 ^ 256:
	//do {
	//	const *= 2;
	//	i++;
	//} while (i < 257);
	//require (x > const - 2);  //OK, but proof not found
	 //require(x > 115792089237316195423570985008687907853269984665640564039457584007913129639936 - 2);  //OK
	 require(x > 2**256 - 2);
	 a256 = x + 3;                                                                                                     
     require(a256 >= x);        //fails with mod arithm; holds otherwise
	 assert (false);          //not reachable with mod arithm; reachable otherwise
  }
}