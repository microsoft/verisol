pragma solidity >=0.5.0;

contract ERC20 {
    function transfer(address recipient, uint256 amount) external returns (bool)
    {  
       assert (false); //should fail
       return false;
    }
}

contract Test {
constructor(ERC20 token) public {
  token.transfer(msg.sender, 1);
 }
}