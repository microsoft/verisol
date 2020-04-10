pragma solidity >=0.4.24 <0.6.0;

import "SafeMath.sol";

library Lib {
	using SafeMath for uint256;
	
	function decrement(uint256 value) internal returns (uint256) {
		return value.sub(1);		
    }

}
contract A {
	using Lib for uint256;
	
	mapping (address => uint256) private count;

    function foo(address x, uint256 id) public {
		uint256 v = count[x];
        count[x] = count[x].decrement();
		assert(v == count[x] + 1);
    }
}
