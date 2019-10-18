pragma solidity >=0.4.24 <0.6.0;

import "./SafeMath.sol"; 

// This test is identical to Overflow1 and is therefore redundant.
// Test uint8 overflow of "+=" with and without /useModularArithmetic option
// The test passes with /useModularArithmetic option and fails otherwise
contract UintTest {
  using SafeMath for uint256;
  
  mapping (address => uint256) private balances;
  
  constructor () public {
  }
  
  function batchTransfer(address[] memory receivers, uint256 value) public {
    uint256 amount = receivers.length * value;  // amount could overflow and be 0
	require(amount == 0);	
	assert(false);
	
	
    require(balances[msg.sender] >= amount);

    balances[msg.sender] = balances[msg.sender].sub(amount);
    for (uint256 i = 0; i < receivers.length; i++) {
        balances[receivers[i]] = balances[receivers[i]].add(value);
    }
}
}