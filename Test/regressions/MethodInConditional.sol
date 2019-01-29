pragma solidity ^0.4.24;

contract A {
      uint x;

      constructor () public {x = 0;}
      function X() private view returns (uint) {return x;}
      function Y() public view {
              if (X() != 0) {
                        assert(false);
              }
      }
}
