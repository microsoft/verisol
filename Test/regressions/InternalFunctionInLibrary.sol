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
	
	function decrementTwice(Counter storage counter) internal {
		uint256 v;
		v = counter.value;
		decrement(counter);
		decrement(counter);
		assert(v == counter.value + 2);
    }

}
contract A {
    using Lib for Lib.Counter;
	
	mapping (address => Lib.Counter) private count;

    function foo(address x, uint256 id) public {
		uint256 v;
		v = count[x].value;
        count[x].decrement();
		count[x].decrementTwice();
		assert(v == count[x].value + 3);
    }
}
