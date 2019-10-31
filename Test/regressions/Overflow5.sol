pragma solidity >=0.4.24 <0.6.0;

import "./SafeMath.sol"; 


// The test ?? with /useModularArithmetic option and ?? otherwise
contract UintTest {
  using SafeMath for uint256;
  
  mapping (address => uint256) private balances;
  
  constructor () public {
  }
  
  function batchTransfer(address[] memory receivers, uint256 value) public {
    uint256 amount = receivers.length * value;  // amount could overflow and be 0
	//require(value > 0 && balances[msg.sender] >= amount);
	require(amount > 0);	//fails with mod arithm, holds otherwise
	assert(false);          //not reachable with mod arithm, reachable otherwise
	
    balances[msg.sender] = balances[msg.sender].sub(amount);
    //for (uint256 i = 0; i < receivers.length; i++) {
    //    balances[receivers[i]] = balances[receivers[i]].add(value);
    //}
}
}