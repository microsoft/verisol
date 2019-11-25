pragma solidity >=0.4.24 <0.6.0;

import "./SafeMath.sol"; 

// This test models the "batch overflow" bug
// When "*" is used, the test fails with /useModularArithmetic option and passes otherwise
// Issue: we do not print array pars values, so the value of receivers.length
// in the counterexample is not printed
// With SafeMath.mul, no overflow, the test passes in both cases
contract UintTest {
  using SafeMath for uint256;
  
  mapping (address => uint256) private balances;
  
  constructor () public {
  }
  
  function batchTransfer(address[] memory receivers, uint256 value) public {
    uint256 amount = receivers.length * value;  // amount could overflow and be 0
	//uint256 amount = SafeMath.mul(receivers.length, value);
	require(value > 0 && balances[msg.sender] >= amount);
	require(receivers.length > 0);
	require(receivers.length < 2**40);  // this is needed, such as "value" overflows
									    // and not receivers.length
	require(amount == 0);	//holds with mod arithm, fails otherwise
	assert(false);          //reachable with mod arithm, not reachable otherwise
	
    //balances[msg.sender] = balances[msg.sender].sub(amount);
    //for (uint256 i = 0; i < receivers.length; i++) {
    //    balances[receivers[i]] = balances[receivers[i]].add(value);
    //}
}
}