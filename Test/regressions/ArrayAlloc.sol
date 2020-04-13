// Example with allocation for arrays

pragma solidity >=0.4.24 <0.6.0;

contract A {

     mapping (address => uint256[]) private lockTime;

     function Foo() public {
        uint256[] memory tempLockTime = new uint256[](50);
	for (uint i = 0; i < 50; ++i) {
	      tempLockTime[i] = lockTime[address(9)][i] + 44;
	}
        assert(tempLockTime.length == 50);
     }

 
}
