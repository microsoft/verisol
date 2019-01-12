pragma solidity ^0.4.24;

contract Throw {

    function testThrow(uint a) public {
        // raw throw is deprecated since version 0.4.13
        // but still common in legacy smart contracts
        if (a < 10) throw;
        assert (a >= 10);
    }

}
