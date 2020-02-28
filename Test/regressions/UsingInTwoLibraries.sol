pragma solidity >=0.4.24 <0.6.0;

import "SafeMath.sol";

library Lib {
	using SafeMath for uint256;
	
	struct Counter {
		uint256 value;  //default: 0
	}
	
	function decrement(Counter storage counter) internal {
        counter.value = counter.value.sub(1);
    }

}
contract A {
    using Lib for Lib.Counter;
	
	mapping (address => Lib.Counter) private count;

    function foo(address x, uint256 id) public {
		uint256 v = count[x].value;
        count[x].decrement();
		assert(v == count[x].value + 1);
    }
}
