pragma solidity >=0.4.24 <0.6.0;

contract Revert {

    function testRevert(uint a) public {
        if (a < 10) {
            revert();
        }
        assert (a >= 10);
    }

    function testRevertWithMessage(uint a) public {
        if (a < 10) {
            revert('Invalid a!');
        }
        assert (a >= 10);
    }

}
