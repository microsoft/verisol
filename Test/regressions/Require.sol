pragma solidity >=0.4.24 <0.6.0;

contract Require {

    function testRequire(uint a) public {
        require (a >= 10);
        assert (a >= 10);
    }

    function testRequireWithMessage(uint a) public {
        require (a >= 10, 'Invalid a!');
        assert (a >= 1);
    }

}
