pragma solidity ^0.4.24;

contract ModifierFunc {
 
  function isValid(uint id) public view returns (bool) 
  { 
    return (id > 0);
  }

  modifier valid(uint id) {
   require(isValid(id), "id is not valid"); // this invocation produces an exception
   _;
  }
}
