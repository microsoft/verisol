pragma solidity >=0.4.24 <0.6.0;

import "SafeMath.sol";

library Lib {
	using SafeMath for uint256;
	
	//struct Counter {
	//	uint256 value;  //default: 0
	//}
	
	//function decrement(Counter storage counter) internal {
	function decrement(uint256 value) internal {
        //counter.value = counter.value.sub(1);
		value = value.sub(1);
    }

}
contract A {
    //using Lib for Lib.Counter;
	using Lib for uint256;
	
	//mapping (address => Lib.Counter) private count;
	mapping (address => uint256) private count;

    function foo(address x, uint256 id) internal {

        count[x].decrement();
 
    }
}
