pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint public y;
    function helper() private view returns (uint) {return 1;}
    constructor(A a) public {
          uint z;
          y = helper(); //this should be broken up into a temporary
          assert (y == 1);
          z = helper(); //this should no be broken up into a temprary
          assert (z == 1);
    }
}