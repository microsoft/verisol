pragma solidity >=0.4.24 <0.6.0;

import "SafeMath.sol";

library Counters {
	using SafeMath for uint256;
	
	struct Counter {
		uint256 _value;  //default: 0
	}
	
	function decrement(Counter storage counter) internal {
        counter._value = counter._value.sub(1);
    }

}
contract NonFungibleBasic {
    using Counters for Counters.Counter;
	
	mapping (address => Counters.Counter) private _ownedTokensCount;

    function _burn(address owner, uint256 tokenId) internal {

        _ownedTokensCount[owner].decrement();
 
    }
}
