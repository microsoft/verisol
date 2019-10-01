pragma solidity >=0.4.24 <0.6.0;

contract C {

   function setOwner() public {
      address owner = tx.origin;
      uint price = tx.gasprice;
      uint x = now;  
   }
}