pragma solidity >=0.4.24 <0.6.0;

contract Throw {

    function testThrow(uint a) public {
        // raw throw is deprecated since version 0.4.13
        // but still common in legacy smart contracts
        if (a < 10) revert();
        assert (a >= 10);
    }

}
