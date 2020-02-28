pragma solidity >=0.4.24 <0.6.0;

import "SafeMath.sol";

library Lib {
	using SafeMath for uint256;
	
	struct Counter {
		uint256 value1;  //default: 0
		uint256[2] value2;
	}
	
	function decrement(Counter storage counter) internal {
        counter.value1 = counter.value1.sub(1);
		counter.value2[0] = counter.value2[0].sub(1);
		counter.value2[1] = counter.value2[1].sub(1);
    }

}
contract A {
    using Lib for Lib.Counter;
	
	mapping (address => Lib.Counter) private count;

    function foo(address x, uint256 id) public {
		count[x].value1 = 1;
		count[x].value2[0] = 2;
		count[x].value2[1] = 3;
        count[x].decrement();
		assert(count[x].value1 == 0);
		assert(count[x].value2[0] == 1);
		assert(count[x].value2[1] == 2);
    }
}
